using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Requests.Tenant
{
    public class Register
    {
        //  Properties
        //  ==========

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}