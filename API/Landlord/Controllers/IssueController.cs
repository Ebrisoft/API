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
    public class IssueController : ControllerBase
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
        public async Task<ActionResult<IEnumerable<Response.Issue>>> GetIssue(Request.GetIssue getIssue)
        {
            if (getIssue == null)
            {
                return BadRequest();
            }

            Issue? searchResult = await issueRepository.GetIssueById(getIssue.Id).ConfigureAwait(false);

            if (searchResult == null)
            {
                return NotFound();
            }

            Response.Issue result = new Response.Issue
            {
                Content = searchResult.Content,
                House = new Response.House
                {
                    Name = searchResult.House.Name
                }
            };

            return Ok(result);
        }

        [HttpPost(Endpoints.CreateIssue)]
        public async Task<ActionResult> CreateIssue(Request.CreateIssue createIssue)
        {
            if (createIssue == null)
            {
                return BadRequest();
            }

            bool isValidHouse = await houseRepository.DoesHouseBelongTo(createIssue.HouseId, HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (!isValidHouse)
            {
                return BadRequest();
            }

            bool success = await issueRepository.CreateIssue(createIssue.HouseId, createIssue.Content).ConfigureAwait(false);

            return success ? NoContent() : StatusCode(500);
        }
    }
}