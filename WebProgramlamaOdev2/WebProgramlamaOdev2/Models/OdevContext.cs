using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebProgramlamaOdev2.Models;

namespace WebProgramlamaOdev2.Models
{
    public class OdevContext : DbContext
    {

        
        public DbSet<User> users { get; set; }
        public DbSet<Urunler> Urunler { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=WebProje;Username=postgres;Password=12345");
        }
        public DbSet<WebProgramlamaOdev2.Models.RegisterModel> RegisterModel { get; set; }
        public DbSet<WebProgramlamaOdev2.Models.Login> Login { get; set; }


    }
}
