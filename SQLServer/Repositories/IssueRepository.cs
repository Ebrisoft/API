using Abstractions.Repositories;
using SQLServer.Models;
using System.Collections.Generic;
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
        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public IssueRepository(AppDbContext context, IHouseRepository houseRepository, ILandlordRepository landlordRepository, ITenantRepository tenantRepository)
        {
            this.context = context;
            this.houseRepository = houseRepository;
            this.landlordRepository = landlordRepository;
            this.tenantRepository = tenantRepository;
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

            user = await tenantRepository.GetFromUsername(username).ConfigureAwait(false);
            if(user == null)
            {
                return new List<Issue>();
            }

            return await context.Issues
                .Include(i => i.Author)
                .Include(i => i.House)
                .Where(i => user.House != null && i.House.Id == user.House.Id)
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

        public async Task<bool> Archive(int issueId)
        {
            Issue? issue = await context.Issues
                                .FirstOrDefaultAsync(i => i.Id == issueId)
                                .ConfigureAwait(false);

            if (issue == null)
            {
                return false;
            }

            issue.IsResolved = true;

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

        public async Task<bool> IsAuthor(int issueId, string username)
        {
            Issue? issue = await context.Issues
                                    .Include(i => i.Author)
                                    .FirstOrDefaultAsync(i => i.Id == issueId)
                                    .ConfigureAwait(false);

            if (issue == null)
            {
                return false;
            }

            return username == issue.Author.UserName;
        }
    }
}