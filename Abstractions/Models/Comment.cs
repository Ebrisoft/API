using System;

namespace Abstractions.Models
{
    public class Comment
    {
        //  Properties
        //  ==========

        public string Id { get; set; } = null!;
        public ApplicationUser Author { get; set; } = null!;
        public Issue Issue { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Content { get; set; } = null!;
    }
}