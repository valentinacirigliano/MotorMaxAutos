using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class VentaEntityTypeConfigurations : EntityTypeConfiguration<Venta>
    {
        public VentaEntityTypeConfigurations()
        {
            ToTable("Ventas");
            Property(c => c.VentaId).HasColumnName("VentaId");
            Property(c => c.ClienteId).HasColumnName("ClienteId");
            Property(c => c.FechaOperacion).HasColumnName("FechaOperación");
            Property(c => c.Monto).HasColumnName("Monto");
            Property(c => c.Estado).HasColumnName("Estado");
            Property(c => c.TransactionId).HasColumnName("TransactionId");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
