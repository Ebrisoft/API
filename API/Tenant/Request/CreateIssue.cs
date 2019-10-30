namespace API.Tenant.Request
{
    public class CreateIssue
    {
        //  Properties
        //  ==========

        public string Content { get; set; } = null!;
        public int HouseId { get; set; }
    }
}