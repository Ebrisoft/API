using Abstractions.Models;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface IHouseRepository
    {
        //  Methods
        //  =======
        public Task<House?> CreateHouse(string landlordUsername, string content);
        public Task<House?> FindById(int id);
    }
}