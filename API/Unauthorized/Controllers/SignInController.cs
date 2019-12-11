using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Unauthorized.Controllers
{
    [ApiController]
    public class SignInController : APIControllerBase
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
        public async Task<ObjectResult> SignIn(Request.SignIn request)
        {
            if (request == null)
            {
                return NoRequest();
            }

            IEnumerable<string>? result = await signInRepository.SignIn(request.Email, request.Password).ConfigureAwait(false);

            if (result == null)
            {
                return Unauthorized("Unable to log in user.");
            }
            
            return Ok(new Response.SignIn(result));
        }

        [HttpPost(Endpoints.SignOut)]
        public async Task<ObjectResult> SignOut()
        {
            await signInRepository.SignOut().ConfigureAwait(false);
            return NoContent();
        }
    }
}