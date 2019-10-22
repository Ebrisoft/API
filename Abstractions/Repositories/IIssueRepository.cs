using Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface IIssueRepository
    {
        //  Methods
        //  =======

        public Task<IEnumerable<Issue>> GetAllIssues();
        public Task<Issue?> GetIssueById(int id);
        public Task<bool> CreateIssue(int houseId, string content);
    }
}