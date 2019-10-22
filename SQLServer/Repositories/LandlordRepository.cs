using Abstractions;
using Abstractions.Models.Results;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using SQLServer.Models.Results;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class LandlordRepository : ILandlordRepository
    {
        //  Variables
        //  =========

        private readonly UserManager<IdentityUser> userManager;

        //  Constructors
        //  ============

        public LandlordRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
    }
}