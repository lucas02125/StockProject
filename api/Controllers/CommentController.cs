using api.Data;
using api.Interface;
using api.Dto.Comment;
using Microsoft.AspNetCore.Mvc;
using api.Mapper;
using api.Dto;
using api.Models;
using Microsoft.AspNetCore.Identity;
using api.Extensions;
using Microsoft.AspNetCore.Authorization;
using api.Helpers;

namespace api.Controllers
{

    //API controllers are compared to doors to a house, they are mainly the first entry point into things, 
    //Handle the http request coming in from the client
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fmpService;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager, IFMPService fMPService)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _fmpService = fMPService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllComments([FromQuery] CommentQueryObject queryObject)
        {
            var comments = await _commentRepo.GetAllCommentsAsync(queryObject);

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

        [HttpPost("{symbol}")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromRoute] string symbol, [FromBody] CreateCommentDto createComment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var foundStock = await _stockRepo.GetStockBySymbolAsync(symbol);

            if (foundStock == null)
            {
                foundStock = await _fmpService.GetStockBySymbolAsync(symbol);
                if (foundStock == null)
                {
                    return BadRequest("This stock does not exist");
                }
                else
                {
                    await _stockRepo.CreateTheStockAsync(foundStock);
                }
            }


            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);

            var commentModel = createComment.toCreateCommentDto(foundStock.StockID);
            commentModel.AppUserId = appUser.Id;
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

            return Ok(commentToRemove);
        }

    }
}