using System.Collections.Generic;

namespace Abstractions.Models
{
    public class House
    {
        //  Properties
        //  ==========

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Pinboard { get; set; } = null!;

        public ApplicationUser Landlord { get; set; } = null!;
        public IEnumerable<ApplicationUser> Tenants { get; set; } = null!;
        public IEnumerable<Issue> Issues { get; set; } = null!;
        public IEnumerable<Contact> Contacts { get; set; } = null!;
    }
}