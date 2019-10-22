using Abstractions.Repositories;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SQLServer.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;
        private readonly IHouseRepository houseRepository;

        //  Constructors
        //  ============

        public IssueRepository(AppDbContext context, IHouseRepository houseRepository)
        {
            this.context = context;
            this.houseRepository = houseRepository;
        }

        //  Methods
        //  =======

        public async Task<bool> CreateIssue(int houseId, string content)
        {
            Abstractions.Models.House? house = await houseRepository.FindById(houseId).ConfigureAwait(false);

            if (house == null)
            {
                return false;
            }

            context.Issues.Add(new Issue
            {
                Content = content,
                House = (House)house
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

        public async Task<IEnumerable<Abstractions.Models.Issue>> GetAllIssues()
        {
            return await context.Issues
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Abstractions.Models.Issue?> GetIssueById(int id)
        {
            return await context.Issues
                .Include(i => i.House)
                .FirstOrDefaultAsync(i => i.Id == id)
                .ConfigureAwait(false);
        }
    }
}