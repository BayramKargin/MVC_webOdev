using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebProgramlamaOdev2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        

    }
}
