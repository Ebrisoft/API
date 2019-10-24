namespace API.Landlord.Request
{
    public class AddTenant
    {
        //  Properties
        //  ==========

        public int HouseId { get; set; }
        public string TenantUsername { get; set; } = null!;
    }
}