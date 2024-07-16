using api.Dto;
using api.Helpers;
using api.Models;

namespace api.Interface
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllCommentsAsync(CommentQueryObject queryObject);

        public Task<Comment?> GetCommentByIDAsync(int id);

        public Task<Comment?> CreateCommentForStock(Comment commentEntity);

        public Task<Comment?> UpdateCommentFromStock(int commentID, UpdateCommentDto updateComment);

        public Task<Comment?> RemoveComment(int id);
    }
}