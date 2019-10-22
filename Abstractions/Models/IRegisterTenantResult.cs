using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Models
{
    public interface IRegisterTenantResult
    {
        //  Properties
        //  ==========

        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}