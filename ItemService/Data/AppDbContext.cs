using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Item> Itens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Restaurante>()
                .HasMany(c => c.Itens)
                .WithOne(i => i.Restaurante!);

            modelBuilder
                .Entity<Item>()
                .HasOne(i => i.Restaurante)
                .WithMany(r => r.Itens)
                .HasForeignKey(i => i.IdRestaurante);
        }
    }
}