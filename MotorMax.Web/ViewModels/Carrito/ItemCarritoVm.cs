using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Carrito
{
    public class ItemCarritoVm
    {
        public int RepuestoId { get; set; }

        [DisplayName("Repuesto")]
        public string Repuesto { get; set; }
        public int Cantidad { get; set; }

        [DisplayName("P. Unit.")]
        public decimal PrecioUnitario { get; set; }

        [DisplayName("P. Total")]
        public decimal PrecioTotal => Cantidad * PrecioUnitario;
    }
}