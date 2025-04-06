using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Laboratorio1.Models;

namespace Laboratorio1.Models
{
    public class ERPDbContext : IdentityDbContext<IdentityUser>
    {
        public ERPDbContext(DbContextOptions<ERPDbContext> options) : base(options)
        {

        }
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }
		public DbSet<Proveedor> Proveedores { get; set; }
		public DbSet<Producto> Productos { get; set; }
		public DbSet<Lote> Lotes { get; set; }
		public DbSet<Movimientos_Inventario> MovimientoInventarios { get; set; }
		public DbSet<Inventario> Inventario { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Evitar múltiples cascadas desde tbl_productos
			modelBuilder.Entity<Inventario>()
			.HasOne(i => i.Producto)
			.WithMany()
			.HasForeignKey(i => i.ProductoId)
			.OnDelete(DeleteBehavior.Restrict); // o .NoAction

			modelBuilder.Entity<Movimientos_Inventario>()
			.HasOne(m => m.Producto)
			.WithMany()
			.HasForeignKey(m => m.ProductoId)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Lote>()
			.HasOne(l => l.Producto)
			.WithMany()
			.HasForeignKey(l => l.ProductoId)
			.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
