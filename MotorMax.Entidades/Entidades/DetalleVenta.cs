using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Entidades
{
    [Table("DetalleVenta")]
    public partial class DetalleVenta
    {
        [Key]
        public int DetalleVentaId { get; set; }

        public int VentaId { get; set; }

        [Required]
        public int RepuestoId { get; set; }
        public Repuesto Repuesto { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
