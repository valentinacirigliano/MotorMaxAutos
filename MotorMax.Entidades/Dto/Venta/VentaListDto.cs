using MotorMax.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Dto.Venta
{
    public class VentaListDto
    {
        public int VentaId { get; set; }
        public DateTime FechaOperacion { get; set; }
        public string Cliente { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string Fecha() => FechaOperacion.ToShortDateString();

    }
}
