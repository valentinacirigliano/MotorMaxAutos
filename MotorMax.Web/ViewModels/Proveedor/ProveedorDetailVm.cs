using MotorMax.Web.ViewModels.Ciudad;
using MotorMax.Web.ViewModels.Provincia;
using MotorMax.Web.ViewModels.Repuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Proveedor
{
    public class ProveedorDetailVm
    {
        public ProveedorListVm Proveedor { get; set; }
        public List<RepuestoListVm> Repuestos { get; set; }
    }
}