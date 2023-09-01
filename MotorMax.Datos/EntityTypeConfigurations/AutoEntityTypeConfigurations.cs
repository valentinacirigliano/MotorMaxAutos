using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class AutoEntityTypeConfigurations:EntityTypeConfiguration<Auto>
    {
        public AutoEntityTypeConfigurations()
        {
            ToTable("Autos");
            HasKey(a => a.AutoId);
            Property(c => c.Patente).HasColumnName("Patente");
            Property(c => c.Modelo).HasColumnName("Modelo");
            Property(c => c.MarcaId).HasColumnName("MarcaId");
            Property(c => c.PrecioFinal).HasColumnName("PrecioFinal");
            Property(c => c.Stock).HasColumnName("Stock");
            Property(c => c.UnidadesEnPedido).HasColumnName("UnidadesEnPedido");
            Property(c => c.Suspendido).HasColumnName("Suspendido");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
