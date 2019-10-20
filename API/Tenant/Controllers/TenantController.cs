using System.Threading.Tasks;
using Abstractions.Models;
using Abstractions.Repositories;
using API.Tenant.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Tenant.Controllers
{
    [ApiController]
    public class TenantController : ControllerBase
    {
        //  Variables
        //  =========

        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public TenantController(ITenantRepository tenantRepository)
        {
            this.tenantRepository = tenantRepository;
        }

        //  Methods
        //  =======

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

            IRegisterTenantResult result = await tenantRepository.RegisterTenant(request.Email, request.Email, request.Password).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponse(result.Errors));
            }

            bool signOnResult = await tenantRepository.SignInTenant(request.Email, request.Password).ConfigureAwait(false);

            if (!signOnResult)
            {
                return StatusCode(500, new ErrorResponse("Account created but unable to sign in user."));
            }

            return NoContent();
        }

        [HttpPost(Endpoints.SignIn)]
        public async Task<ActionResult> SignIn(SignIn request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            bool result = await tenantRepository.SignInTenant(request.Username, request.Password).ConfigureAwait(false);

            if (!result)
            {
                return Unauthorized(new ErrorResponse("Unable to log in tenant."));
            }

            return NoContent();
        }

        [HttpPost(Endpoints.SignOut)]
        public async Task<ActionResult> SignOut()
        {
            await tenantRepository.SignOutTenant().ConfigureAwait(false);
            return NoContent();
        }
    }
}