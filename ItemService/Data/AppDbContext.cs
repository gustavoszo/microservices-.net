using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Item> Itens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Restaurant>()
                .HasMany(c => c.Itens)
                .WithOne(i => i.Restaurant!);

            modelBuilder
                .Entity<Item>()
                .HasOne(i => i.Restaurant)
                .WithMany(r => r.Itens)
                .HasForeignKey(i => i.IdRestaurant);
        }
    }
}