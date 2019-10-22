using Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class RegisterTenantResult : IRegisterTenantResult
    {
        //  Properties
        //  ==========

        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}