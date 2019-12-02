using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class ContactRepository : IContactRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;
        private readonly IHouseRepository houseRepository;

        //  Constructors
        //  ============

        public ContactRepository(AppDbContext context, IHouseRepository houseRepository)
        {
            this.context = context;
            this.houseRepository = houseRepository;
        }

        public async Task<Contact?> CreateContact(string name, int houseId, string? phoneNumber, string? email)
        {
            House? house = await houseRepository.FindById(houseId).ConfigureAwait(false);

            if (house == null)
            {
                return null;
            }

            ContactDbo newContact = new ContactDbo
            {
                Name = name,
                House = (HouseDbo)house,
                PhoneNumber = phoneNumber,
                Email = email
            };

            context.Contacts.Add(newContact);

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

            return newContact;
        }

        public async Task<IEnumerable<Contact>> GetContactsForHouse(int houseId)
        {
            IEnumerable<Contact> results = await context.Contacts
                                                .Include(c => c.House)
                                                .Where(c => c.House.Id == houseId)
                                                .ToListAsync()
                                                .ConfigureAwait(false);

            return results;
        }
    }
}
