using api.Data;
using api.Interface;
using api.Dto.Comment;
using Microsoft.AspNetCore.Mvc;
using api.Mapper;
using api.Dto;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var foundComment = await _commentRepo.GetCommentByIDAsync(id);
            if (foundComment == null)
            {
                return NotFound();
            }
            return Ok(foundComment);
        }

        [HttpPost("{stockID:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockID, [FromBody] CreateCommentDto createComment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var foundStock = await _stockRepo.StockExist(stockID);
            if (foundStock == false)
            {
                return BadRequest("Stock doesnt exist");
            }

            var commentModel = createComment.toCreateCommentDto(stockID);
            await _commentRepo.CreateCommentForStock(commentModel);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.CommentID }, commentModel.toCommentDto());
        }

        [HttpPut("{commentID:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int commentID, [FromBody] UpdateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentToUpdate = await _commentRepo.UpdateCommentFromStock(commentID, commentDto);
            if (commentToUpdate == null)
            {
                return BadRequest("Comment doesn't exist");
            }

            return Ok(commentToUpdate.toCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentToRemove = await _commentRepo.RemoveComment(id);
            if (commentToRemove == null)
            {
                return BadRequest("Comment does not exist you fool");
            }

            return await GetAllComments();
        }

    }
}