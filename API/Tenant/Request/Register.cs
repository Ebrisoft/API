using System.ComponentModel.DataAnnotations;

namespace API.Tenant.Request
{
    public class Register
    {
        //  Properties
        //  ==========

        [Required]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
            ErrorMessage = "The Email field is not a valid email-address.")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(\+44|0)7[0-9]{9}$", ErrorMessage="The PhoneNumber field is not a valid phone number.")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;
    }
}