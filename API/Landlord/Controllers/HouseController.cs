using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Landlord.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Landlord)]
    public class HouseController : ControllerBase
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
        public async Task<ActionResult<Response.House>> CreateHouse(Request.CreateHouse createHouse)
        {
            if (createHouse == null)
            {
                return BadRequest();
            }

            House? house = await houseRepository.CreateHouse(HttpContext.User.Identity.Name!, createHouse.Name).ConfigureAwait(false);

            if (house == null)
            {
                return StatusCode(500, new ErrorResponse("Unable to create house."));
            }

            return StatusCode(201, new Response.House
            {
                Name = house.Name,
                Issues = house.Issues.Select(i => new Response.Issue
                {
                    Content = i.Content
                })
            });
        }

        [HttpPost(Endpoints.GetHouse)]
        public async Task<ActionResult<Response.House>> GetHouse(Request.GetHouse getHouse)
        {
            if (getHouse == null)
            {
                return BadRequest();
            }

            House? house = await houseRepository.FindById(getHouse.Id).ConfigureAwait(false);

            if (house == null)
            {
                return BadRequest();
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
        public async Task<ActionResult> AddTenant(Request.AddTenant addTenant)
        {
            if (addTenant == null)
            {
                return BadRequest();
            }

            bool success = await houseRepository.AddTenant(addTenant.HouseId, addTenant.TenantUsername).ConfigureAwait(false);

            if (!success)
            {
                return StatusCode(500, new ErrorResponse("Unable to add tenant to house."));
            }

            return NoContent();
        }
    }
}