using Abstractions.Models;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ITenantRepository
    {
        //  Methods
        //  =======

        public Task<IRegisterTenantResult> RegisterTenant(string username, string email, string password);
        public Task<bool> SignInTenant(string username, string password);
        public Task SignOutTenant();
    }
}