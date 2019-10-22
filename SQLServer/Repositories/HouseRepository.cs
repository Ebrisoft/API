using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Abstractions.Models.House?> CreateHouse(string landlordUsername, string name)
        {
            House newHouse = new House
            {
                Name = name,
                Issues = new List<Issue>()
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

        public async Task<Abstractions.Models.House?> FindById(int id)
        {
            return await context.Houses
                .Include(h => h.Issues)
                .FirstOrDefaultAsync(h => h.Id == id)
                .ConfigureAwait(false);
        }
    }
}