using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using SQLServer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class LandlordRepository : ILandlordRepository
    {
        //  Variables
        //  =========

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        //  Constructors
        //  ============

        public LandlordRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        //  Methods
        //  =======

        public async Task<IRegisterLandlordResult> Register(string username, string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            IdentityResult identityResult = await userManager.CreateAsync(user, password).ConfigureAwait(false);

            if (!identityResult.Succeeded)
            {
                return new RegisterLandlordResult
                {
                    Succeeded = identityResult.Succeeded,
                    Errors = identityResult.Errors.Select(e => e.Description)
                };
            }

#warning If the above is successful but this fails then there is a user created with no role
            IdentityResult addRoleIdentityResult = await userManager.AddToRoleAsync(user, Roles.Landlord).ConfigureAwait(false);

            return new RegisterLandlordResult
            {
                Succeeded = addRoleIdentityResult.Succeeded,
                Errors = addRoleIdentityResult.Errors.Select(e => e.Description)
            };
        }

        public async Task<bool> SignIn(string username, string password)
        {
            SignInResult result = await signInManager.PasswordSignInAsync(username, password, true, false).ConfigureAwait(false);

            return result.Succeeded;
        }

        public async Task SignOut()
        {
            await signInManager.SignOutAsync().ConfigureAwait(false);
        }
    }
}