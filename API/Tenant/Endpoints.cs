namespace API.Tenant
{
    public static class Endpoints
    {
        private const string Base = "api/v1/tenant/";

        //  Feed

        public const string GetFeed = Base + "getfeed";

        //  Issue

        public const string GetIssue = Base + "getissue";
        public const string CreateIssue = Base + "createissue";

        //  Tenant

        public const string Register = Base + "register";
        public const string SignIn = Base + "signin";
        public const string SignOut = Base + "signout";
    }
}