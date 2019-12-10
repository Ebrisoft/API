using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;

        //  Constructors
        //  ============

        public CommentRepository(AppDbContext context)
        {
            this.context = context;
        }

        //  Methods
        //  =======

        public async Task<Comment?> CreateComment(string content, ApplicationUser author, Issue issue)
        {
            CommentDbo newComment = new CommentDbo
            {
                Content = content,
                Author = (ApplicationUserDbo)author,
                Issue = (IssueDbo)issue
            };

            context.Comments.Add(newComment);

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return newComment;
        }
    }
}
