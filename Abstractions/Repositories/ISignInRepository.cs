using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ISignInRepository
    {

        //  Methods
        //  =======

        public Task<IEnumerable<string>?> SignIn(string username, string password);
        public Task SignOut();
    }
}