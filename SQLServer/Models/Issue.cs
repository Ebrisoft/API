using Abstractions.Models;

namespace SQLServer.Models
{
    public class Issue : IIssue
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
    }
}