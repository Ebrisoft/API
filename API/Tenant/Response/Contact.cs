namespace API.Tenant.Response
{
    public class Contact
    {
        //  Properties
        //  ==========

        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Type { get; set; } = null!;
    }
}