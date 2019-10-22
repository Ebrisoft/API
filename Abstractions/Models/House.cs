using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Models
{
    public class House
    {
        //  Properties
        //  ==========

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public IEnumerable<Issue> Issues { get; set; } = null!;
    }
}