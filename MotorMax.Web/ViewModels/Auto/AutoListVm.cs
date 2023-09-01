using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Auto
{
    public class AutoListVm
    {
        public int AutoId { get; set; }
        [DisplayName("Patente")]
        public string Patente { get; set; }
        [DisplayName("Marca")]
        public string Marca { get; set; }
        [DisplayName("Modelo")]
        public string Modelo { get; set; }
        [DisplayName("Precio")]
        public decimal PrecioFinal { get; set; }
        [DisplayName("Stock")]
        public int UnidadesDisponibles { get; set; }
        [DisplayName("Suspendido")]
        public bool Suspendido { get; set; }
    }
}