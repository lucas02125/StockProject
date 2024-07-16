using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int CommentID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockID { get; set; }
        //Navigation 
        public Stock? Stock { get; set; }
        //One to One
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}