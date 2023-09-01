using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.ViewModels.Repuesto
{
    public class RepuestoFiltroVm
    {
        public List<SelectListItem> Categorias { get; set; }
        public IPagedList<RepuestoListVm> Repuestos { get; set; }
        public int? CategoriaFiltro { get; set; }
    }
}