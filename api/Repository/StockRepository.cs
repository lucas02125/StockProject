using api.Data;
using api.Dto.Stock;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        //Dependency Injection
        private readonly ApplicationDBContext _dbContext;
        public StockRepository(ApplicationDBContext applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }

        public async Task<Stock> CreateTheStockAsync(Stock stockEntity)
        {
            await _dbContext.AddAsync(stockEntity);
            await _dbContext.SaveChangesAsync();
            return stockEntity;
        }

        public async Task<Stock?> DeleteStockAsync(int stockID)
        {
            var stockToRemove = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.StockID == stockID);
            if (stockToRemove == null)
            {
                return null;
            }

            _dbContext.Stocks.Remove(stockToRemove);
            await _dbContext.SaveChangesAsync();
            return stockToRemove;
        }

        //Repository Pattern as it takes in the abstract method and allows it flexibility to morph into various values
        public async Task<List<Stock>> GetAllStockAsync()
        {
            //SELECT * FROM Stocks INNNER JOIN Comments WHERE Stocks.StockId = Comments.StockId
            return await _dbContext.Stocks.Include(s => s.Comments).ToListAsync();
        }

        public async Task<Stock?> GetSingleStockAsync(int stockID)
        {
            var singleStock = await _dbContext.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(q => q.StockID == stockID);
            if (singleStock == null)
            {
                return null;
            }
            return singleStock;
        }

        public async Task<bool> StockExist(int stockID)
        {
            return await _dbContext.Stocks.AnyAsync(x => x.StockID == stockID);
        }

        public async Task<Stock?> UpdateTheStockAsync(int stockId, UpdateStockDto stockDto)
        {
            var stockToUpdate = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.StockID == stockId);
            if (stockToUpdate == null)
            {
                return null;
            }

            stockToUpdate.Symbol = stockDto.Symbol;
            stockToUpdate.CompanyName = stockDto.CompanyName;
            stockToUpdate.Purchase = stockDto.Purchase;
            stockToUpdate.LastDiv = stockDto.LastDiv;
            stockToUpdate.Industry = stockDto.Industry;
            stockToUpdate.MarketCap = stockDto.MarketCap;

            await _dbContext.SaveChangesAsync();
            return stockToUpdate;

        }
    }
}