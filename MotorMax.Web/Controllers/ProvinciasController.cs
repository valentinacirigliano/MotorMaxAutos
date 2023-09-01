using AutoMapper;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using MotorMax.Web.App_Start;
using MotorMax.Web.ViewModels.Ciudad;
using MotorMax.Web.ViewModels.Provincia;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MotorMax.Web.Controllers
{
    public class ProvinciasController : Controller
    {
        private readonly IServiciosProvincias _servicios;
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IMapper _mapper;
        public ProvinciasController(IServiciosProvincias servicios, IServiciosCiudades serviciosCiudades)
        {
            _servicios = servicios;
            _serviciosCiudades = serviciosCiudades;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index(int? page, int? pageSize, string SearchBy = null)
        {
            var lista = _servicios.GetProvincias();
            var listaVm = _mapper.Map<List<ProvinciaListVm>>(lista);
            
            listaVm.ForEach(p => p.CantidadCiudades = _serviciosCiudades
                    .GetCantidad(c => c.ProvinciaId == p.ProvinciaId));
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;

            if (SearchBy != null)
            {
                listaVm = listaVm
                    .Where(c => c.Nombre.Contains(SearchBy))
                    .ToList();
            }
            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProvinciaEditVm provinciaVm)
        {
            if (ModelState.IsValid)
            {
                var provincia = _mapper.Map<Provincia>(provinciaVm);
                if (_servicios.Existe(provincia))
                {
                    ModelState.AddModelError(string.Empty, "Provincia existente!!!");
                    return View(provinciaVm);
                }
                _servicios.Guardar(provincia);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index");

            }
            else
            {
                return View(provinciaVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var provincia = _servicios.GetProvinciaPorId(id.Value);
            if (provincia == null)
            {
                return HttpNotFound("Código de provincia inesistente!!!");
            }
            var provinciaVm = _mapper.Map<ProvinciaListVm>(provincia);
            return View(provinciaVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfim(int id)
        {
            var provincia = _servicios.GetProvinciaPorId(id);
            if (_servicios.EstaRelacionado(provincia))
            {
                var provinciaVm = _mapper.Map<ProvinciaListVm>(provincia);
                ModelState.AddModelError(string.Empty, "Provincia relacionada... Baja denegada!!!");
                return View(provinciaVm);

            }
            _servicios.Borrar(id);
            TempData["Msg"] = "Registro borrado satisfactoriamente!!!";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var provincia = _servicios.GetProvinciaPorId(id.Value);
            if (provincia == null)
            {
                return HttpNotFound("Código de provincia inesistente!!!");
            }
            var provinciaVm = _mapper.Map<ProvinciaEditVm>(provincia);
            return View(provinciaVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProvinciaEditVm provinciaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(provinciaVm);
            }
            var provincia = _mapper.Map<Provincia>(provinciaVm);
            if (_servicios.Existe(provincia))
            {
                ModelState.AddModelError(string.Empty, "Provincia existente!!!");
                return View(provinciaVm);
            }
            _servicios.Guardar(provincia);
            TempData["Msg"] = "Registro editado satisfactoriamente!!!";
            return RedirectToAction("Index");

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var provincia = _servicios.GetProvinciaPorId(id.Value);
            if (provincia == null)
            {
                return HttpNotFound("Código de provincia inesistente!!!");
            }
            var provinciaVm = _mapper.Map<ProvinciaListVm>(provincia);
            provinciaVm.CantidadCiudades = _serviciosCiudades
                .GetCantidad(c => c.ProvinciaId == provinciaVm.ProvinciaId);
            var provinciaDetailVm = new ProvinciaDetailVm()
            {
                Provincia = provinciaVm,
                Ciudades = _mapper.Map<List<CiudadListVm>>(
                _serviciosCiudades.GetCiudades(provincia.ProvinciaId))
            };
            return View(provinciaDetailVm);
        }

        public ActionResult AddCity(int? provinciaId)
        {
            if (provinciaId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var provincia = _servicios.GetProvinciaPorId(provinciaId.Value);
            if (provincia == null)
            {
                return HttpNotFound("Código de provincia inesistente!!!");
            }
            var ciudadVm = new CiudadEditVm()
            {
                Provincias = _servicios.GetProvinciasDropDownList()
            };
            return View(ciudadVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCity(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
                return View(ciudadVm);
            }
            try
            {
                var ciudad = _mapper.Map<Ciudad>(ciudadVm);
                if (!_serviciosCiudades.Existe(ciudad))
                {
                    _serviciosCiudades.Guardar(ciudad);
                    return RedirectToAction($"Details/{ciudad.ProvinciaId}");
                }
                else
                {
                    ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
                    ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {

                ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar agregar una Ciudad");
                return View(ciudadVm);

            }
        }

        public ActionResult DeleteCity(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var ciudad = _serviciosCiudades.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inexistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
            ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
            return View(ciudadVm);

        }
        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCityConfirm(int id)
        {
            var ciudad = _serviciosCiudades.GetCiudadPorId(id);
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
            try
            {
                if (!_serviciosCiudades.EstaRelacionada(ciudad))
                {
                    _serviciosCiudades.Borrar(id);
                    return RedirectToAction($"Details/{ciudad.ProvinciaId}");
                }
                else
                {
                    ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
                    ModelState.AddModelError(string.Empty, "Ciudad relacionada... Baja denegada");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {

                ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar borrar una ciudad");
                return View(ciudadVm);

            }
        }

        public ActionResult EditCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var ciudad = _serviciosCiudades.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inexistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
            ciudadVm.ProvinciaAnteriorId = ciudad.ProvinciaId;
            ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
            return View(ciudadVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCity(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
                return View(ciudadVm);

            }
            try
            {
                var ciudad = _mapper.Map<Ciudad>(ciudadVm);
                if (!_serviciosCiudades.Existe(ciudad))
                {
                    _serviciosCiudades.Guardar(ciudad);
                    return RedirectToAction($"Details/{ciudadVm.ProvinciaAnteriorId}");
                }
                else
                {
                    ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
                    ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                    return View(ciudadVm);

                }
            }
            catch (Exception)
            {

                ciudadVm.Provincias = _servicios.GetProvinciasDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar agregar una ciudad");
                return View(ciudadVm);
            }
        }

    }
}