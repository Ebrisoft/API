using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
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
