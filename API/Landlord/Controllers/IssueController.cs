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

        //  Constructors
        //  ============

        public IssueController(IIssueRepository issueRepository, IHouseRepository houseRepository, ILandlordRepository landlordRepository)
        {
            this.issueRepository = issueRepository;
            this.houseRepository = houseRepository;
            this.landlordRepository = landlordRepository;
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

            Issue? searchResult = await issueRepository.GetIssueById(getIssue.Id!.Value).ConfigureAwait(false);

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

        public async Task<ObjectResult> Archive(Request.Archive archive)
        {
            if (archive == null)
            {
                return NoRequest();
            }

            bool ownsHouseForIssue = await landlordRepository.DoesOwnHouseForIssue(HttpContext.User.Identity.Name!, archive.Id).ConfigureAwait(false);

            if (!ownsHouseForIssue)
            {
                return BadRequest("You do not own the property for this issue.");
            }

            bool success = await issueRepository.Archive(archive.Id).ConfigureAwait(false);

            return success ? NoContent() : ServerError("Unable to archive the issue.");
        }
    }
}