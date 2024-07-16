using api.Interface;
using api.Mapper;
using api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _configuration = config;
        }

        public async Task<Stock> GetStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["FMPKey"]}");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];
                    if (stock != null)
                    {
                        return stock.toStockFromFMP();
                    }
                    return null;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}