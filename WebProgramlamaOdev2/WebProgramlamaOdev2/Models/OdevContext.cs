using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace WebProgramlamaOdev2.Models
{
    public class OdevContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Login> login { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=WebOdev;Trusted_Connection=True;");
        }
    }
}
