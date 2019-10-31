using System.Collections.Generic;

namespace API
{
    public class APIResponse<T> where T : class
    {
        //  Properties
        //  ==========

        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public T? Payload { get; set; }
    }
}