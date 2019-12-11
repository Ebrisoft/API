using Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface IContactRepository
    {
        //  Methods
        //  =======

        public Task<Contact?> CreateContact(string name, int houseId, string? phoneNumber, string? email);
        public Task<IEnumerable<Contact>> GetPhonebook(string username);
    }
}