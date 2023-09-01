using AutoMapper;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using MotorMax.Web.App_Start;
using MotorMax.Web.ViewModels.Marca;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MotorMax.Web.Controllers
{
    public class MarcasController : Controller
    {
        IServiciosMarcas _servicios;
        private readonly IMapper _mapper;
        public MarcasController(IServiciosMarcas servicio)
        {
            _servicios = servicio;
            _mapper = AutoMapperConfig.Mapper;
        }
        // GET: Marcas
        public ActionResult Index()
        {
            var lista = _servicios.GetMarcas();
            var listaVm = _mapper.Map<List<MarcaListVm>>(lista);
            return View(listaVm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(MarcaEditVm marcaVm)
        {
            var marca = _mapper.Map<Marca>(marcaVm);
            if (ModelState.IsValid)
            {
                if (_servicios.Existe(marca))
                {
                    ModelState.AddModelError(string.Empty, "Marca existente");
                    return View(marcaVm);
                }
                _servicios.Guardar(marca);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index"); 
            }
            else
            {
                return View(marcaVm);
            }
        }


        public ActionResult Delete(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var marca = _servicios.GetMarcaPorId(id.Value);
            if (marca == null)
            {
                return HttpNotFound("Código de marca inexistente");
            }
            var marcaVm=_mapper.Map<MarcaListVm>(marca);
            return View(marcaVm);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfim(int id)
        {
            var marca = _servicios.GetMarcaPorId(id);
            if (_servicios.EstaRelacionado(marca))
            {
                var marcaVm = _mapper.Map<MarcaListVm>(marca);
                ModelState.AddModelError(string.Empty, "Marca relacionada. . . Baja denegada");
                return View(marcaVm);
            }
            _servicios.Borrar(id);
            TempData["Msg"] = "Registro borrado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var marca = _servicios.GetMarcaPorId(id.Value);
            if (marca == null)
            {
                return HttpNotFound("Código de marca inexistente");
            }
            var marcaVm = _mapper.Map<MarcaEditVm>(marca);
            return View(marcaVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MarcaEditVm marcaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(marcaVm);
            }
            var marca = _mapper.Map<Marca>(marcaVm);
            if (_servicios.Existe(marca))
            {
                ModelState.AddModelError(string.Empty, "Marca existente");
                return View(marcaVm);
            }
            _servicios.Guardar(marca);
            TempData["Msg"] = "Registro editado satisfactoriamente";
            return RedirectToAction("Index");
        }

    }
}