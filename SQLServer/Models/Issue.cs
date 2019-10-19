using Abstractions.Models;

namespace SQLServer.Models
{
    public class Issue : IIssue
    {
        public string Content { get; set; } = null!;
    }
}