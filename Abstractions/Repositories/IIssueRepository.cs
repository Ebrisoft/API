using Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface IIssueRepository
    {
        //  Methods
        //  =======

        public Task<IEnumerable<IIssue>> GetAllIssues();
        public Task<IIssue> GetIssueById(int id);
        public Task<bool> CreateIssue(string content);
    }
}