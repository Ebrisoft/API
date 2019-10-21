using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using API.Landlord.Models;
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
        public async Task<ActionResult<IEnumerable<Issue>>> GetFeed()
        {
            IEnumerable<IIssue> searchResults = await issueRepository.GetAllIssues().ConfigureAwait(false);

            IEnumerable<Issue> result = searchResults.Select(s => new Issue
            {
                Content = s.Content
            });

            return Ok(result);
        }
    }
}