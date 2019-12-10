using Abstractions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SQLServer.Models
{
    public class IssueDbo : Issue
    {
        //  Properties
        //  ==========

        [Key]
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Title { get => base.Title; set => base.Title = value; }
        public new string Content { get => base.Content; set => base.Content = value; }
        public new DateTime CreatedAt { get => base.CreatedAt; set => base.CreatedAt = value; }
        public new bool IsResolved { get => base.IsResolved; set => base.IsResolved = value; }
        public new int Priority { get => base.Priority; set => base.Priority = value; }
        public new HouseDbo House { get => (HouseDbo)base.House; set => base.House = value; }
        public new ApplicationUserDbo Author { get => (ApplicationUserDbo)base.Author; set => base.Author = value; }
        public new IEnumerable<CommentDbo> Comments { get => base.Comments.Cast<CommentDbo>(); set => base.Comments = value; }
    }
}