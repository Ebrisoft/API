using Abstractions.Models;
using Abstractions.Models.Results;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ILandlordRepository
    {
        //  Methods
        //  =======

        public Task<IRegisterLandlordResult> Register(string username, string email, string password);
        public Task<ApplicationUser?> GetFromUsername(string username);
    }
}