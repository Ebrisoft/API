namespace API.Tenant
{
    public static class Endpoints
    {
        private const string TenantBase = "api/v1/tenant/";

        //  Feed

        public const string GetFeed = TenantBase + "getfeed";

        //  Issue

        public const string GetIssue = TenantBase + "getissue";
        public const string CreateIssue = TenantBase + "createissue";

        //  Tenant

        public const string Register = TenantBase + "register";
        public const string SignIn = TenantBase + "signin";
        public const string SignOut = TenantBase + "signout";
    }
}