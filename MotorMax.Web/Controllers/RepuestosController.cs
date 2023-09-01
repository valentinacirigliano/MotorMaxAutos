
using MotorMax.Entidades.Dto.Repuesto;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using MotorMax.Servicios.Servicios;
using MotorMax.Web.App_Start;
using MotorMax.Web.ViewModels.Repuesto;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using MotorMax.Web.ViewModels.Proveedor;
using MotorMax.Web.ViewModels.Ciudad;
using MotorMax.Web.ViewModels.Provincia;
using MotorMax.Web.ViewModels.RepuestoProveedor;

namespace MotorMax.Web.Controllers
{
    public class RepuestosController : Controller
    {
        private readonly IServiciosRepuestos _servicios;
        private readonly IServiciosCategorias _serviciosCategorias;
        private readonly IServiciosRepuestosProveedores _serviciosRepProv;
        private readonly IServiciosProveedores _serviciosProveedores;
        private readonly IMapper _mapper;
        public RepuestosController(IServiciosRepuestos serviciosRepuestos, IServiciosCategorias serviciosCategorias,
            IServiciosRepuestosProveedores serviciosRepuestosProveedores, IServiciosProveedores serviciosProveedores)
        {
            _servicios = serviciosRepuestos;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosCategorias = serviciosCategorias;
            _serviciosRepProv = serviciosRepuestosProveedores;
            _serviciosProveedores = serviciosProveedores;
        }
        // GET: Repuestos
        public ActionResult Index(int? CategoriaFiltro, int? page, int? pageSize, string SearchBy = null)
        {
            List<RepuestoListDto> lista;

            if (CategoriaFiltro == null)
            {
                Func<Repuesto, bool> predicado = p => p.Suspendido == false;
                lista = _servicios.Filtrar(predicado);

            }
            else
            {
                Func<Repuesto, bool> predicado = p => (p.Suspendido == false) && p.CategoriaId == CategoriaFiltro.Value;
                lista = _servicios.Filtrar(predicado);

            }

            if (SearchBy != null)
            {
                lista = lista
                    .Where(c => c.Descripcion.Contains(SearchBy))
                    .ToList();
            }

            var listaVm = _mapper.Map<List<RepuestoListVm>>(lista).OrderBy(c => c.Descripcion);
            page = page ?? 1;
            pageSize = pageSize ?? 6;

            var repuestoVm = new RepuestoFiltroVm
            {
                CategoriaFiltro = CategoriaFiltro,
                Categorias = _serviciosCategorias.GetCategoriasDropDownList(),
                Repuestos = listaVm.ToPagedList(page.Value, pageSize.Value)
            };
            return View(repuestoVm);
        }


