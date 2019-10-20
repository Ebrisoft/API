using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        //  Variables
        //  =========

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        //  Constructors
        //  ============

        public TenantRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        //  Methods
        //  =======

        public async Task<IRegisterTenantResult> RegisterTenant(string username, string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            IdentityResult identityResult = await userManager.CreateAsync(user, password).ConfigureAwait(false);

            return new RegisterTenantResult
            {
                Succeeded = identityResult.Succeeded,
                Errors = identityResult.Errors.Select(e => e.Description)
            };
        }

        public async Task<bool> SignInTenant(string username, string password)
        {
            IdentityUser user = await userManager.FindByNameAsync(username).ConfigureAwait(false);

            SignInResult result = await signInManager.PasswordSignInAsync(user, password, true, false).ConfigureAwait(false);

            return result.Succeeded;
        }
    }
}