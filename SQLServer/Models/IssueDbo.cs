using Abstractions.Models;
using System.ComponentModel.DataAnnotations;

namespace SQLServer.Models
{
    public class IssueDbo : Issue
    {
        [Key]
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Content { get => base.Content; set => base.Content = value; }

        public new HouseDbo House { get => (HouseDbo)base.House; set => base.House = value; }
    }
}