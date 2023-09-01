using AutoMapper;
using MotorMax.Entidades.Dto.Auto;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using MotorMax.Web.App_Start;
using MotorMax.Web.ViewModels.Auto;
using MotorMax.Web.ViewModels.Marca;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MotorMax.Web.Controllers
{
    public class AutosController : Controller
    {
        private readonly IServiciosAutos _servicios;
        private readonly IServiciosMarcas _serviciosMarcas;
        private readonly IMapper _mapper;
        public AutosController(IServiciosAutos serviciosAutos, IServiciosMarcas serviciosMarcas)
        {
            _servicios = serviciosAutos;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosMarcas = serviciosMarcas;

        }
        // GET: Autos
        public ActionResult Index(int? MarcaFiltro, int? page, int? pageSize)
        {
            List<AutoListDto> lista;
            if (User.IsInRole("Comprador"))
            {
                if (MarcaFiltro == null)
                {
                    Func<Auto, bool> predicado = p => (p.UnidadesEnPedido > 0 || p.Suspendido == false);
                    lista = _servicios.Filtrar(predicado);

                }
                else
                {
                    Func<Auto, bool> predicado = p => (p.UnidadesEnPedido > 0 && p.Suspendido == false) && p.MarcaId == MarcaFiltro.Value;
                    lista = _servicios.Filtrar(predicado);

                }
            }
            else
            {
                Func<Auto, bool> predicado = p => p.Suspendido==true || p.Suspendido == false;

                lista = _servicios.Filtrar(predicado);

            }

            var listaVm = _mapper.Map<List<AutoListVm>>(lista);
            page = page ?? 1;
            pageSize = pageSize ?? 8;

            var autoVm = new AutoFiltroVm
            {
                MarcaFiltro = MarcaFiltro,
                Marcas = _serviciosMarcas.GetMarcasDropDownList(),
                Autos = listaVm.ToPagedList(page.Value, pageSize.Value)
            };
            return View(autoVm);

        }


        public ActionResult Create()
        {
            var autoVm = new AutoEditVm
            {
                Marcas = _serviciosMarcas.GetMarcasDropDownList()

            };
            return View(autoVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AutoEditVm autoVm)
        {
            if (!ModelState.IsValid)
            {
                autoVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                return View(autoVm);
            }
            try
            {
                var producto = _mapper.Map<Auto>(autoVm);
                if (!_servicios.Existe(producto))
                {
                    
                    _servicios.Guardar(producto);
                    TempData["Msg"] = "Registro agregado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Auto existente!!!");
                    autoVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();

                    return View(autoVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Auto");
                autoVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();

                return View(autoVm);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var auto = _servicios.GetAutoPorId(id.Value);
            if (auto == null)
            {
                return HttpNotFound("Cód. de auto inexistente!!!");
            }
            var autoVm = _mapper.Map<AutoEditVm>(auto);
            autoVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
            return View(autoVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AutoEditVm autoVm)
        {
            if (!ModelState.IsValid)
            {
                autoVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                return View(autoVm);

            }
            try
            {
                var auto = _mapper.Map<Auto>(autoVm);
                if (!_servicios.Existe(auto))
                {
                    _servicios.Guardar(auto);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Auto existente!!!");
                   autoVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                    return View(autoVm);

                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Auto existente!!!");
                autoVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                return View(autoVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var producto = _servicios.GetAutoPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Cód. producto inexistente!!!");
            }
            var autoVm = _mapper.Map<AutoListVm>(producto);
            return View(autoVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var producto = _servicios.GetAutoPorId(id);
            var autoVm = _mapper.Map<AutoListVm>(producto);
            try
            {
                if (!_servicios.EstaRelacionado(producto))
                {
                    _servicios.Borrar(id);
                    TempData["Msg"] = "Auto borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registro relacionado... Baja denegada");
                    return View(autoVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intengar borrar un proveedor");
                return View(autoVm);

            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var auto = _servicios.GetAutoPorId(id.Value);
            if (auto == null)
            {
                return HttpNotFound("Cód. auto inexistente!!!");
            }
            var autoVm = _mapper.Map<AutoListVm>(auto);
            return View(autoVm);
        }
    }
}