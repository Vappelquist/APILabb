using Microsoft.EntityFrameworkCore;

namespace APILabb.Models
{
    public class Context : DbContext
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        public DbSet<Interest> Interests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }


        public Context()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Interest)
                .WithMany()
                .HasForeignKey(c => c.InterestID);


            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Name = "Alice", PhoneNumber = "1234567890" },
                new User { ID = 2, Name = "Bob", PhoneNumber = "0987654321" },
                new User { ID = 3, Name = "Charlie", PhoneNumber = "5555555555" },
                new User { ID = 4, Name = "David", PhoneNumber = "1111111111" }
            );
            modelBuilder.Entity<Interest>().HasData(
                new Interest { ID = 1, InterestName = "Programming" },
                new Interest { ID = 2, InterestName = "Cooking" },
                new Interest { ID = 3, InterestName = "Traveling" },
                new Interest { ID = 4, InterestName = "Sports" }
            );
            
            modelBuilder.Entity<Connection>().HasData(
                new Connection { ID = 1, UserID = 1, InterestID = 1, URL = "https://www.youtube.com" },
                new Connection { ID = 2, UserID = 1, InterestID = 1, URL = "https://www.google.com" },
                new Connection { ID = 3, UserID = 1, InterestID = 2, URL = "https://www.google.com" },
                new Connection { ID = 4, UserID = 3, InterestID = 3, URL = "https://www.microsoft.com" },
                new Connection { ID = 5, UserID = 4, InterestID = 4, URL = "https://www.github.com" }
            );
        }


    }
}
