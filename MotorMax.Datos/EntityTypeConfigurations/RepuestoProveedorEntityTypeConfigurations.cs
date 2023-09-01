using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class RepuestoProveedorEntityTypeConfigurations:EntityTypeConfiguration<RepuestoProveedor>
    {
        public RepuestoProveedorEntityTypeConfigurations()
        {
            ToTable("RepuestosProveedores");
            Property(c => c.RepuestoId).HasColumnName("RepuestoId");
            Property(c => c.ProveedorId).HasColumnName("ProveedorId");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();

        }
    }
}
