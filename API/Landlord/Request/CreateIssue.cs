using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class CreateIssue
    {
        //  Properties
        //  ==========

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;
        
        [Required]
        public int? HouseId { get; set; }
        
        [Required]
        [Range(0, 2)]
        public int? Priority { get; set; }
    }
}