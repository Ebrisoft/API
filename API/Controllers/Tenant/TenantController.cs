using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        //  Constructors
        //  ============

        public TenantController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        //  Methods
        //  =======

        [HttpPost(TenantEndpoints.RegisterTenant)]
        public async Task<ActionResult> RegisterTenant(RegisterTenant registerTenant)
        {
            if (registerTenant == null)
            {
                return BadRequest();
            }

            if (registerTenant.Password != registerTenant.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse("The passwords do not match!"));
            }

            var user = new IdentityUser
            {
                UserName = registerTenant.Email,
                Email = registerTenant.Email
            };

            IdentityResult result = await userManager.CreateAsync(user, registerTenant.Password).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponse(result.Errors.Select(e => e.Description)));
            }

            await signInManager.SignInAsync(user, true).ConfigureAwait(false);

            return NoContent();
        }
    }
}