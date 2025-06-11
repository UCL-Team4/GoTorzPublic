using GoTorz.Models.API;
using GoTorz.Models.Chat;
using GoTorz.Models.Booking;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoTorz.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        // The guids were created with guidgenerator.com since the migration requires hardcoded values
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER", Id = "1", ConcurrencyStamp = "246a32ee-49ad-4572-912d-83eabde94aa3" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Sales", NormalizedName = "SALES", Id = "2", ConcurrencyStamp = "d6e116bd-0efc-4952-b2b5-0c8c6c95a07a" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {Name = "Admin", NormalizedName = "ADMIN", Id = "3", ConcurrencyStamp = "a8b123af-7287-403d-9267-3633dbc4cdf1" });
        }
    }
}
