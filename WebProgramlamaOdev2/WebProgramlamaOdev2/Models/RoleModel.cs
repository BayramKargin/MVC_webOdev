﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebProgramlamaOdev2.Identity;

namespace WebProgramlamaOdev2.Models
{
    public class RoleModel
    {
        [Required]
        public string? Name { get; set; }
        
        public class RoleDetails
        {
            public IdentityRole Role { get; set; }
            public List<User> Members { get; set; }
            public List<User> NoMembers { get; set; }
        }

        public class RoleEditModel
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public string[]? IdsToAdd { get; set; }
            public string[]? IdsToDelete { get; set; }
        }
    }
}
