using MotorMax.Web.ViewModels.Ciudad;
using MotorMax.Web.ViewModels.Proveedor;
using MotorMax.Web.ViewModels.Provincia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Repuesto
{
    public class RepuestoDetailVm
    {
        public RepuestoListVm Repuesto { get; set; }
        public List<ProveedorListVm> Proveedores { get; set; }
    }
}