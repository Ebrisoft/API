using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Landlord.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Landlord)]
    public class HouseController : APIControllerBase
    {
        //  Variables
        //  =========

        private readonly IHouseRepository houseRepository;

        //  Constructors
        //  ============

        public HouseController(IHouseRepository houseRepository)
        {
            this.houseRepository = houseRepository;
        }

        //  Methods
        //  =======

        [HttpPost(Endpoints.CreateHouse)]
        public async Task<ObjectResult> CreateHouse(Request.CreateHouse createHouse)
        {
            if (createHouse == null)
            {
                return NoRequest();
            }

            House? house = await houseRepository.CreateHouse(HttpContext.User.Identity.Name!, createHouse.Name).ConfigureAwait(false);

            if (house == null)
            {
                return ServerError("Unable to create house.");
            }

            return Created(new Response.House
            {
                Name = house.Name,
                Issues = house.Issues.Select(i => new Response.Issue
                {
                    Content = i.Content
                })
            });
        }

        [HttpPost(Endpoints.GetHouse)]
        public async Task<ObjectResult> GetHouse(Request.GetHouse getHouse)
        {
            if (getHouse == null)
            {
                return NoRequest();
            }

            House? house = await houseRepository.FindById(getHouse.Id).ConfigureAwait(false);

            if (house == null)
            {
                return NotFound("Could not find a house with that ID.");
            }

            return Ok(new Response.House
            {
                Name = house.Name,
                Issues = house.Issues.Select(i => new Response.Issue
                {
                    Content = i.Content
                })
            });
        }

        [HttpPost(Endpoints.AddTenant)]
        public async Task<ObjectResult> AddTenant(Request.AddTenant addTenant)
        {
            if (addTenant == null)
            {
                return NoRequest();
            }

            bool success = await houseRepository.AddTenant(addTenant.HouseId, addTenant.TenantUsername).ConfigureAwait(false);

            if (!success)
            {
                return ServerError("Unable to add tenant to house.");
            }

            return NoContent();
        }

        [HttpPost(Endpoints.GetPinboard)]
        public async Task<ObjectResult> GetPinboard(Request.GetPinboard getPinboard)
        {
            if (getPinboard == null)
            {
                return NoRequest();
            }

            bool doesOwn = await houseRepository.DoesHouseBelongTo(getPinboard.HouseId, HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (!doesOwn)
            {
                return BadRequest("You do not own this house.");
            }

            string? pinboardText = await houseRepository.GetPinboard(getPinboard.HouseId).ConfigureAwait(false);

            if (pinboardText == null)
            {
                return BadRequest("Could not find a house with that ID");
            }

            return Ok(new Response.Pinboard
            {
                Text = pinboardText
            });
        }

        [HttpPost(Endpoints.SetPinboard)]
        public async Task<ObjectResult> SetPinboard(Request.SetPinboard setPinboard)
        {
            if (setPinboard == null)
            {
                return NoRequest();
            }

            bool doesOwn = await houseRepository.DoesHouseBelongTo(setPinboard.HouseId, HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (!doesOwn)
            {
                return BadRequest("You do not own this house.");
            }

            bool success = await houseRepository.SetPinboard(setPinboard.HouseId, setPinboard.Text).ConfigureAwait(false);

            if (!success)
            {
                return BadRequest("Could not find a house with that ID");
            }

            return NoContent();
        }
    }
}