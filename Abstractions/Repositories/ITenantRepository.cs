using Abstractions.Models;
using Abstractions.Models.Results;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ITenantRepository
    {
        //  Methods
        //  =======

        public Task<IRegisterTenantResult> RegisterTenant(string email, string password, string phoneNumber, string Name);
        public Task<ApplicationUser?> GetFromUsername(string username);
        public Task<ApplicationUser?> GetLandlord(string username);
    }
}