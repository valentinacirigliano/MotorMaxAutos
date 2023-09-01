using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Venta
{
    public class VentaListVm
    {
        [DisplayName("Vta. Nro.")]
        public int VentaId { get; set; }
        [DisplayName("Fec. Vta.")]
        public DateTime FechaOperacion { get; set; }
        public string Cliente { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string Fecha { get; set; }
    }
}