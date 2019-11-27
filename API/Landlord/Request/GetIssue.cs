using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class GetIssue
    {
        //  Properties
        //  ==========

        [Required]
        public int? Id { get; set; }
    }
}
