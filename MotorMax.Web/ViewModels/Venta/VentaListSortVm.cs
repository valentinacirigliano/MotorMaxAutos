using MotorMax.Web.ViewModels.Proveedor;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Venta
{
    public class VentaListSortVm
    {
        public IPagedList<VentaListVm> Ventas { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, string> Sorts { get; set; }
        public string SearchBy { get; set; }
    }
}