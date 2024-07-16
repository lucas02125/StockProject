using System.Runtime.InteropServices;
using api.Extensions;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFMPService _fmpService;
        public PortfolioController(UserManager<AppUser> userManager
        , IStockRepository stockRepository, IPortfolioRepository portfolioRepository, IFMPService fMPService)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _fmpService = fMPService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var foundStock = await _stockRepository.GetStockBySymbolAsync(symbol);

            if (foundStock == null)
            {
                foundStock = await _fmpService.GetStockBySymbolAsync(symbol);
                if (foundStock == null)
                {
                    return BadRequest("This stock does not exist");
                }
                else
                {
                    await _stockRepository.CreateTheStockAsync(foundStock);
                }
            }

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Cannot add same stock to portfolio");
            }

            var portfolioModel = new Portfolio
            {
                AppUser = appUser,
                Stock = foundStock,
            };

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create portfolio");
            }

            await _portfolioRepository.CreatePortfolioAsync(portfolioModel);

            return Ok(portfolioModel);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemovePortfolio(string symbol)
        {
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            var filter = userPortfolio.Where(e => e.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (!filter.Any())
            {
                return BadRequest("Portfolio not found");
            }

            var deleted = await _portfolioRepository.DeletePortfolioAsync(appUser, symbol);

            return Ok(deleted);

        }

    }
}