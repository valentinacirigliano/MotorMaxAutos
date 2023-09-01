using AutoMapper;
using MotorMax.Servicios.Interfaces;
using MotorMax.Web.App_Start;
using MotorMax.Web.ViewModels.Ciudad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.Controllers
{
    public class GenericoController : Controller
    {
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IMapper _mapper;
        public GenericoController(IServiciosCiudades serviciosCiudades)
        {
            _serviciosCiudades = serviciosCiudades;
            _mapper = AutoMapperConfig.Mapper;
        }
        public JsonResult GetCities(int paisId)
        {
            var lista = _serviciosCiudades.GetCiudades(paisId);
            var ciudadesVm = _mapper.Map<List<CiudadListVm>>(lista);
            return Json(ciudadesVm);
        }
    }
}