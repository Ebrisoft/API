using System.Linq;
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
        private readonly ILandlordRepository landlordRepository;
        private readonly ICommentRepository commentRepository;

        //  Constructors
        //  ============

        public IssueController(IIssueRepository issueRepository, IHouseRepository houseRepository, ILandlordRepository landlordRepository, ICommentRepository commentRepository)
        {
            this.issueRepository = issueRepository;
            this.houseRepository = houseRepository;
            this.landlordRepository = landlordRepository;
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
                return NotFound("Could not find Issue.");
            }

            return Ok(new Response.Issue
            {
                Id = searchResult.Id,
                CreatedAt = searchResult.CreatedAt,
                IsResolved = searchResult.IsResolved,
                Title = searchResult.Title,
                Content = searchResult.Content,
                Priority = searchResult.Priority,
                House = new Response.House
                {
                    Id = searchResult.House.Id,
                    Name = searchResult.House.Name
                },
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
                        Id = c.Author.Id,
                        Name = c.Author.Name,
                        PhoneNumber = c.Author.PhoneNumber,
                        Email = c.Author.Email
                    },
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                })
            }); ;
        }

        [HttpPost(Endpoints.CreateIssue)]
        public async Task<ObjectResult> CreateIssue(Request.CreateIssue createIssue)
        {
            if (createIssue == null)
            {
                return NoRequest();
            }

            ApplicationUser? landlord = await landlordRepository.GetFromUsername(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (landlord == null)
            {
                return BadRequest();
            }

            House? house = await houseRepository.FindById(createIssue.HouseId!.Value).ConfigureAwait(false);

            if (house == null || house.Landlord.Id != landlord.Id)
            {
                return BadRequest();
            }

            bool success = await issueRepository.CreateIssue(createIssue.Title, createIssue.Content, house, landlord, createIssue.Priority!.Value).ConfigureAwait(false);

            return success ? Created() : ServerError("Unable to create issue");
        }

        [HttpPost(Endpoints.SetPriority)]
        public async Task<ObjectResult> SetPriority(Request.SetPriority setPriority)
        {
            if (setPriority == null)
            {
                return NoRequest();
            }

            bool success = await issueRepository.SetPriority(setPriority.Id!.Value, setPriority.Priority!.Value).ConfigureAwait(false);

            return success ? NoContent() : ServerError("Unable to set priority");
        }

        [HttpPost(Endpoints.CreateComment)]
        public async Task<ObjectResult> CreateComment(Request.CreateComment createComment)
        {
            if (createComment == null)
            {
                return NoRequest();
            }

            Issue? issue = await issueRepository.GetIssueById(createComment.IssueId, includeHouse: true, includeComments: true).ConfigureAwait(false);

            if (issue == null)
            {
                return BadRequest("Issue does not exist");
            }

            bool ownsHouse = await houseRepository.DoesHouseBelongTo(issue.House.Id, HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (!ownsHouse)
            {
                return BadRequest("You do not own this house");
            }

            ApplicationUser? landlord = await landlordRepository.GetFromUsername(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            if (landlord == null)
            {
                return ServerError("Could not find your account");
            }

            Comment? comment = await commentRepository.CreateComment(createComment.Content, landlord, issue).ConfigureAwait(false);

            return comment != null ? Created() : ServerError("Unable to create comment");
        }
    }
}