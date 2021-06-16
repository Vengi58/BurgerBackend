using BurgerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerBackend.Data
{
    public class BurgerDBContext : DbContext
    {
        public BurgerDBContext(DbContextOptions<BurgerDBContext> options) : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Hours> Hours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasKey(r => r.Name);

            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Hours)
                .WithOne(h => h.Restaurant)
                .HasForeignKey<Hours>(h => h.RestaurantName);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.RestaurantName);
        }
    }
}
