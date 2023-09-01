using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Entidades
{
    [Table("Repuestos")]
    public class Repuesto
    {
        public Repuesto()
        {
            RepuestoProveedor = new HashSet<RepuestoProveedor>();
        }
        [Key]
        public int RepuestoId { get; set; }
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public bool Suspendido { get; set; }
        public string Imagen { get; set; }
        public int UnidadesEnPedido { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }
        public int UnidadesDisponibles() => Stock - UnidadesEnPedido;
        public Categoria Categoria { get; set; }
        public virtual ICollection<RepuestoProveedor> RepuestoProveedor { get; set; }
    }
}
