using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class CreateHouse
    {
        //  Properties
        //  ==========

        [Required]
        public string Name { get; set; } = null!;
    }
}
