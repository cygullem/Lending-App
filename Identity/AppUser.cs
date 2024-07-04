using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CyMvc.Identity
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? Age { get; set; }
        public string? Address { get; set; }
    }
}