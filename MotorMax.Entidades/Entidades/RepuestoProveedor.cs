using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Entidades
{
    [Table("RepuestosProveedores")]
    public class RepuestoProveedor
    {
        
        [Key]
        [Column("RepuestoId", Order = 0)]
        public int RepuestoId { get; set; }
        [Key]
        [Column("ProveedorId", Order = 1)]
        public int ProveedorId { get; set; }
        public virtual Repuesto Repuesto { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        
        public byte[] RowVersion { get; set; }
    }
}
