using Abstractions.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SQLServer.Models
{
    public class HouseDbo : House
    {
        //  Properties
        //  ==========

        [Key]
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Name { get => base.Name; set => base.Name = value; }

        public new IEnumerable<IssueDbo> Issues { get => base.Issues.Cast<IssueDbo>(); set => base.Issues = value; }
    }
}