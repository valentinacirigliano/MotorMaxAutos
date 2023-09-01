using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.ViewModels.Ciudad
{
    public class CiudadFiltroVm
    {
        public List<SelectListItem> Provincias { get; set; }
        public IPagedList<CiudadListVm> Ciudades { get; set; }
        public int? ProvinciaFiltro { get; set; }
    }
}