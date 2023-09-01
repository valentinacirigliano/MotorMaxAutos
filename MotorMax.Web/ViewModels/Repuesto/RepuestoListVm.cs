using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Repuesto
{
    public class RepuestoListVm
    {
        public int RepuestoId { get; set; }
        [DisplayName("Repuesto")]
        public string Descripcion { get; set; }
        [DisplayName("Categoría")]
        public string Categoria { get; set; }

        [DisplayName("P. Unit")]
        public decimal PrecioUnitario { get; set; }
        [DisplayName("Stock")]
        public int UnidadesDisponibles { get; set; }
        public bool Suspendido { get; set; }
        [DisplayName("Imágen")]
        public string Imagen { get; set; }
    }
}