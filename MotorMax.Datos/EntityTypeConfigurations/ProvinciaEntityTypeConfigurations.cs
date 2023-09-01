using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class ProvinciaEntityTypeConfigurations : EntityTypeConfiguration<Provincia>
    {
        public ProvinciaEntityTypeConfigurations()
        {
            ToTable("Provincias");
            Property(p => p.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
