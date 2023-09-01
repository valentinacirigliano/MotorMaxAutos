using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.ViewModels.RepuestoProveedor
{
    public class RepuestoProveedorEditVm
    {
        public int RepuestoId { get; set; }
        public int ProveedorId { get; set; }
        public List<SelectListItem> Proveedores { get; set; }
        public List<SelectListItem> Repuestos { get; set; }
        public byte[] RowVersion { get; set; }
    }
}