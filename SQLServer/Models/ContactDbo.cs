using Abstractions.Models;
using System.ComponentModel.DataAnnotations;

namespace SQLServer.Models
{
    public class ContactDbo : Contact
    {
        //  Properties
        //  ==========

        [Key]
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Name { get => base.Name; set => base.Name = value; }
        public new HouseDbo House { get => (HouseDbo)base.House; set => base.House = value; }
        public new string? Email { get => base.Email; set => base.Email = value; }
        public new string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
    }
}
