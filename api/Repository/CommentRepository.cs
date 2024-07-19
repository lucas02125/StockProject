using api.Data;
using api.Dto;
using api.Helpers;
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

        public async Task<List<Comment>> GetAllCommentsAsync(CommentQueryObject queryObject)
        {
            var comments = _commentContext.Comments.Include(a => a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(s => s.Stock.Symbol == queryObject.Symbol);
            }

            if (queryObject.IsDesc == true)
            {
                comments = comments.OrderByDescending(s => s.CreatedOn);
            }

            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIDAsync(int id)
        {
            var commentFound = await _commentContext.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.CommentID == id);
            if (commentFound == null)
            {
                return null;
            }

            return commentFound;

        }

        public async Task<Comment?> RemoveComment(int id)
        {
            var commentToRemove = await _commentContext.Comments.FirstOrDefaultAsync(c => c.CommentID == id);
            if (commentToRemove == null)
            {
                return null;
            }

            _commentContext.Comments.Remove(commentToRemove);
            await _commentContext.SaveChangesAsync();
            return commentToRemove;
        }

        public async Task<Comment?> UpdateCommentFromStock(int commentID, UpdateCommentDto updateComment)
        {
            var commentFound = await _commentContext.Comments.FirstOrDefaultAsync(c => c.CommentID == commentID);
            if (commentFound == null)
            {
                return null;
            }

            commentFound.Title = updateComment.Title;
            commentFound.Content = updateComment.Content;

            await _commentContext.SaveChangesAsync();

            return commentFound;
        }
    }
}