using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Landlord.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Landlord)]
    public class FeedController : ControllerBase
    {
        //  Variables
        //  =========

        private readonly IIssueRepository issueRepository;

        //  Constructors
        //  ============

        public FeedController(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        //  Methods
        //  =======

        [HttpPost(Endpoints.GetFeed)]
        public async Task<ActionResult<IEnumerable<Response.Issue>>> GetFeed()
        {
            IEnumerable<Issue> searchResults = await issueRepository.GetAllIssues(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            IEnumerable<Response.Issue> result = searchResults.Select(s => new Response.Issue
            {
                Content = s.Content
            });

            return Ok(result);
        }
    }
}