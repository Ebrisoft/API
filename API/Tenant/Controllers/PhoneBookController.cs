using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Tenant.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Tenant)]
    public class PhoneBookController : APIControllerBase
    {
        //  Constants
        //  =========

        private const string CustomContact = "custom";
        private const string LandlordContact = "landlord";

        //  Variables
        //  =========

        private readonly IContactRepository contactRepository;
        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public PhoneBookController(IContactRepository contactRepository, ITenantRepository tenantRepository)
        {
            this.contactRepository = contactRepository;
            this.tenantRepository = tenantRepository;
        }

        //  Methods
        //  =======

        [HttpPost(Endpoints.GetPhoneBook)]
        public async Task<ObjectResult> GetPhoneBook()
        {
            IEnumerable<Contact> contacts = await contactRepository.GetPhonebook(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            List<Response.Contact> results = contacts.Select(c => new Response.Contact
            {
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Type = CustomContact
            })
            .OrderBy(c => c.Name)
            .ToList();

            ApplicationUser? landlord = await tenantRepository.GetLandlord(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (landlord != null)
            {
                results.Insert(0, new Response.Contact
                {
                    Name = landlord.Name,
                    Email = landlord.Email,
                    PhoneNumber = landlord.PhoneNumber,
                    Type = LandlordContact
                });
            }

            return Ok(results);
        }
    }
}