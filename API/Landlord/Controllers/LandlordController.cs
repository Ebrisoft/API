using System.Threading.Tasks;
using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using API.Landlord.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Landlord.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Landlord)]
    public class LandlordController : ControllerBase
    {
        //  Variables
        //  =========

        private readonly ILandlordRepository landlordRepository;

        //  Constructors
        //  ============

        public LandlordController(ILandlordRepository landlordRepository)
        {
            this.landlordRepository = landlordRepository;
        }

        //  Methods
        //  =======

        [AllowAnonymous]
        [HttpPost(Endpoints.Register)]
        public async Task<ActionResult> Register(Register request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse("The passwords do not match."));
            }

            IRegisterLandlordResult result = await landlordRepository.Register(request.Email, request.Email, request.Password).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponse(result.Errors));
            }

            bool signOnResult = await landlordRepository.SignIn(request.Email, request.Password).ConfigureAwait(false);

            if (!signOnResult)
            {
                return StatusCode(500, new ErrorResponse("Account created but unable to sign in user."));
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost(Endpoints.SignIn)]
        public async Task<ActionResult> SignIn(SignIn request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            bool result = await landlordRepository.SignIn(request.Username, request.Password).ConfigureAwait(false);

            if (!result)
            {
                return Unauthorized(new ErrorResponse("Unable to log in landlord."));
            }

            return NoContent();
        }

        [HttpPost(Endpoints.SignOut)]
        public async Task<ActionResult> SignOut()
        {
            await landlordRepository.SignOut().ConfigureAwait(false);
            return NoContent();
        }
    }
}