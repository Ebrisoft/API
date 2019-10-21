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
    public class IssueController : ControllerBase
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
        public async Task<ActionResult<IEnumerable<Response.Issue>>> GetIssue(Request.GetIssue getIssue)
        {
            if (getIssue == null)
            {
                return BadRequest();
            }

            IIssue searchResult = await issueRepository.GetIssueById(getIssue.Id).ConfigureAwait(false);

            if (searchResult == null)
            {
                return NotFound();
            }

            Response.Issue result = new Response.Issue
            {
                Content = searchResult.Content
            };

            return Ok(result);
        }

        [HttpPost(Endpoints.CreateIssue)]
        public async Task<ActionResult<IEnumerable<Response.Issue>>> CreateIssue(Request.CreateIssue createIssue)
        {
            if (createIssue == null)
            {
                return BadRequest();
            }

            bool success = await issueRepository.CreateIssue(createIssue.Content).ConfigureAwait(false);

            return success ? NoContent() : StatusCode(500);
        }
    }
}