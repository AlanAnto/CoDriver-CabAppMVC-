using CabApp.Models;

namespace CabApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BookingCab>()
                .HasOne(m=>m.User)
                .WithMany(m=>m.BookingCabs)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<BookingCab> Bookings { get; set; }

        public DbSet<CabDriver> CabDrivers { get; set; }


    }
}
