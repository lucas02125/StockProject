namespace api.Dto.Comment
{


    public class CommentDto
    {
        public int CommentID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public int? StockID { get; set; }

    }
}