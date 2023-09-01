using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.EntityTypeConfigurations
{
    public class CategoriaEntityTypeConfigurations:EntityTypeConfiguration<Categoria>
    {
        public CategoriaEntityTypeConfigurations()
        {
            ToTable("Categorias");
            HasKey(c => c.CategoriaId);
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
