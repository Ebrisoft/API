using Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface IIssueRepository
    {
        //  Methods
        //  =======

        public Task<IEnumerable<Issue>> GetAllIssues(string username);
        public Task<Issue?> GetIssueById(int id);
        public Task<bool> CreateIssue(string title, string content, House house, ApplicationUser author, int priority = 1);
        public Task<bool> SetPriority(int issueId, int newPriority);
        public Task<bool> Archive(int issueId);
    }
}