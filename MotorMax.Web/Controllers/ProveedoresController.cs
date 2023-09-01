using AutoMapper;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using MotorMax.Web.App_Start;
using MotorMax.Web.ViewModels.Ciudad;
using MotorMax.Web.ViewModels.Cliente;
using MotorMax.Web.ViewModels.Proveedor;
using MotorMax.Web.ViewModels.Provincia;
using MotorMax.Web.ViewModels.Repuesto;
using MotorMax.Web.ViewModels.RepuestoProveedor;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MotorMax.Web.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly IServiciosProveedores _servicios;
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IServiciosProvincias _serviciosProvincias;
        private readonly IServiciosRepuestosProveedores _servRepProv;
        private readonly IServiciosRepuestos _serviciosRepuestos;
        private readonly IMapper _mapper;
        public ProveedoresController(IServiciosProveedores servicios,
            IServiciosCiudades serviciosCiudades,
            IServiciosProvincias serviciosProvincias,
            IServiciosRepuestosProveedores servRepProv,
            IServiciosRepuestos serviciosRepuestos)
        {
            _servicios = servicios;
            _serviciosCiudades = serviciosCiudades;
            _serviciosProvincias = serviciosProvincias;
            _mapper = AutoMapperConfig.Mapper;
            _servRepProv = servRepProv;
            _serviciosRepuestos = serviciosRepuestos;
        }
        // GET: Proveedors
        public ActionResult Index(int? page, int? pageSize, string SortBy = "Proveedor", string SearchBy = null)
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            var lista = _servicios.GetProveedores();
            if (SearchBy != null)
            {
                lista = lista
                    .Where(c => c.NombreProveedor.Contains(SearchBy) || c.Ciudad.Contains(SearchBy))
                    .ToList();
            }

            var listaVm = _mapper.Map<List<ProveedorListVm>>(lista);
            if (SortBy == "Proveedor")
            {
                listaVm = listaVm.OrderBy(c => c.NombreProveedor).ToList();
            }
            else
            {
                listaVm = listaVm.OrderBy(c => c.Ciudad).ThenBy(c => c.Ciudad).ToList();
            }
            var proveedorVm = new ProveedorListSortVm
            {
                Proveedores = listaVm.ToPagedList(page.Value, pageSize.Value),
                Sorts = new Dictionary<string, string> {
                    {"Por Proveedor","Proveedor"},
                    {"Por Ciudad","Ciudad" }
                },
                SortBy = SortBy,
                SearchBy = SearchBy
            };

            return View(proveedorVm);
        }

        public ActionResult Create()
        {
            var proveedorVm = new ProveedorEditVm
            {
                Ciudades = _serviciosCiudades.GetCiudadesDropDownList()

            };
            return View(proveedorVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProveedorEditVm proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList();
                return View(proveedorVm);
            }
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorVm);
                if (!_servicios.Existe(proveedor))
                {
                    _servicios.Guardar(proveedor);
                    TempData["Msg"] = "Registro agregado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                    proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList();

                    return View(proveedorVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Proveedor");
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList();

                return View(proveedorVm);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Cód. de proveedor inexistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorEditVm>(proveedor);
            proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList();
            return View(proveedorVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProveedorEditVm proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList();
                return View(proveedorVm);

            }
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorVm);
                if (!_servicios.Existe(proveedor))
                {
                    _servicios.Guardar(proveedor);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                    proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList();
                    return View(proveedorVm);

                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList();
                return View(proveedorVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Cód. proveedor inexistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            return View(proveedorVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var proveedor = _servicios.GetProveedorPorId(id);
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            try
            {
                if (!_servicios.EstaRelacionado(proveedor))
                {
                    _servicios.Borrar(id);
                    TempData["Msg"] = "Proveedor borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registro relacionado... Baja denegada");
                    return View(proveedorVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intengar borrar un proveedor");
                return View(proveedorVm);

            }
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Código de proveedor inexistente");
            }
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            proveedorVm.CantidadRepuestos = _servRepProv.GetListaPorProveedor(proveedor.ProveedorId).Count();
            var proveedorDetailVm = new ProveedorDetailVm()
            {
                Proveedor = proveedorVm,
                Repuestos = _mapper.Map<List<RepuestoListVm>>(
                _servRepProv.GetRepuestosPorProveedor(proveedor.ProveedorId))
            };
            return View(proveedorDetailVm);
        }


        public ActionResult AddRepuesto(int? proveedorId)
        {
            if (proveedorId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(proveedorId.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Código de proveedor inesistente!!!");
            }
            var repuestoProveedorVm = new RepuestoProveedorEditVm
            {
                Repuestos = _serviciosRepuestos.GetRepuestosDropDownList(),
                Proveedores = _servicios.GetProveedoresDropDownList(),
                ProveedorId = proveedorId.Value

            };
            return View(repuestoProveedorVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRepuesto(RepuestoProveedorEditVm repuestoProveedorVm)
        {
            if (!ModelState.IsValid)
            {
                repuestoProveedorVm.Repuestos = _serviciosRepuestos.GetRepuestosDropDownList();
                repuestoProveedorVm.Proveedores = _servicios.GetProveedoresDropDownList();
                return View(repuestoProveedorVm);
            }
            try
            {
                var repuestoProveedor = _mapper.Map<RepuestoProveedor>(repuestoProveedorVm);
                if (!_servRepProv.Existe(repuestoProveedor))
                {
                    _servRepProv.Guardar(repuestoProveedor);
                    return RedirectToAction($"Details/{repuestoProveedor.ProveedorId}");
                }
                else
                {
                    repuestoProveedorVm.Proveedores = _servicios.GetProveedoresDropDownList();
                    repuestoProveedorVm.Repuestos = _serviciosRepuestos.GetRepuestosDropDownList();
                    ModelState.AddModelError(string.Empty, "Registro existente!!!");
                    return View(repuestoProveedorVm);
                }
            }
            catch (Exception)
            {

                repuestoProveedorVm.Proveedores = _servicios.GetProveedoresDropDownList();
                repuestoProveedorVm.Repuestos = _serviciosRepuestos.GetRepuestosDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Repuesto");
                return View(repuestoProveedorVm);

            }
        }


        //public ActionResult DeleteRepuesto(int? id, int? proveedorId)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //    }
        //    var repuestoProveedor = _serviciosRepuestos.GetRepuestoPorId(id.Value);
        //    if (repuestoProveedor == null)
        //    {
        //        return HttpNotFound("Código de repuesto inexistente!!!");
        //    }
        //    var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
        //    ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
        //    return View(ciudadVm);

        //}
        //[HttpPost, ActionName("DeleteCity")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteCityConfirm(int id)
        //{
        //    var ciudad = _serviciosCiudades.GetCiudadPorId(id);
        //    var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
        //    try
        //    {
        //        if (!_serviciosCiudades.EstaRelacionada(ciudad))
        //        {
        //            _serviciosCiudades.Borrar(id);
        //            return RedirectToAction($"Details/{ciudad.ProvinciaId}");
        //        }
        //        else
        //        {
        //            ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
        //            ModelState.AddModelError(string.Empty, "Ciudad relacionada... Baja denegada");
        //            return View(ciudadVm);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
        //        ModelState.AddModelError(string.Empty, "Error al intentar borrar una ciudad");
        //        return View(ciudadVm);

        //    }
        //}

    }
}