using System.Threading.Tasks;
using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Tenant.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Tenant)]
    public class HouseController : APIControllerBase
    {
        //  Variables
        //  =========

        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public HouseController(IHouseRepository houseRepository, ITenantRepository tenantRepository)
        {
            this.tenantRepository = tenantRepository;
        }

        //  Methods
        //  =======
        
        [HttpPost(Endpoints.GetPinboard)]
        public async Task<ObjectResult> GetPinboard()
        {
            House? house = (await tenantRepository.GetFromUsername(HttpContext.User.Identity.Name!).ConfigureAwait(false))?.House;

            if (house == null)
            {
                return BadRequest("No house found.");
            }

            return Ok(new Response.Pinboard
            {
                Text = house.Pinboard
            });
        }
    }
}