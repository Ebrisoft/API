namespace Abstractions.Models
{
    public interface IIssue
    {
        //  Properties
        //  ==========

        public int Id { get; set; }
        public string Content { get; set; }
    }
}