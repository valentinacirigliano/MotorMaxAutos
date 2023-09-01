using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Entidades
{
    public class Proveedor
    {
        public Proveedor()
        {
            Repuestos = new HashSet<RepuestoProveedor>();
        }

        [Key]
        public int ProveedorId { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreProveedor { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required]
        public int CiudadId { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }
        public Ciudad Ciudad { get; set; }

        public virtual ICollection<RepuestoProveedor> Repuestos { get; set; }

    }
}
