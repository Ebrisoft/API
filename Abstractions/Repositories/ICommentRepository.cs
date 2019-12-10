using Abstractions.Models;
using System.Threading.Tasks;

namespace Abstractions.Repositories
{
    public interface ICommentRepository
    {
        //  Methods
        //  =======

        public Task<Comment?> CreateComment(string content, ApplicationUser author, Issue issue);
    }
}