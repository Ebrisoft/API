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
    }
}