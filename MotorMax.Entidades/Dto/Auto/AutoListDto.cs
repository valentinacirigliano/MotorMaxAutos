using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Dto.Auto
{
    public class AutoListDto
    {
        public int AutoId { get; set; } 
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal PrecioFinal { get; set; }
        public int UnidadesDisponibles { get; set; }
        public bool Suspendido { get; set; }
    }
}
