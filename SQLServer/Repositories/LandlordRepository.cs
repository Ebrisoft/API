using Abstractions;
using Abstractions.Models;
using Abstractions.Models.Results;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using SQLServer.Models.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class LandlordRepository : ILandlordRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUserDbo> userManager;

        //  Constructors
        //  ============

        public LandlordRepository(AppDbContext context, UserManager<ApplicationUserDbo> userManager)
        {
            this.context = context;
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
            ApplicationUserDbo landlord = await context.Users
                .Include(u => u.Houses)
                .FirstOrDefaultAsync(u => u.UserName == username)
                .ConfigureAwait(false);

            if (!await userManager.IsInRoleAsync(landlord, Roles.Landlord).ConfigureAwait(false))
            {
                return null;
            }

            return landlord;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllTenants(string username)
        {
            IEnumerable<ApplicationUserDbo> results = await context.Users
                                                        .Include(u => u.House)
                                                            .ThenInclude(h => h!.Landlord)
                                                        .Where(u => u.House != null && u.House.Landlord.UserName == username)
                                                        .ToListAsync()
                                                        .ConfigureAwait(false);
            return results;
        }

        public async Task<bool> DoesOwnHouseForIssue(string username, int issueId)
        {
            Issue? issue = await context.Issues
                                    .Include(i => i.House)
                                        .ThenInclude(h => h.Landlord)
                                    .FirstOrDefaultAsync(i => i.Id == issueId)
                                    .ConfigureAwait(false);

            if (issue == null)
            {
                return false;
            }

            return username == issue.House.Landlord.UserName;
        }
    }
}