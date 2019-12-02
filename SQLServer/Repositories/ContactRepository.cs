using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
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

        //  Methods
        //  =======

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
                return await GetLandlordPhonebook(username).ConfigureAwait(false);
            }

            return await GetTenantPhonebook(username).ConfigureAwait(false);
        }

        private async Task<IEnumerable<Contact>> GetLandlordPhonebook(string username)
        {
            List<ContactDbo> results = await context.Contacts
                                                    .Include(c => c.House)
                                                        .ThenInclude(h => h.Landlord)
                                                    .Where(c => c.House.Landlord.UserName == username)
                                                    .ToListAsync()
                                                    .ConfigureAwait(false);

            return results.Select(u => new Contact
            {
                Name = u.Name,
                House = u.House!,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            });
        }

        private async Task<IEnumerable<Contact>> GetTenantPhonebook(string username)
        {
            ApplicationUser? user = await tenantRepository.GetFromUsername(username).ConfigureAwait(false);

            if (user == null || user.House == null)
            {
                return new List<Contact>();
            }

            IEnumerable<Contact> results = await context.Contacts
                                                    .Include(c => c.House)
                                                    .Where(c => c.House.Id == user.House.Id)
                                                    .ToListAsync()
                                                    .ConfigureAwait(false);

            return results.Select(u => new Contact
            {
                Name = u.Name,
                House = u.House!,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            });
        }
    }
}