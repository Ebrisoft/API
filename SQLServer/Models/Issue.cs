using Abstractions.Models;
using System.ComponentModel.DataAnnotations;

namespace SQLServer.Models
{
    public class Issue : IIssue
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; } = null!;
    }
}