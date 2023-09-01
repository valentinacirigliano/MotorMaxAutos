using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.ViewModels.Auto
{
    public class AutoFiltroVm
    {
        public List<SelectListItem> Marcas { get; set; }
        public IPagedList<AutoListVm> Autos { get; set; }
        public int? MarcaFiltro { get; set; }
    }
}