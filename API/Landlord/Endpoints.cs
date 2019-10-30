namespace API.Landlord
{
    public static class Endpoints
    {
        private const string Base = "api/v1/landlord/";

        //  Feed

        public const string GetFeed = Base + "getfeed";

        //  Issue

        public const string GetIssue = Base + "getissue";
        public const string CreateIssue = Base + "createissue";

        //  House

        public const string CreateHouse = Base + "createhouse";
        public const string GetHouse = Base + "gethouse";

        //  Landlord

        public const string Register = Base + "register";
        public const string SignIn = Base + "signin";
        public const string SignOut = Base + "signout";

        //  Tenant

        public const string AddTenant = Base + "addtenant";
    }
}