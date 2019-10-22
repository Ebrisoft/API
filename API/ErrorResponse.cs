using System.Collections.Generic;
using System.Linq;

namespace API
{
    public class ErrorResponse
    {
        //  Properties
        //  ==========

        public IEnumerable<string> Errors { get; }

        //  Constuctors
        //  ===========

        public ErrorResponse(params string[] errors)
        {
            Errors = errors.ToList();
        }
        
        public ErrorResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}