using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class RepuestoEntityTypeConfigurations:EntityTypeConfiguration<Repuesto>
    {
        public RepuestoEntityTypeConfigurations()
        {
            ToTable("Repuestos");
            Property(c => c.RepuestoId).HasColumnName("RepuestoId");
            Property(c => c.Descripcion).HasColumnName("Descripcion");
            Property(c => c.PrecioUnitario).HasColumnName("PrecioUnitario");
            Property(c => c.Stock).HasColumnName("Stock");
            Property(c => c.Suspendido).HasColumnName("Suspendido");
            Property(c => c.Imagen).HasColumnName("Imagen");
            Property(c => c.UnidadesEnPedido).HasColumnName("UnidadesEnPedido");
            HasKey(c => c.RepuestoId);
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    
    }
}
