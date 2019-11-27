using Abstractions;
using Abstractions.Models;
using Abstractions.Models.Results;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using SQLServer.Models.Results;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class LandlordRepository : ILandlordRepository
    {

        //  Variables
        //  =========

        private readonly AppDbContext appDbContext;
        private readonly UserManager<ApplicationUserDbo> userManager;

        //  Constructors
        //  ============

        public LandlordRepository(AppDbContext appDbContext, UserManager<ApplicationUserDbo> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }

        //  Methods
        //  =======

        public async Task<IRegisterLandlordResult> Register(string email, string password, string phoneNumber, string name)
        {
            ApplicationUserDbo landlord = new ApplicationUserDbo
            {
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber,
                Name = name
            };

            IdentityResult identityResult = await userManager.CreateAsync(landlord, password).ConfigureAwait(false);

            if (!identityResult.Succeeded)
            {
                return new RegisterLandlordResult
                {
                    Succeeded = identityResult.Succeeded,
                    Errors = identityResult.Errors.Select(e => e.Description)
                };
            }

#warning If the above is successful but this fails then there is a user created with no role
            IdentityResult addRoleIdentityResult = await userManager.AddToRoleAsync(landlord, Roles.Landlord).ConfigureAwait(false);

            return new RegisterLandlordResult
            {
                Succeeded = addRoleIdentityResult.Succeeded,
                Errors = addRoleIdentityResult.Errors.Select(e => e.Description)
            };
        }

        public async Task<ApplicationUser?> GetFromUsername(string username)
        {
            ApplicationUserDbo landlord = await appDbContext.Users
                .Include(u => u.Houses)
                .FirstOrDefaultAsync(u => u.UserName == username)
                .ConfigureAwait(false);

            if (!await userManager.IsInRoleAsync(landlord, Roles.Landlord).ConfigureAwait(false))
            {
                return null;
            }

            return landlord;
        }
    }
}