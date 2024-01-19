using api.Data;
using api.Interface;
using api.Dto.Comment;
using Microsoft.AspNetCore.Mvc;
using api.Mapper;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepo.GetAllCommentsAsync();

            var commentDtos = comments.Select(c => c.toCommentDto());

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var foundComment = await _commentRepo.GetCommentByIDAsync(id);
            if (foundComment == null)
            {
                return NotFound();
            }
            return Ok(foundComment);
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockID, [FromBody] CreateCommentDto createComment)
        {
            var foundStock = await _stockRepo.StockExist(stockID);
            if (foundStock == false)
            {
                return BadRequest("Stock doesnt exist");
            }

            var commentModel = createComment.toCreateCommentDto(stockID);
            await _commentRepo.CreateCommentForStock(commentModel);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel }, commentModel.toCommentDto());
        }

    }
}