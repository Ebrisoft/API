using System.Collections.Generic;

namespace API.Landlord.Response
{
    public class House
    {
        //  Properties
        //  ==========

        public string Name { get; set; } = null!;
        public IEnumerable<Issue> Issues { get; set; } = null!;
    }
}