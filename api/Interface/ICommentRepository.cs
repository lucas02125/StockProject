using api.Models;

namespace api.Interface
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllCommentsAsync();

        public Task<Comment?> GetCommentByIDAsync(int id);

        public Task<Comment?> CreateCommentForStock(Comment commentEntity);
    }
}