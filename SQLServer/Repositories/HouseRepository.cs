using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;
        private readonly ILandlordRepository landlordRepository;

        //  Constructors
        //  ============

        public HouseRepository(AppDbContext context, ILandlordRepository landlordRepository)
        {
            this.context = context;
            this.landlordRepository = landlordRepository;
        }

        //  Methods
        //  =======

        public async Task<House?> CreateHouse(string landlordUsername, string name)
        {
            ApplicationUser? landlord = await landlordRepository.GetFromUsername(landlordUsername).ConfigureAwait(false);

            if (landlord == null)
            {
                return null;
            }

            HouseDbo newHouse = new HouseDbo
            {
                Name = name,
                Issues = new List<IssueDbo>(),
                Landlord = (ApplicationUserDbo)landlord
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

        public async Task<bool> DoesHouseBelongTo(int houseId, string landlordUsername)
        {
            return await context.Houses
                .Include(h => h.Landlord)
                .FirstOrDefaultAsync(h => h.Id == houseId && h.Landlord.UserName == landlordUsername)
                .ConfigureAwait(false) != null;
        }
    }
}