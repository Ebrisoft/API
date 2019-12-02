using Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface IHouseRepository
    {
        //  Methods
        //  =======
        public Task<House?> CreateHouse(string landlordUsername, string content);
        public Task<House?> FindById(int id);
        public Task<IEnumerable<House>> GetAllHousesForLandlord(string landlordUsername);
        public Task<bool> DoesHouseBelongTo(int houseId, string landlordUsername);
        public Task<bool> IsInHouse(int houseId, string tenantUsername);
        public Task<bool> AddTenant(int houseId, string tenantUsername);
        public Task<string?> GetPinboard(int houseId);
        public Task<bool> SetPinboard(int houseId, string pinboardContent);
    }
}