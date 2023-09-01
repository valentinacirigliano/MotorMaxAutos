using MotorMax.Entidades.Entidades;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace MotorMax.Datos
{
    public partial class AutosDbContext : DbContext
    {
        public AutosDbContext()
            : base("name=AutosDbContext")
        {
        }

        public virtual DbSet<Auto> Autos { get; set; }
        public virtual DbSet<ItemCarrito> Carrito { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Ciudad> Ciudades { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<Provincia> Provincias { get; set; }
        public virtual DbSet<Repuesto> Repuestos { get; set; }
        public virtual DbSet<RepuestoProveedor> RepuestosProveedores { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AutosDbContext>(null);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Auto>()
                .ToTable("Autos")
                .Property(e => e.RowVersion)
                .IsFixedLength();


            modelBuilder.Entity<Ciudad>()
                .ToTable("Ciudades")
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Categoria>()
                .ToTable("Categorias")
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Cliente>()
                .ToTable("Clientes")
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Ventas)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleVenta>()
                .ToTable("DetalleVenta")
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Marca>()
                .ToTable("Marcas")
                .Property(e => e.NombreMarca)
                .IsUnicode(false);

            modelBuilder.Entity<Marca>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Provincia>()
                .ToTable("Provincias")
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Provincia>()
                .HasMany(e => e.Ciudades)
                .WithRequired(e => e.Provincia)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedores")
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Repuesto>()
                .ToTable("Repuestos")
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<RepuestoProveedor>()
                .ToTable("RepuestosProveedores")
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<Venta>()
                .ToTable("Ventas")
                .Property(e => e.Monto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Venta>()
                .Property(e => e.RowVersion)
                .IsFixedLength();
        }
    }
}
