using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Landlord.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Landlord)]
    public class PhoneBookController : APIControllerBase
    {
        //  Constants
        //  =========

        private const string CustomContact = "custom";
        private const string TenantContact = "tentant";

        //  Variables
        //  =========

        private readonly IContactRepository contactRepository;
        private readonly ILandlordRepository landlordRepository;

        //  Constructors
        //  ============

        public PhoneBookController(IContactRepository contactRepository, ILandlordRepository landlordRepository)
        {
            this.contactRepository = contactRepository;
            this.landlordRepository = landlordRepository;
        }

        //  Methods
        //  =======

        [HttpPost(Endpoints.GetPhoneBook)]
        public async Task<ObjectResult> GetPhoneBook()
        {
            IEnumerable<Contact> contacts = await contactRepository.GetPhonebook(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            IEnumerable<ApplicationUser> tenants = await landlordRepository.GetAllTenants(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            return Ok(contacts.Select(c => new Response.Contact
            {
                Name = c.Name,
                HouseId = c.House.Id,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Type = CustomContact
            })
            .Union(tenants.Select(t => new Response.Contact
            {
                Name = t.Name,
                HouseId = t.House!.Id,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber,
                Type = TenantContact
            }))
            .OrderBy(c => c.Name));
        }
    }
}