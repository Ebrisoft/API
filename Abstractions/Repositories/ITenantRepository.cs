using Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ITenantRepository
    {
        //  Methods
        //  =======

        public Task<IRegisterTenantResult> RegisterTenant(string username, string email, string password);
        public Task<bool> SignInTenant(string username, string password);
    }
}