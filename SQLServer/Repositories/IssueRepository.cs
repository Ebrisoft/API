using Abstractions.Models;
using Abstractions.Repositories;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SQLServer.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;

        //  Constructors
        //  ============

        public IssueRepository(AppDbContext context)
        {
            this.context = context;
        }

        //  Methods
        //  =======

        public async Task<bool> CreateIssue(string content)
        {
            context.Issues.Add(new Models.Issue
            {
                Content = content
            });

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return false;
            }

            return true;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IEnumerable<Abstractions.Models.Issue>> GetAllIssues()
        {
            return context.Issues;
        }

        public async Task<Abstractions.Models.Issue> GetIssueById(int id)
        {
            return context.Issues.FirstOrDefault(i => i.Id == id);
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}