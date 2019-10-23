using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Abstractions.Models
{
    public class ApplicationUser : IdentityUser
    {
        //  Properties
        //  ==========

        public IEnumerable<House> Houses { get; set; } = null!;
    }
}