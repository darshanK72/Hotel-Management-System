using Microsoft.EntityFrameworkCore;

namespace HMS_API.Models
{
    public class HMSDbContext : DbContext
    {
        public HMSDbContext(DbContextOptions<HMSDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Rate> Rate { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, UserRole = "Owner", Description = "Hotel owner" },
                new Role { RoleId = 2, UserRole = "Manager", Description = "Hotel manager" },
                new Role { RoleId = 3, UserRole = "Receptionist", Description = "Front desk staff" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "owner", Password = "owner123", Email = "admin@example.com", RoleId = 1 },
                new User { UserId = 2, Username = "manager", Password = "manager123", Email = "manager@example.com", RoleId = 2 }
            );
        }

    }


}
