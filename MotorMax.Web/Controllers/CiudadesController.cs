using AutoMapper;
using MotorMax.Entidades.Dto.Ciudad;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using MotorMax.Servicios.Servicios;
using MotorMax.Web.App_Start;
using MotorMax.Web.ViewModels.Ciudad;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MotorMax.Web.Controllers
{
    public class CiudadesController : Controller
    {
        // GET: Ciudades
        private readonly IServiciosCiudades _servicio;
        private readonly IServiciosProvincias _serviciosProvincias;
        private readonly IMapper _mapper;
        public CiudadesController(IServiciosCiudades servicio, IServiciosProvincias serviciosProvincias)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosProvincias = serviciosProvincias;

        }
        public ActionResult Index(int? ProvinciaFiltro, int? page, int? pageSize, string SearchBy = null)
        {
            List<CiudadListDto> lista;
            if (ProvinciaFiltro == null)
            {
                lista = _servicio.GetCiudades();

            }
            else
            {
                lista = _servicio.GetCiudades(ProvinciaFiltro.Value);
            }
            if (SearchBy != null)
            {
                lista = lista
                    .Where(c => c.Nombre.Contains(SearchBy))
                    .ToList();
            }

            page = page ?? 1;
            pageSize = pageSize ?? 8;

            var listaVm = _mapper.Map<List<CiudadListVm>>(lista);
            var ciudadVm = new CiudadFiltroVm
            {
                ProvinciaFiltro = ProvinciaFiltro,
                Provincias = _serviciosProvincias.GetProvinciasDropDownList(),
                Ciudades = listaVm.ToPagedList(page.Value, pageSize.Value)
            };
            return View(ciudadVm);
        }

        public ActionResult Create()
        {
            var ciudadVm = new CiudadEditVm()
            {
                Provincias = _serviciosProvincias.GetProvinciasDropDownList(),
            };
            return View(ciudadVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                return View(ciudadVm);
            }
            var ciudad = _mapper.Map<Ciudad>(ciudadVm);
            if (_servicio.Existe(ciudad))
            {

                ciudadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();

                ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                return View(ciudadVm);
            }
            _servicio.Guardar(ciudad);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ciudad = _servicio.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inexistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadListVm>(ciudad);
            return View(ciudadVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var ciudad = _servicio.GetCiudadPorId(id);
            var ciudadVm = _mapper.Map<CiudadListVm>(ciudad);
            try
            {
                if (!_servicio.EstaRelacionada(ciudad))
                {
                    _servicio.Borrar(ciudad.CiudadId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ciudad relacionada... Baja denegada!!");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de ciudades");
                return View(ciudadVm);

            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ciudad = _servicio.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inexistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);


            ciudadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
            return View(ciudadVm);

        }
        [HttpPost]
        public ActionResult Edit(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                return View(ciudadVm);
            }
            try
            {
                var ciudad = _mapper.Map<Ciudad>(ciudadVm);
                if (!_servicio.Existe(ciudad))
                {
                    _servicio.Guardar(ciudad);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ciudadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();

                    ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {
                ciudadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();

                ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                return View(ciudadVm);
            }
        }
    }
}