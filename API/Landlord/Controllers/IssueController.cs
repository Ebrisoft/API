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

            if (createIssue.Priority < 0 || createIssue.Priority > 2)
            {
                return BadRequest("The priority needs to be in range 0-2");
            }

            House? house = await houseRepository.FindById(createIssue.HouseId).ConfigureAwait(false);

            if (house == null || house.Landlord.Id != landlord.Id)
            {
                return BadRequest();
            }

            bool success = await issueRepository.CreateIssue(createIssue.Title, createIssue.Content, house, landlord, createIssue.Priority).ConfigureAwait(false);

            return success ? Created() : ServerError("Unable to create issue");
        }
    }
}