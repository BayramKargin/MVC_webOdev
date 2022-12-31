using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebProgramlamaOdev2.Models
{
    public class Login
    {
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(5)]
        public string Password { get; set; }
    }
}
