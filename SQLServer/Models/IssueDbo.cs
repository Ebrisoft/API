using Abstractions.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SQLServer.Models
{
    public class IssueDbo : Issue
    {
        [Key]
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Title { get => base.Title; set => base.Title = value; }
        public new string Content { get => base.Content; set => base.Content = value; }
        public new DateTime CreatedAt { get => base.CreatedAt; set => base.CreatedAt = value; }
        public new bool IsResolved { get => base.IsResolved; set => base.IsResolved = value; }
        public new int Priority { get => base.Priority; set => base.Priority = value; }

        public new HouseDbo House { get => (HouseDbo)base.House; set => base.House = value; }

        public new ApplicationUserDbo Author { get => (ApplicationUserDbo)base.Author; set => base.Author = value; }
    }
}