using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Entidades
{
    public class ItemCarrito
    {
        [Key]
        public int ItemCarritoId { get; set; }
        public int RepuestoId { get; set; }
        public string UserName { get; set; }
        public string Repuesto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal => Cantidad * PrecioUnitario;
    }
}
