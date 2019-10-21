using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class SignInRepository : ISignInRepository
    {
        //  Variables
        //  =========

        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        //  Constructors
        //  ============

        public SignInRepository(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        //  Methods
        //  =======

        public async Task<IEnumerable<string>?> SignIn(string username, string password)
        {
            SignInResult result = await signInManager.PasswordSignInAsync(username, password, true, false).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return null;
            }

            IdentityUser user = await userManager.FindByNameAsync(username).ConfigureAwait(false);

            IList<string> roles = await userManager.GetRolesAsync(user).ConfigureAwait(false);

            return roles;
        }

        public async Task SignOut()
        {
            await signInManager.SignOutAsync().ConfigureAwait(false);
        }
    }
}