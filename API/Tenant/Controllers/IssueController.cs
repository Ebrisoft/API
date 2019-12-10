using System.Linq;
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
        private readonly ICommentRepository commentRepository;

        //  Constructors
        //  ============

        public IssueController(IIssueRepository issueRepository, ITenantRepository tenantRepository, ICommentRepository commentRepository)
        {
            this.issueRepository = issueRepository;
            this.tenantRepository = tenantRepository;
            this.commentRepository = commentRepository;
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

            Issue? searchResult = await issueRepository.GetIssueById(getIssue.Id!.Value, true, true, true).ConfigureAwait(false);

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
                    Id = searchResult.Author.Id,
                    Name = searchResult.Author.Name,
                    Email = searchResult.Author.Email,
                    PhoneNumber = searchResult.Author.PhoneNumber
                },
                Comments = searchResult.Comments.Select(c => new Response.Comment
                {
                    Author = new Response.ApplicationUser
                    {
                        Email = c.Author.Email,
                        Id = c.Author.Id,
                        Name = c.Author.Name,
                        PhoneNumber = c.Author.PhoneNumber
                    },
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                })
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
    
        [HttpPost(Endpoints.CreateComment)]
        public async Task<ObjectResult> CreateComment(Request.CreateComment createComment)
        {
            if (createComment == null)
            {
                return NoRequest();
            }
            
            Issue? issue = await issueRepository.GetIssueById(createComment.IssueId, includeHouse: true).ConfigureAwait(false);

            if (issue == null)
            {
                return BadRequest("Cannot find issue");
            }

            ApplicationUser? tenant = await tenantRepository.GetFromUsername(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (tenant == null)
            {
                return ServerError("Cannot find your account");
            }

            if (tenant.House == null || tenant.House.Id != issue.House.Id)
            {
                return BadRequest("You are not in the property for this issue");
            }

            Comment? comment = await commentRepository.CreateComment(createComment.Content, tenant, issue).ConfigureAwait(false);

            return comment != null ? Created() : ServerError("Unable to create comment");
        }

        [HttpPost(Endpoints.Archive)]
        public async Task<ObjectResult> Archive(Request.Archive archive)
        {
            if (archive == null)
            {
                return NoRequest();
            }
            
            bool ownsIssue = await issueRepository.IsAuthor(archive.Id, HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (!ownsIssue)
            {
                return BadRequest("You do not own this issue.");
            }

            bool success = await issueRepository.Archive(archive.Id).ConfigureAwait(false);

            return success ? NoContent() : ServerError("Unable to archive the issue.");
        }
    }
}