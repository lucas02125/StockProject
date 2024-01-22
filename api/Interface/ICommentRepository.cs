using api.Dto;
using api.Models;

namespace api.Interface
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllCommentsAsync();

        public Task<Comment?> GetCommentByIDAsync(int id);

        public Task<Comment?> CreateCommentForStock(Comment commentEntity);

        public Task<Comment?> UpdateCommentFromStock(int commentID, UpdateCommentDto updateComment);

        public Task<Comment?> RemoveComment(int id);
    }
}