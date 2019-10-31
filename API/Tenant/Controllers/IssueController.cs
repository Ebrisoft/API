using System.Collections.Generic;
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
    public class IssueController : APIControllerBase
    {
        //  Variables
        //  =========

        private readonly IIssueRepository issueRepository;

        //  Constructors
        //  ============

        public IssueController(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
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
                return NotFound("Issue");
            }

            Response.Issue result = new Response.Issue
            {
                Content = searchResult.Content
            };

            return Ok(result);
        }

        
        [HttpPost(Endpoints.CreateIssue)]
        public async Task<ObjectResult> CreateIssue(Request.CreateIssue createIssue)
        {
            if (createIssue == null)
            {
                return NoRequest();
            }

#warning Needs refactoring to take the house Id from the house the user lives in once houses have tenants
            bool success = await issueRepository.CreateIssue(createIssue.HouseId, createIssue.Content).ConfigureAwait(false);

            return success ? NoContent() : ServerError("Unable to create issue.");
        }
    }
}