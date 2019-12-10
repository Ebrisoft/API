using Abstractions.Models;
using Abstractions.Models.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ILandlordRepository
    {
        //  Methods
        //  =======

        public Task<IRegisterLandlordResult> Register(string email, string password, string phoneNumber, string name);
        public Task<ApplicationUser?> GetFromUsername(string username);
        public Task<IEnumerable<ApplicationUser>> GetAllTenants(string username);
        public Task<bool> DoesOwnHouseForIssue(string username, int issueId);
    }
}