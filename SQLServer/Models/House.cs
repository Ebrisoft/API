using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SQLServer.Models
{
    public class House : Abstractions.Models.House
    {
        //  Properties
        //  ==========

        [Key]
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Name { get => base.Name; set => base.Name = value; }

        public new IEnumerable<Issue> Issues { get => base.Issues.Cast<Issue>(); set => base.Issues = value; }
    }
}