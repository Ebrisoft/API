namespace API.Landlord.Response
{
    public class Contact
    {
        //  Properties
        //  ==========

        public string Name { get; set; } = null!;
        public int HouseId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}