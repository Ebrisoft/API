using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class GetPinboard
    {
        //  Properties
        //  ==========

        [Required]
        public int? HouseId { get; set; }
    }
}
