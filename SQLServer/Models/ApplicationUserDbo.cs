using Abstractions.Models;
using System.Collections.Generic;
using System.Linq;

namespace SQLServer.Models
{
    public class ApplicationUserDbo : ApplicationUser
    {
        //  Properties
        //  ==========

        public new HouseDbo? House { get => base.House == null ? null : (HouseDbo)base.House; set => base.House = value; }
        public new IEnumerable<HouseDbo> Houses { get => base.Houses.Cast<HouseDbo>(); set => base.Houses = value; }
        
        public new IEnumerable<IssueDbo> Issues { get => base.Issues.Cast<IssueDbo>(); set => base.Issues = value; }
        public new IEnumerable<CommentDbo> Comments { get => base.Comments.Cast<CommentDbo>(); set => base.Comments = value; }
    }
}