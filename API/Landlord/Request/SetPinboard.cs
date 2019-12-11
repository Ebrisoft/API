using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class SetPinboard
    {
        //  Properties
        //  ==========

        [Required]
        public int? HouseId { get; set; }

        [Required]
        public string Text { get; set; } = null!;
    }
}
