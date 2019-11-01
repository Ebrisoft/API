using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Models.Results;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Tenant.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Tenant)]
    public class TenantController : APIControllerBase
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
        public async Task<ObjectResult> Register(Request.Register request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("The passwords do not match.");
            }

            IRegisterTenantResult result = await tenantRepository.RegisterTenant(request.Email, request.Email, request.Password).ConfigureAwait(false);

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