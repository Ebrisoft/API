using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Landlord.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Landlord)]
    public class IssueController : APIControllerBase
    {
        //  Variables
        //  =========

        private readonly IIssueRepository issueRepository;
        private readonly IHouseRepository houseRepository;

        //  Constructors
        //  ============

        public IssueController(IIssueRepository issueRepository, IHouseRepository houseRepository)
        {
            this.issueRepository = issueRepository;
            this.houseRepository = houseRepository;
        }

        //  Methods
        //  =======

        [HttpPost(Endpoints.GetIssue)]
        public async Task<ObjectResult> GetIssue(Request.GetIssue getIssue)
        {
            if (getIssue == null)
            {
                return NoRequest();
            }

            Issue? searchResult = await issueRepository.GetIssueById(getIssue.Id).ConfigureAwait(false);

            if (searchResult == null)
            {
                return NotFound("Could not find Issue.");
            }

            return Ok(new Response.Issue
            {
                Content = searchResult.Content,
                House = new Response.House
                {
                    Name = searchResult.House.Name
                }
            });
        }

        [HttpPost(Endpoints.CreateIssue)]
        public async Task<ObjectResult> CreateIssue(Request.CreateIssue createIssue)
        {
            if (createIssue == null)
            {
                return NoRequest();
            }

            bool isValidHouse = await houseRepository.DoesHouseBelongTo(createIssue.HouseId, HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (!isValidHouse)
            {
                return BadRequest("Could not find the house.");
            }

            bool success = await issueRepository.CreateIssue(createIssue.HouseId, createIssue.Content).ConfigureAwait(false);

#warning Should return at least ID to created issue
            return success ? Created("") : ServerError("Unable to create issue");
        }
    }
}