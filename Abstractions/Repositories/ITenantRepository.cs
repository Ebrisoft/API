using Abstractions.Models;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ITenantRepository
    {
        //  Methods
        //  =======

        public Task<IRegisterTenantResult> RegisterTenant(string username, string email, string password);
    }
}