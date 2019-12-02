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
        private readonly ILandlordRepository landlordRepository;
        private readonly ITenantRepository tenantRepository;

        //  Constructors
        //  ============

        public ContactRepository(AppDbContext context, IHouseRepository houseRepository, ILandlordRepository landlordRepository, ITenantRepository tenantRepository)
        {
            this.context = context;
            this.houseRepository = houseRepository;
            this.landlordRepository = landlordRepository;
            this.tenantRepository = tenantRepository;
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

        public async Task<IEnumerable<Contact>> GetPhonebook(string username)
        {
            ApplicationUser? user = await landlordRepository.GetFromUsername(username).ConfigureAwait(false);

            if (user != null)
            {
                IEnumerable<Contact> landlordResults = await context.Contacts
                                                        .Include(c => c.House)
                                                            .ThenInclude(h => h.Landlord)
                                                        .Where(c => c.House.Landlord.UserName == username)/*
                                                        .Union(context.Users
                                                            .Include(u => u.House)
                                                                .ThenInclude(h => h!.Landlord)
                                                            .Where(u => u.House != null)
                                                            .Select(u => new Contact
                                                            {
                                                                House = u.House!,
                                                                Name = u.Name,
                                                                Email = u.Email,
                                                                PhoneNumber = u.PhoneNumber
                                                            }))*/
                                                        .ToListAsync()
                                                        .ConfigureAwait(false);
                return landlordResults;
            }
            
            user = await tenantRepository.GetFromUsername(username).ConfigureAwait(false);

            if (user == null || user.House == null)
            {
                return new List<Contact>();
            }

            IEnumerable<Contact> tenantResults = await context.Contacts
                                                    .Include(c => c.House)
                                                    .Where(c => c.House.Id == user.House.Id)
                                                    .ToListAsync()
                                                    .ConfigureAwait(false);
            return tenantResults;
        }
    }
}