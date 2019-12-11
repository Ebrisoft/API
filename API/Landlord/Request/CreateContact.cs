using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class CreateContact
    {
        //  Properties
        //  ==========

        [Required]
        public int? HouseId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
            ErrorMessage = "The Email field is not a valid email-address.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(\+44|0)7[0-9]{9}$", ErrorMessage = "The PhoneNumber field is not a valid phone number.")]
        public string? PhoneNumber { get; set; }
    }
}