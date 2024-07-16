using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PortfolioRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            await _dbContext.AddAsync(portfolio);
            await _dbContext.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolioAsync(AppUser appUser, string symbol)
        {
            var toBeDeleted = await _dbContext.Portfolios.FirstOrDefaultAsync(x => x.AppUser == appUser && x.Stock.Symbol.ToLower() == symbol.ToLower());
            if (toBeDeleted == null)
            { return null; }
            _dbContext.Portfolios.Remove(toBeDeleted);
            await _dbContext.SaveChangesAsync();
            return toBeDeleted;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _dbContext.Portfolios.Where(p => p.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                StockID = stock.Stock.StockID,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap,
            }).ToListAsync();
        }
    }
}