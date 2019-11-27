using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Models.Results;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Landlord.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Landlord)]
    public class LandlordController : APIControllerBase
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
        public async Task<ObjectResult> Register(Request.Register request)
        {
            if (request == null)
            {
                return NoRequest();
            }

            IRegisterLandlordResult result = await landlordRepository.Register(request.Email, request.Password, request.PhoneNumber, request.Name).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            IEnumerable<string>? roles = await signInRepository.SignIn(request.Email, request.Password).ConfigureAwait(false);

            if (roles == null)
            {
                return ServerError("Account created but unable to sign in user.");
            }

            return Ok(new Unauthorized.Response.SignIn(roles));
        }
    }
}