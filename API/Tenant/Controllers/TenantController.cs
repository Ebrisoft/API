using System.Collections.Generic;
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
    public class TenantController : ControllerBase
    {
        //  Variables
        //  =========

        private readonly ISignInRepository signInRepository;
        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public TenantController(ISignInRepository signInRepository, ITenantRepository tenantRepository)
        {
            this.signInRepository = signInRepository;
            this.tenantRepository = tenantRepository;
        }

        //  Methods
        //  =======

        [AllowAnonymous]
        [HttpPost(Endpoints.Register)]
        public async Task<ActionResult> Register(Request.Register request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse("The passwords do not match."));
            }

            IRegisterTenantResult result = await tenantRepository.RegisterTenant(request.Email, request.Email, request.Password).ConfigureAwait(false);

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