        public ActionResult Create()
        {
            var repuestoVm = new RepuestoEditVm
            {
                Categorias = _serviciosCategorias.GetCategoriasDropDownList()

            };
            return View(repuestoVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RepuestoEditVm repuestoVm)
        {
            if (!ModelState.IsValid)
            {
                repuestoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                return View(repuestoVm);
            }
            try
            {
                var repuesto = _mapper.Map<Repuesto>(repuestoVm);
                if (!_servicios.Existe(repuesto))
                {

                    _servicios.Guardar(repuesto);
                    TempData["Msg"] = "Registro agregado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Repuesto existente!!!");
                    repuestoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();

                    return View(repuestoVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Repuesto");
                repuestoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();

                return View(repuestoVm);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var repuesto = _servicios.GetRepuestoPorId(id.Value);
            if (repuesto == null)
            {
                return HttpNotFound("Cód. de repuesto inexistente!!!");
            }
            var repuestoVm = _mapper.Map<RepuestoEditVm>(repuesto);
            repuestoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
            return View(repuestoVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RepuestoEditVm repuestoVm)
        {
            if (!ModelState.IsValid)
            {
                repuestoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                return View(repuestoVm);

            }
            try
            {
                var repuesto = _mapper.Map<Repuesto>(repuestoVm);
                if (!_servicios.Existe(repuesto))
                {
                    _servicios.Guardar(repuesto);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Repuesto existente!!!");
                    repuestoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                    return View(repuestoVm);

                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Repuesto existente!!!");
                repuestoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                return View(repuestoVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var repuesto = _servicios.GetRepuestoPorId(id.Value);
            if (repuesto == null)
            {
                return HttpNotFound("Cód. repuesto inexistente!!!");
            }
            var repuestoVm = _mapper.Map<RepuestoListVm>(repuesto);
            return View(repuestoVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var repuesto = _servicios.GetRepuestoPorId(id);
            var repuestoVm = _mapper.Map<RepuestoListVm>(repuesto);
            try
            {
                if (!_servicios.EstaRelacionado(repuesto))
                {
                    _servicios.Borrar(id);
                    TempData["Msg"] = "Repuesto borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registro relacionado... Baja denegada");
                    return View(repuestoVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intengar");
                return View(repuestoVm);

            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var repuesto = _servicios.GetRepuestoPorId(id.Value);
            if (repuesto == null)
            {
                return HttpNotFound("Cód. repuesto inexistente!!!");
            }
            var repuestoVm = _mapper.Map<RepuestoListVm>(repuesto);
            return View(repuestoVm);
        }

        public ActionResult ProveedoresDelRepuesto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var repuesto = _servicios.GetRepuestoPorId(id.Value);
            var proveedores = _serviciosRepProv.GetProveedoresPorRepuesto(id.Value);
            if (proveedores == null)
            {
                return HttpNotFound("Cód. repuesto inexistente!!!");
            }
            var repuestoVm = _mapper.Map<RepuestoListVm>(repuesto);
            var repuestoDetailVm = new RepuestoDetailVm()
            {
                Repuesto = repuestoVm,
                Proveedores = _mapper.Map<List<ProveedorListVm>>(_serviciosRepProv.GetProveedoresPorRepuesto(id.Value))
            };
            return View(repuestoDetailVm);
        }


        public ActionResult AddProveedor(int? repuestoId)
        {
            if (repuestoId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var repuesto = _servicios.GetRepuestoPorId(repuestoId.Value);
            if (repuesto == null)
            {
                return HttpNotFound("Código de repuesto inesistente!!!");
            }
            var repuestoProveedorVm = new RepuestoProveedorEditVm
            {
                Repuestos = _servicios.GetRepuestosDropDownList(),
                Proveedores = _serviciosProveedores.GetProveedoresDropDownList(),
                RepuestoId = repuestoId.Value

            };
            return View(repuestoProveedorVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRepuesto(RepuestoProveedorEditVm repuestoProveedorVm)
        {
            if (!ModelState.IsValid)
            {
                repuestoProveedorVm.Repuestos = _servicios.GetRepuestosDropDownList();
                repuestoProveedorVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                return View(repuestoProveedorVm);
            }
            try
            {
                var repuestoProveedor = _mapper.Map<RepuestoProveedor>(repuestoProveedorVm);
                if (!_serviciosRepProv.Existe(repuestoProveedor))
                {
                    _serviciosRepProv.Guardar(repuestoProveedor);
                    return RedirectToAction($"ProveedoresDelRepuesto/{repuestoProveedor.ProveedorId}");
                }
                else
                {
                    repuestoProveedorVm.Proveedores = _servicios.GetRepuestosDropDownList();
                    repuestoProveedorVm.Repuestos = _serviciosProveedores.GetProveedoresDropDownList();
                    ModelState.AddModelError(string.Empty, "Registro existente!!!");
                    return View(repuestoProveedorVm);
                }
            }
            catch (Exception)
            {

                repuestoProveedorVm.Proveedores = _servicios.GetRepuestosDropDownList();
                repuestoProveedorVm.Repuestos = _serviciosProveedores.GetProveedoresDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Proveedor");
                return View(repuestoProveedorVm);

            }
        }

    }
}