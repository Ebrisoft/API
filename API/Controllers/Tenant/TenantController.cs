using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Models;
using Abstractions.Repositories;
using API.Endpoints;
using API.Models;
using API.Requests.Tenant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Tenant
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

        [HttpPost(TenantEndpoints.RegisterTenant)]
        public async Task<ActionResult> RegisterTenant(RegisterTenant request)
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

        [HttpPost(TenantEndpoints.SignIn)]
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
    }
}