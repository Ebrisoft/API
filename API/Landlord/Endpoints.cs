namespace API.Landlord
{
    public static class Endpoints
    {
        private const string LandlordBase = "api/v1/landlord/";

        //  Feed

        public const string GetFeed = LandlordBase + "getfeed";

        //  Issue

        public const string GetIssue = LandlordBase + "getissue";
        public const string CreateIssue = LandlordBase + "createissue";

        //  Landlord

        public const string Register = LandlordBase + "register";
        public const string SignIn = LandlordBase + "signin";
        public const string SignOut = LandlordBase + "signout";
    }
}