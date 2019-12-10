using System.ComponentModel.DataAnnotations;

namespace API.Tenant.Request
{
    public class CreateComment
    {
        //  Properties
        //  ==========

        [Required]
        public int IssueId { get; set; }

        [Required]
        public string Content { get; set; } = null!;
    }
}
