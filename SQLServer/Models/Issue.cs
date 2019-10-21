using System.ComponentModel.DataAnnotations;

namespace SQLServer.Models
{
    public class Issue : Abstractions.Models.Issue
    {
        [Key]
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Content { get => base.Content; set => base.Content = value; }
    }
}