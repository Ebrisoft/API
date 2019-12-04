using Abstractions.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SQLServer.Models
{
    public class CommentDbo : Comment
    {
        //  Properties
        //  ==========

        [Key]
        public new string Id { get => base.Id; set => base.Id = value; }
        public new ApplicationUserDbo Author { get => (ApplicationUserDbo)base.Author; set => base.Author = value; }
        public new IssueDbo Issue { get => (IssueDbo)base.Issue; set => base.Issue = value; }
        public new DateTime CreatedAt { get => base.CreatedAt; set => base.CreatedAt = value; }
        public new string Content { get => base.Content; set => base.Content = value; }
    }
}