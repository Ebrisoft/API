using System.ComponentModel.DataAnnotations;

namespace API.Tenant.Request
{
    public class CreateIssue
    {
        //  Properties
        //  ==========

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;
    }
}