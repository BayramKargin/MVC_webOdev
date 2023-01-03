using Microsoft.AspNetCore.Identity;

namespace WebProgramlamaOdev2.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
