using System.ComponentModel.DataAnnotations;

namespace API.Landlord.Request
{
    public class AddTenant
    {
        //  Properties
        //  ==========

        [Required]
        public int? HouseId { get; set; }

        [Required]
        public string TenantUsername { get; set; } = null!;
    }
}