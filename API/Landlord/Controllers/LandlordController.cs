using System.Collections.Generic;
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
        private readonly ISignInRepository signInRepository;

        //  Constructors
        //  ============

        public LandlordController(ILandlordRepository landlordRepository, ISignInRepository signInRepository)
        {
            this.landlordRepository = landlordRepository;
            this.signInRepository = signInRepository;
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

            IEnumerable<string>? roles = await signInRepository.SignIn(request.Email, request.Password).ConfigureAwait(false);

            if (roles == null)
            {
                return StatusCode(500, new ErrorResponse("Account created but unable to sign in user."));
            }

            return Ok(new Unauthorized.Response.SignIn(roles));
        }
    }
}