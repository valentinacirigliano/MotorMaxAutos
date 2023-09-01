using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class DetalleVentasEntityTypeConfigurations : EntityTypeConfiguration<DetalleVenta>
    {
        public DetalleVentasEntityTypeConfigurations()
        {
            ToTable("DetalleVenta");
            HasKey(c => c.DetalleVentaId);
            Property(c => c.DetalleVentaId).HasColumnName("DetalleVentaId");
            Property(c => c.VentaId).HasColumnName("VentaId");
            Property(c => c.RepuestoId).HasColumnName("RepuestoId");
            Property(c => c.PrecioUnitario).HasColumnName("PrecioUnitario");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
