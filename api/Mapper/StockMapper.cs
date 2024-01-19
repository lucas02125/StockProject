using api.Dto.Comment;
using api.Dto.Stock;
using api.Models;

namespace api.Mapper
{
    public static class StockMapper
    {

        //Need to make an extension class using the dto object
        public static StockDto toStockDto(this Stock stockDto)
        {
            return new StockDto()
            {
                StockID = stockDto.StockID,
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                Comments = stockDto.Comments.Select(c => c.toCommentDto()).ToList()
            };
        }

        public static Stock toCreateRequestDto(this CreateStockRequestDto createStock)
        {
            return new Stock
            {
                Symbol = createStock.Symbol,
                CompanyName = createStock.CompanyName,
                Purchase = createStock.Purchase,
                LastDiv = createStock.LastDiv,
                Industry = createStock.Industry,
                MarketCap = createStock.MarketCap
            };
        }
    }
}