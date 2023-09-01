using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorMax.Entidades.Entidades
{
    public partial class Marca
    {
        public int MarcaId { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreMarca { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }
    }
}
