using Abstractions.Models;
using Abstractions.Repositories;
using SQLServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace SQLServer.MockRepositories
{
    public class MockIssueRepository : IIssueRepository
    {
        //  Variables
        //  =========

        private readonly List<IIssue> issues;
        private int currentId;

        //  Constructors
        //  ============

        public MockIssueRepository()
        {
            issues = new List<IIssue>
            {
                new Issue
                {
                    Id = 0,
                    Content = "This is an issue"
                },
                new Issue
                {
                    Id = 1,
                    Content = "This is also an issue"
                }
            };

            currentId = issues.Count;
        }

        //  Methods
        //  =======

        public async Task<IEnumerable<IIssue>> GetAllIssues()
        {
            return issues;
        }

        public async Task<IIssue> GetIssueById(int id)
        {
            return issues.FirstOrDefault(i => i.Id == id);
        }

        public async Task<bool> CreateIssue(string content)
        {
            issues.Add(new Issue
            {
                Id = currentId,
                Content = content
            });

            currentId++;

            return true;
        }
    }
}