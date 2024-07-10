using PruebaTecnicaAlerium.Models;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaAlerium.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proveedor>()
                .HasIndex(p => new { p.RazonSocial,p.Codigo})
                .IsUnique();

            modelBuilder.Entity<Producto>()
                .HasIndex(p => new { p.IdProveedor, p.Codigo })
                .IsUnique();

            modelBuilder.Entity<Proveedor>()
                .Property(p => p.RFC)
                .HasMaxLength(13)
                .IsFixedLength();
        }
    }

}
