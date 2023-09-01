using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorMax.Entidades.Enums;

namespace MotorMax.Entidades.Entidades
{
    public partial class Venta
    {
        [Key]
        public int VentaId { get; set; }
        
        public int ClienteId { get; set; }


        public DateTime FechaOperacion { get; set; }

        [Column(TypeName = "money")]
        public decimal Monto { get; set; }


        public Estado Estado { get; set; }
        public string TransactionId { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }
        public List<DetalleVenta> Detalles { get; set; }

        public Venta()
        {
            Detalles = new List<DetalleVenta>();
        }
        public Cliente Cliente { get; set; }

    }
}
