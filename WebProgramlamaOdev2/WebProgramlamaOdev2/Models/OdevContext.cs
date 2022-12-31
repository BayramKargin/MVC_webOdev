using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace WebProgramlamaOdev2.Models
{
    public class OdevContext : DbContext
    {

        
        public DbSet<User> users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=WebProje;Username=postgres;Password=12345");
        }

    }
}
