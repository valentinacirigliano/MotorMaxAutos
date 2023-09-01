using AutoMapper;
using MotorMax.Servicios.Interfaces;
using MotorMax.Servicios.Servicios;
using MotorMax.Web.App_Start;
using MotorMax.Web.ViewModels.Ciudad;
using MotorMax.Web.ViewModels.DetalleVenta;
using MotorMax.Web.ViewModels.Proveedor;
using MotorMax.Web.ViewModels.Venta;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.Controllers
{
    public class VentasController : Controller
    {
        private readonly IServiciosVentas _servicio;
        private readonly IMapper _mapper;
        public VentasController(IServiciosVentas servicio)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index(int? page, int? pageSize, string SortBy = "Fecha", string SearchBy = null)
        {

            //var lista = _servicio.GetVentas();
            //var listaVm = _mapper.Map<List<VentaListVm>>(lista);

            //return View(listaVm);
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            var lista = _servicio.GetVentas();
            if (SearchBy != null)
            {
                lista = lista
                    .Where(c => c.Cliente.Contains(SearchBy))
                    .ToList();
            }

            var listaVm = _mapper.Map<List<VentaListVm>>(lista);
            if (SortBy == "Fecha")
            {
                listaVm = listaVm.OrderBy(c => c.FechaOperacion).ToList();
            }
            else
            {
                listaVm = listaVm.OrderBy(c => c.Monto).ToList();
            }
            var ventaVm = new VentaListSortVm
            {
                Ventas = listaVm.ToPagedList(page.Value, pageSize.Value),
                Sorts = new Dictionary<string, string> {
                    {"Por Fecha","Fecha"},
                    {"Por Monto","Monto" }
                },
                SortBy = SortBy,
                SearchBy = SearchBy
            };

            return View(ventaVm);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var venta = _servicio.GetVentaListDtoPorId(id.Value);
            if (venta == null)
            {
                return HttpNotFound("Código no encontrado");
            }
            var detalle = _servicio.GetDetalleVenta(id.Value);
            var detalleVm = _mapper.Map<List<DetalleVentaListVm>>(detalle);
            var ventaVm = new VentaDetailVm
            {
                Venta = _mapper.Map<VentaListVm>(venta),
                Detalles = detalleVm
            };
            return View(ventaVm);
        }
    }
}