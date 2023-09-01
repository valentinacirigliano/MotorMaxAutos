using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class ProveedorEntityTypeConfigurations:EntityTypeConfiguration<Proveedor>
    {
        public ProveedorEntityTypeConfigurations()
        {
            ToTable("Proveedores");
            Property(c => c.ProveedorId).HasColumnName("ProveedorId");
            Property(c => c.NombreProveedor).HasColumnName("NombreProveedor");
            Property(c => c.Direccion).HasColumnName("Direccion");
            Property(c => c.CiudadId).HasColumnName("CiudadId");
            Property(c => c.Telefono).HasColumnName("Telefono");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
