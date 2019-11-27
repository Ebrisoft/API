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
        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public IssueController(IIssueRepository issueRepository, ITenantRepository tenantRepository)
        {
            this.issueRepository = issueRepository;
            this.tenantRepository = tenantRepository;
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
                Id = searchResult.Id,
                Content = searchResult.Content,
                CreatedAt = searchResult.CreatedAt,
                IsResolved = searchResult.IsResolved,
                Title = searchResult.Title,
                Author = new Response.ApplicationUser
                {
                    Name = searchResult.Author.Name,
                    Email = searchResult.Author.Email,
                    PhoneNumber = searchResult.Author.PhoneNumber
                }
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

            ApplicationUser? tenant = await tenantRepository.GetFromUsername(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (tenant == null)
            {
                return BadRequest();
            }

            if (tenant.House == null)
            {
                return BadRequest("You are not currently in a house!");
            }

            bool success = await issueRepository.CreateIssue(createIssue.Title, createIssue.Content, tenant.House, tenant).ConfigureAwait(false);

            return success ? NoContent() : ServerError("Unable to create issue.");
        }
    }
}