using Abstractions.Repositories;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abstractions.Models;

namespace SQLServer.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;
        private readonly IHouseRepository houseRepository;
        private readonly ILandlordRepository landlordRepository;

        //  Constructors
        //  ============

        public IssueRepository(AppDbContext context, IHouseRepository houseRepository, ILandlordRepository landlordRepository)
        {
            this.context = context;
            this.houseRepository = houseRepository;
            this.landlordRepository = landlordRepository;
        }

        //  Methods
        //  =======

        public async Task<bool> CreateIssue(string title, string content, House house, ApplicationUser author, int priority = 1)
        {
            context.Issues.Add(new IssueDbo
            {
                Title = title,
                Content = content,
                House = (HouseDbo)house,
                Author = (ApplicationUserDbo)author,
                Priority = priority
            });

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Issue>> GetAllIssues(string username)
        {
            ApplicationUser? user = await landlordRepository.GetFromUsername(username).ConfigureAwait(false);

            if (user != null)
            {
                return await context.Issues
                    .Include(i => i.House)
                        .ThenInclude(h => h.Landlord)
                    .Include(i => i.Author)
                    .Where(i => i.House.Landlord.Id == user.Id)
                    .ToListAsync()
                    .ConfigureAwait(false);
            }

#warning Needs refactoring once houses have tenants
            return await context.Issues
                .Include(i => i.Author)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Issue?> GetIssueById(int id)
        {
            return await context.Issues
                .Include(i => i.House)
                .Include(i => i.Author)
                .FirstOrDefaultAsync(i => i.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<bool> SetPriority(int issueId, int newPriority)
        {
            Issue? issue = await GetIssueById(issueId).ConfigureAwait(false);

            if (issue == null)
            {
                return false;
            }

            issue.Priority = newPriority;

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }
    }
}