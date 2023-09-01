using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class CiudadEntityTypeConfigurations : EntityTypeConfiguration<Ciudad>
    {
        public CiudadEntityTypeConfigurations()
        {
            ToTable("Ciudades");
            Property(c=>c.Nombre).HasColumnName("Nombre");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();

        }
    }
}
