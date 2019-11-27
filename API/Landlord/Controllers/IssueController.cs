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

            Issue? searchResult = await issueRepository.GetIssueById(getIssue.Id).ConfigureAwait(false);

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
                House = new Response.House
                {
                    Name = searchResult.House.Name
                },
                Author = new Response.ApplicationUser
                {
                    UserName = searchResult.Author.UserName
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

            House? house = await houseRepository.FindById(createIssue.HouseId).ConfigureAwait(false);

            if (house == null || house.Landlord.Id != HttpContext.User.Identity.Name!)
            {
                return BadRequest();
            }

            bool success = await issueRepository.CreateIssue(createIssue.Title, createIssue.Content, house, landlord).ConfigureAwait(false);

            return success ? Created("") : ServerError("Unable to create issue");
        }
    }
}