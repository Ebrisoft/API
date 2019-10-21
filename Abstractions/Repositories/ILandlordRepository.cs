using Abstractions.Models;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ILandlordRepository
    {
        //  Methods
        //  =======

        public Task<IRegisterLandlordResult> Register(string username, string email, string password);
        public Task<bool> SignIn(string username, string password);
        public Task SignOut();
    }
}