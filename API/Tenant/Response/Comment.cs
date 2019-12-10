using System;

namespace API.Tenant.Response
{
    public class Comment
    {
        //  Properties
        //  ==========

        public ApplicationUser Author { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Content { get; set; } = null!;
    }
}
