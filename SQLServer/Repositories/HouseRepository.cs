using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;

        //  Constructors
        //  ============

        public HouseRepository(AppDbContext context)
        {
            this.context = context;
        }

        //  Methods
        //  =======

        public async Task<House?> CreateHouse(string landlordUsername, string name)
        {
            HouseDbo newHouse = new HouseDbo
            {
                Name = name,
                Issues = new List<IssueDbo>()
            };

            context.Houses.Add(newHouse);

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return newHouse;
        }

        public async Task<House?> FindById(int id)
        {
            return await context.Houses
                .Include(h => h.Issues)
                .FirstOrDefaultAsync(h => h.Id == id)
                .ConfigureAwait(false);
        }
    }
}