using System.ComponentModel.DataAnnotations;

namespace api.Dto.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title cannot be less than 5 characters")]
        [MaxLength(255, ErrorMessage = "Title cannot be larger than 255 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content cannot be less than 5 characters")]
        [MaxLength(255, ErrorMessage = "Content cannot be larger than 255 characters")]
        public string Content { get; set; } = string.Empty;

    }
}