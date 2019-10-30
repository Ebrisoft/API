namespace Abstractions.Models
{
    public class Issue
    {
        //  Properties
        //  ==========

        public int Id { get; set; }
        public string Content { get; set; } = null!;

        public House House { get; set; } = null!;
    }
}