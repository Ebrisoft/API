using Abstractions.Models;
using System.Collections.Generic;
using System.Linq;

namespace SQLServer.Models
{
    public class ApplicationUserDbo : ApplicationUser
    {
        //  Properties
        //  ==========

        public new IEnumerable<HouseDbo> Houses { get => base.Houses.Cast<HouseDbo>(); set => base.Houses = value; }
    }
}