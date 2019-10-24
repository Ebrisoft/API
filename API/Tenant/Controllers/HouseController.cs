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
    public class HouseController : ControllerBase
    {
        //  Variables
        //  =========

        private readonly IHouseRepository houseRepository;
        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public HouseController(IHouseRepository houseRepository, ITenantRepository tenantRepository)
        {
            this.houseRepository = houseRepository;
            this.tenantRepository = tenantRepository;
        }

        //  Methods
        //  =======
        
        [HttpPost(Endpoints.GetPinboard)]
        public async Task<ActionResult<Response.Pinboard>> GetPinboard()
        {
            House? house = (await tenantRepository.GetFromUsername(HttpContext.User.Identity.Name!).ConfigureAwait(false))?.House;

            if (house == null)
            {
                return BadRequest();
            }

            return new Response.Pinboard
            {
                Text = house.Pinboard
            };
        }
    }
}