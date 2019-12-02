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
        public const string SetPriority = Base + "setpriority";

        //  House

        public const string CreateHouse = Base + "createhouse";
        public const string GetHouse = Base + "gethouse";

        //  Pinboard

        public const string GetPinboard = Base + "getpinboard";
        public const string SetPinboard = Base + "setpinboard";

        //  Contact

        public const string CreateContact = Base + "createcontact";

        //  Landlord

        public const string Register = Base + "register";
        public const string SignIn = Base + "signin";
        public const string SignOut = Base + "signout";

        //  Tenant

        public const string AddTenant = Base + "addtenant";
    }
}