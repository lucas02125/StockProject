using api.Models;

namespace api.Interface
{
    public interface IFMPService
    {
        public Task<Stock> GetStockBySymbolAsync(string symbol);
    }
}