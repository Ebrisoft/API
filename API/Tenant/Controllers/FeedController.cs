using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Tenant.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Tenant)]
    public class FeedController : APIControllerBase
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
        public async Task<ObjectResult> GetFeed()
        {
            IEnumerable<Issue> searchResults = await issueRepository.GetAllIssues(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            IEnumerable<Response.Issue> result = searchResults.Select(s => new Response.Issue
            {
                Id = s.Id,
                Content = s.Content,
                CreatedAt = s.CreatedAt,
                IsResolved = s.IsResolved,
                Title = s.Title,
                Author = new Response.ApplicationUser()
                {
                    UserName = s.Author.UserName
                }
            });

            return Ok(result);
        }
    }
}