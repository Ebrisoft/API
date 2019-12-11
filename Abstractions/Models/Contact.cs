namespace Abstractions.Models
{
    public class Contact
    {
        //  Properties
        //  ==========

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public House House { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
