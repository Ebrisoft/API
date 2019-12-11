using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class SetPriority
    {
        //  Properties
        //  ==========

        [Required]
        public int? Id { get; set; }

        [Required]
        [Range(0, 2)]
        public int? Priority { get; set; }
    }
}
