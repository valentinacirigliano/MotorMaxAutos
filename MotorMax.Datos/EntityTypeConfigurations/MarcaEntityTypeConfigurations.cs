using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class MarcasEntityTypeConfigurations : EntityTypeConfiguration<Marca>
    {
        public MarcasEntityTypeConfigurations()
        {
            ToTable("Marcas");
            Property(c => c.MarcaId).HasColumnName("MarcaId");
            Property(c => c.NombreMarca).HasColumnName("NombreMarca");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
