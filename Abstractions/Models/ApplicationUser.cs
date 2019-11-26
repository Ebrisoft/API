using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Abstractions.Models
{
    public class ApplicationUser : IdentityUser
    {
        //  Properties
        //  ==========

        public House? House { get; set; } = null;
        public IEnumerable<House> Houses { get; set; } = null!;

        public IEnumerable<Issue> Issues { get; set; } = null!;
    }
}