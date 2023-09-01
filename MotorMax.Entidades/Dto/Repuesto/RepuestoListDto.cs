using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Dto.Repuesto
{
    public class RepuestoListDto
    {
        public int RepuestoId { get; set; }
        [DisplayName("Artículo")]
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        [DisplayName("P.Unit.")]
        public decimal PrecioUnitario { get; set; }
        [DisplayName("U. Disp.")]
        public int UnidadesDisponibles { get; set; }
        [DisplayName("Suspendido")]
        public bool Suspendido { get; set; }
        public string Imagen { get; set; }
    }
}
