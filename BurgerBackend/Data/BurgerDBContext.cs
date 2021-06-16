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

            //modelBuilder.ApplyConfiguration(new RestaurantConfiguration());

            // configures one-to-many relationship
            //modelBuilder.Entity<Student>()
            //    .HasRequired<Grade>(s => s.CurrentGrade)
            //    .WithMany(g => g.Students)
            //    .HasForeignKey<int>(s => s.CurrentGradeId);
        }
    }

    //public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    //{
    //    public void Configure(EntityTypeBuilder<Restaurant> builder)
    //    {
    //        builder.HasKey(c => c.RestaurantID);
    //        builder.Property(c => c.Name)
    //            .IsRequired()
    //            .HasMaxLength(100);
    //        builder.HasMany(e => e.Reviews).WithMany( e => e.re;
    //    }
    //}
    //public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    //{
    //    public void Configure(EntityTypeBuilder<Review> builder)
    //    {
    //        builder.HasKey(c => c.ReviewID);
    //        builder.Property(c => c.Taste)
    //            .IsRequired();
    //        builder.Property(c => c.Texture)
    //            .IsRequired();
    //        builder.Property(c => c.VisualRepresentation)
    //            .IsRequired();
    //        builder.HasKey(k => k.RestaurantID).
    //    }
    //}
}
