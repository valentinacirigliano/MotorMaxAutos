using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorMax.Entidades.Entidades
{
    [Table("Autos")]
    public partial class Auto
    {
        [Key]
        public int AutoId { get; set; }

        [Required]
        [StringLength(7)]
        public string Patente { get; set; }

        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }

        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        public decimal PrecioFinal { get; set; }

        public int Stock { get; set; }

        public int UnidadesEnPedido { get; set; }

        public bool Suspendido { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
