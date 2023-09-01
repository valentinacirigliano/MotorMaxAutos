using MotorMax.Web.ViewModels.DetalleVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Venta
{
    public class VentaDetailVm
    {
        public VentaListVm Venta { get; set; }
        public List<DetalleVentaListVm> Detalles { get; set; }
    }
}