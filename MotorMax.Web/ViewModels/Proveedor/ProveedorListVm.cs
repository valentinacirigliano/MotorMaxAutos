using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Proveedor
{
    public class ProveedorListVm
    {
        public int ProveedorId { get; set; }
        [DisplayName("Proveedor")]
        public string NombreProveedor { get; set; }
        public string Ciudad { get; set; }
        [DisplayName("Cantidad de Repuestos")]
        public int CantidadRepuestos { get; set; }
    }
}