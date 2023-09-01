using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.DetalleVenta
{
    public class DetalleVentaListVm
    {
        public int DetalleVentaId { get; set; }
        public string Repuesto { get; set; }

        [DisplayName("P. Unit.")]
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        [DisplayName("P. Total")]
        public decimal Subtotal => Cantidad * PrecioUnitario;

    }
}