using api.Dto.Stock;
using api.Helpers;
using api.Models;

namespace api.Interface
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllStockAsync(QueryObject queryObject);
        public Task<Stock?> GetSingleStockAsync(int stockID);
        public Task<Stock> CreateTheStockAsync(Stock stockEntity);
        public Task<Stock?> UpdateTheStockAsync(int stockId, UpdateStockDto stockDto);
        public Task<Stock?> DeleteStockAsync(int stockID);
        public Task<bool> StockExist(int stockID);
    }
}