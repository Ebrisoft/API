using Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SQLServer;
using SQLServer.Models;
using System;
using System.Threading.Tasks;

namespace API
{
    internal class DataGenerator
    {
        /// <exception cref="DbUpdateException">Ignore.</exception>
        /// <exception cref="DbUpdateConcurrencyException">Ignore.</exception>
        internal static async Task Initialize(IServiceProvider services)
        {
            using AppDbContext context = new AppDbContext(services.GetRequiredService<DbContextOptions<AppDbContext>>());
            using UserManager<ApplicationUserDbo> userManager = services.GetRequiredService<UserManager<ApplicationUserDbo>>();
            using RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);

            await roleStore.CreateAsync(new IdentityRole
            {
                Name = Roles.Tenant,
                NormalizedName = Roles.Tenant.ToUpperInvariant()
            }).ConfigureAwait(false);
            await roleStore.CreateAsync(new IdentityRole
            {
                Name = Roles.Landlord,
                NormalizedName = Roles.Landlord.ToUpperInvariant()
            }).ConfigureAwait(false);
            await TrySave(context).ConfigureAwait(false);
        }

        private static async Task TrySave(AppDbContext context)
        {
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {

            }
        }
    }
}