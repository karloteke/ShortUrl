using Microsoft.EntityFrameworkCore;
using ShortUrl.Models;
using Microsoft.Extensions.Logging;

namespace ShortUrl.Data
{
    public class ShortUrlContext : DbContext
    {

        public ShortUrlContext(DbContextOptions<ShortUrlContext> options)
            : base(options)
        {

        }

        
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llamamos al método base para asegurarnos de que se ejecute cualquier lógica definida allí.
            base.OnModelCreating(modelBuilder);

            // Agregar usuarios User/Admin
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Carlota",
                    Password = "Carlota36", 
                    Email = "carlota@clinicare.com",
                    Role = Roles.Admin
                },
                new User
                {
                    Id = 2,
                    UserName = "Jesus",
                    Password = "Jesus30", 
                    Email = "jesus@clinicare.com",
                    Role = Roles.Admin
                },
                new User
                {
                    Id = 3,
                    UserName = "Paola",
                    Password = "Paola30", 
                    Email = "paola@gmail.com",
                    Role = Roles.User
                },
                new User
                {
                    Id = 4,
                    UserName = "Nerea",
                    Password = "Nerea30", 
                    Email = "nerea@gmail.com",
                    Role = Roles.User
                }
            );
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(); // Opcional
        }
        public DbSet<User> Users { get; set; }
       
    }
}

