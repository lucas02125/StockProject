using api.Dto.Comment;
using api.Models;

namespace api.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto toCommentDto(this Comment comment)
        {
            return new CommentDto()
            {
                CommentID = comment.CommentID,
                Title = comment.Title,
                Content = comment.Title,
                CreatedOn = comment.CreatedOn,
                CreatedBy = comment.AppUser.UserName,
                StockID = comment.StockID,
            };
        }

        public static Comment toCreateCommentDto(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Content = commentDto.Content,
                Title = commentDto.Title,
                StockID = stockId
            };
        }
    }
}