using AutoMapper;
using MotorMax.Entidades.Entidades;
using MotorMax.Entidades.Enums;
using MotorMax.Servicios;
using MotorMax.Servicios.Interfaces;
using MotorMax.Servicios.Servicios;
using MotorMax.Web.App_Start;
using MotorMax.Web.Models.Carrito;
using MotorMax.Web.ViewModels.Carrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IServiciosRepuestos _serviciosRepuestos;
        private readonly IServiciosCarrito _serviciosCarritos;
        private readonly IServiciosVentas _serviciosVentas;
        private readonly IServiciosClientes _serviciosClientes;
        private readonly IMapper _mapper;
        public CarritoController(IServiciosRepuestos serviciosRepuestos, IServiciosCarrito serviciosCarrito,
            IServiciosVentas serviciosVenta, IServiciosClientes serviciosClientes)
        {
            _serviciosRepuestos = serviciosRepuestos;
            _serviciosCarritos = serviciosCarrito;
            _serviciosVentas = serviciosVenta;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosClientes = serviciosClientes;
        }

        private Carrito _carrito;

        public ActionResult Index(string returnUrl)
        {
            _carrito = GetCarrito();
            if (_carrito.GetCantidad() == 0)
            {
                return View("EmptyCart");
            }
            var listaItem = _carrito.GetItems();
            var listaItemVm = _mapper.Map<List<ItemCarritoVm>>(listaItem);

            CarritoVm carritoVm = new CarritoVm
            {
                Items = listaItemVm,
                returnUrl = returnUrl
            };
            return View(carritoVm);
        }
        public ActionResult MostrarCarrito()
        {
            _carrito = GetCarrito();
            return PartialView("_MostrarCarrito", _carrito);
        }

        private Carrito GetCarrito()
        {
            if (_carrito != null)
            {
                return _carrito;
            }
            _carrito = (Carrito)Session["carrito"];
            if (_carrito == null)
            {
                _carrito = new Carrito();
                Session["carrito"] = _carrito;
            }
            return _carrito;
        }

        [HttpPost]
        public ActionResult AddToCart(int repuestoId, string returnUrl, int cantidad = 1)
        {
            var repuestoDto = _serviciosRepuestos.GetRepuestoPorId(repuestoId);
            if (repuestoDto.Stock >= cantidad)
            {
                ItemCarrito itemCarrito = new ItemCarrito
                {
                    RepuestoId = repuestoDto.RepuestoId,
                    UserName = User.Identity.Name,
                    Repuesto = repuestoDto.Descripcion,
                    PrecioUnitario = repuestoDto.PrecioUnitario,
                    Cantidad = cantidad,

                };
                _carrito = GetCarrito();
                _carrito.AddToCart(itemCarrito);
                Session["carrito"] = _carrito;
                _serviciosCarritos.Guardar(itemCarrito);
                _serviciosRepuestos.ActualizarUnidadesEnPedido(repuestoId, cantidad,true);
                return RedirectToAction("Index", new { returnUrl });

            }
            else
            {
                TempData["Error"] = "Cantidad mayor al stock";
                return RedirectToAction("Index", new { returnUrl });
            }
        }

        public ActionResult RemoveFromCart(int repuestoId, string returnUrl)
        {
            _carrito = GetCarrito();
            _carrito.RemoveFromCart(repuestoId);
            Session["carrito"] = _carrito;
            _serviciosRepuestos.ActualizarUnidadesEnPedido(repuestoId, _carrito.GetItems()[0].Cantidad,false);
            _serviciosCarritos.Borrar(User.Identity.Name, repuestoId);
            return RedirectToAction("Index", new { returnUrl });

        }
        public ActionResult CancelOrder()
        {
            _carrito = GetCarrito();
            foreach (var item in _carrito.GetItems())
            {
                _serviciosCarritos.Borrar(User.Identity.Name, item.RepuestoId);
                _serviciosRepuestos.ActualizarUnidadesEnPedido(item.RepuestoId, _carrito.GetItems()[0].Cantidad, false);
            }
            _carrito.Clear();
            Session["carrito"] = _carrito;

            return View();
        }

        public ActionResult ConfirmOrder(string returnUrl)
        {
            //TODO: Hacer la confirmación de la orden
            _carrito = GetCarrito();
            if (_carrito.GetCantidad() == 0)
            {
                return View("EmptyCart");
            }
            var listaItem = _carrito.GetItems();
            var listaItemVm = _mapper.Map<List<ItemCarritoVm>>(listaItem);

            CarritoVm carritoVm = new CarritoVm
            {
                Items = listaItemVm,
                returnUrl = returnUrl
            };
            CheckOutVm model = new CheckOutVm()
            {
                Carrito = carritoVm
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmOrder(CheckOutVm model)
        {
            _carrito = GetCarrito();
            var listaItem = _carrito.GetItems();
            var listaItemVm = _mapper.Map<List<ItemCarritoVm>>(listaItem);

            CarritoVm carritoVm = new CarritoVm
            {
                Items = listaItemVm,
            };
            model.Carrito = carritoVm;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var VentaId = CheckOut(model);
            if (VentaId!=0)
            {
                TempData["TransaccionId"] = VentaId.ToString();
                return RedirectToAction("Complete");
            }
            ModelState.AddModelError(string.Empty, "No se ha podido concretar la venta");
            return View(model);

        }


        int id;
        private int CheckOut(CheckOutVm model)
        {
            
            Cliente cliente = _serviciosClientes.GetClientePorUsuario(User.Identity.Name);
            var venta = new Venta()
            {
                ClienteId = cliente.ClienteId,
                FechaOperacion = DateTime.Now,
                Estado = Estado.Impaga,
                Monto = _carrito.GetTotal()
            };
            foreach (var item in model.Carrito.Items)
            {
                DetalleVenta detalleVenta = new DetalleVenta()
                {

                    RepuestoId = item.RepuestoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,

                };
                venta.Detalles.Add(detalleVenta);
            }


            bool esValido = false;
            
            if (    (model.CardNumber.Length == 16) && (model.CVV.Length == 4) &&
                 (int.Parse(model.Year) <= (DateTime.Today.Year + 3) && (int.Parse(model.Year) > DateTime.Today.Year))  )
            {
                esValido = true;
            }
            else if ((int.Parse(model.Year) == DateTime.Today.Year) && (int.Parse(model.Month) > DateTime.Today.Month))
            {
                esValido = true;
            } 

            if (esValido)
            {
                string idTransaction = (_serviciosVentas.GetVentas().Last().VentaId + 1).ToString();


                _carrito = GetCarrito();
                _carrito.Clear();
                Session["carrito"] = _carrito;

                venta.TransactionId = idTransaction;
                id= _serviciosVentas.Guardar(venta, User.Identity.Name);
            }

            return id;
        }

        public ActionResult Complete()
        {
            ViewBag.TransaccionId = (string)TempData["TransaccionId"];
            return View();
        }
    }
}