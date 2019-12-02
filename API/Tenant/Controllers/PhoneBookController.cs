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
        //  Variables
        //  =========

        private readonly IContactRepository contactRepository;

        //  Constructors
        //  ============

        public PhoneBookController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        //  Methods
        //  =======

        [HttpPost(Endpoints.GetPhoneBook)]
        public async Task<ObjectResult> GetPhoneBook()
        {
            IEnumerable<Contact> searchResults = await contactRepository.GetPhonebook(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            return Ok(searchResults.Select(c => new Response.Contact
            {
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            }));
        }
    }
}