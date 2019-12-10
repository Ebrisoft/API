namespace API.Landlord.Request
{
    public class CreateComment
    {
        //  Properties
        //  ==========

        public int IssueId { get; set; }
        public string Content { get; set; } = null!;
    }
}
