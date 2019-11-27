namespace API.Landlord.Request
{
    public class CreateIssue
    {
        //  Properties
        //  ==========

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int HouseId { get; set; }
        public int Priority { get; set; }
    }
}