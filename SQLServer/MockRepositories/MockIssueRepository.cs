using Abstractions.Models;
using Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace SQLServer.MockRepositories
{
    public class MockIssueRepository : IIssueRepository
    {
        //  Variables
        //  =========

        private readonly List<IIssue> issues;

        //  Constructors
        //  ============

        public MockIssueRepository()
        {
            issues = new List<IIssue>
            {
                new Models.Issue
                {
                    Content = "This is an issue"
                },
                new Models.Issue
                {
                    Content = "This is also an issue"
                }
            };
        }

        public async Task<IEnumerable<IIssue>> GetAllIssues()
        {
            return issues;
        }
    }
}