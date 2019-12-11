using System.ComponentModel.DataAnnotations;

namespace API.Tenant.Request
{
    public class GetIssue
    {
        //  Properties
        //  ==========

        [Required]
        public int? Id { get; set; }
    }
}
