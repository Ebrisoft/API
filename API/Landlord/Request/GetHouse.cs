using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class GetHouse
    {
        //  Properties

        [Required]
        public int? Id { get; set; }
    }
}