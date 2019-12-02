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
        private readonly ILandlordRepository landlordRepository;
        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public HouseRepository(AppDbContext context, ILandlordRepository landlordRepository, ITenantRepository tenantRepository)
        {
            this.context = context;
            this.landlordRepository = landlordRepository;
            this.tenantRepository = tenantRepository;
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
                Landlord = (ApplicationUserDbo)landlord,
                Pinboard = string.Empty
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

        public async Task<bool> IsInHouse(int houseId, string tenantUsername)
        {
            return await context.Users
                .Include(u => u.House)
                .FirstOrDefaultAsync(u => u.UserName == tenantUsername && u.House != null && u.House.Id == houseId)
                .ConfigureAwait(false) != null;
        }

        public async Task<bool> AddTenant(int houseId, string tenantUsername)
        {
            ApplicationUser? tenant = await tenantRepository.GetFromUsername(tenantUsername).ConfigureAwait(false);

            if (tenant == null || tenant.House != null)
            {
                return false;
            }

            House? house = await FindById(houseId).ConfigureAwait(false);

            if (house == null)
            {
                return false;
            }

            tenant.House = house;

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

        public async Task<string?> GetPinboard(int houseId)
        {
            return (await context.Houses
                .FirstOrDefaultAsync(h => h.Id == houseId)
                .ConfigureAwait(false))?.Pinboard;
        }

        public async Task<bool> SetPinboard(int houseId, string pinboardContent)
        {
            House house = await context.Houses
                .FirstOrDefaultAsync(h => h.Id == houseId)
                .ConfigureAwait(false);

            if (house == null)
            {
                return false;
            }

            house.Pinboard = pinboardContent ?? string.Empty;

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