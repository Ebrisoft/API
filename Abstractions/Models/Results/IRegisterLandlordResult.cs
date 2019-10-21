using System.Collections.Generic;

namespace Abstractions.Models.Results
{
    public interface IRegisterLandlordResult
    {
        //  Properties
        //  ==========

        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}