using api.Dto.Stock;
using api.Interface;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{


    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {

        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStock()
        {
            var stocks = await _stockRepo.GetAllStockAsync();

            var stockdtos = stocks.Select(s => s.toStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        //Model Binding
        public async Task<IActionResult> GetSingleStock([FromRoute] int id)
        {
            var singleStock = await _stockRepo.GetSingleStockAsync(id);

            if (singleStock == null)
            {
                return NotFound();
            }

            return Ok(singleStock);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto createStock)
        {
            var stockModel = createStock.toCreateRequestDto();
            await _stockRepo.CreateTheStockAsync(stockModel);

            return CreatedAtAction(nameof(GetSingleStock), new { id = stockModel.StockID }, stockModel.toStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStock)
        {
            var stockToUpdate = await _stockRepo.UpdateTheStockAsync(id, updateStock);
            if (stockToUpdate == null)
            {
                return NotFound();
            }

            return Ok(stockToUpdate.toStockDto());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStock([FromRoute] int id)
        {
            var stockToRemove = await _stockRepo.DeleteStockAsync(id);
            if (stockToRemove == null)
            {
                return NotFound();
            }

            return await GetAllStock();
            //return NoContent();
            //return NoContent() does same thing returns 204

        }
    }
}