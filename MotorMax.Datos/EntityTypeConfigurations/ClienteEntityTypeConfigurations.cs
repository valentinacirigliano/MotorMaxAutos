using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class ClienteEntityTypeConfigurations : EntityTypeConfiguration<Cliente>
    {
        public ClienteEntityTypeConfigurations()
        {
            ToTable("Clientes");
            Property(c => c.ClienteId).HasColumnName("ClienteId");
            Property(c => c.NombreApellido).HasColumnName("NombreApellido");
            Property(c => c.Direccion).HasColumnName("Dirección");
            Property(c => c.CiudadId).HasColumnName("CiudadId");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }

    }
}
