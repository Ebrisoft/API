using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Endpoints
{
    public static class TenantEndpoints
    {
        private const string TenantBase = "api/v1/tenant/";

        //  Feed

        public const string GetFeed = TenantBase + "getfeed";

        //  Issue

        public const string GetIssue = TenantBase + "getissue";
        public const string CreateIssue = TenantBase + "createissue";

        //  Tenant

        public const string RegisterTenant = TenantBase + "registertenant";
    }
}