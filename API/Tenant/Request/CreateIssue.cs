namespace API.Tenant.Request
{
    public class CreateIssue
    {
        //  Properties
        //  ==========

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}