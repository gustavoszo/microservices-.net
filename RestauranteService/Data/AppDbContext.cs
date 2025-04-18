using Microsoft.EntityFrameworkCore;
using RestauranteService.Models;

namespace RestauranteService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; }
}
