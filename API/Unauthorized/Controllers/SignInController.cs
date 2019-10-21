using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Unauthorized.Controllers
{
    [ApiController]
    public class SignInController : ControllerBase
    {
        //  Variables
        //  =========

        private readonly ISignInRepository signInRepository;

        //  Constructors
        //  ============

        public SignInController(ISignInRepository signInRepository)
        {
            this.signInRepository = signInRepository;
        }

        //  Methods
        //  =======

        [HttpPost(Endpoints.SignIn)]
        public async Task<ActionResult> SignIn(Request.SignIn request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            IEnumerable<string>? result = await signInRepository.SignIn(request.Username, request.Password).ConfigureAwait(false);

            if (result == null)
            {
                return Unauthorized(new ErrorResponse("Unable to log in user."));
            }

            return Ok(new Response.SignIn(result));
        }

        [HttpPost(Endpoints.SignOut)]
        public async Task<ActionResult> SignOut()
        {
            await signInRepository.SignOut().ConfigureAwait(false);
            return NoContent();
        }
    }
}