using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _commentContext;
        public CommentRepository(ApplicationDBContext applicationDBContext)
        {
            _commentContext = applicationDBContext;
        }

        public async Task<Comment?> CreateCommentForStock(Comment commentEntity)
        {
            await _commentContext.AddAsync(commentEntity);
            await _commentContext.SaveChangesAsync();
            return commentEntity;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _commentContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIDAsync(int id)
        {
            var commentFound = await _commentContext.Comments.FirstOrDefaultAsync(c => c.CommentID == id);
            if (commentFound == null)
            {
                return null;
            }

            return commentFound;

        }
    }
}