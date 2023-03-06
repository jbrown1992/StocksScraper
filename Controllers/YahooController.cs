using Microsoft.AspNetCore.Mvc;
using StocksScraper.Logic;
using System.Collections.Generic;

namespace StocksScraper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YahooController : Controller
    {
        private readonly IYahooFinanceLogic _yahooFinanceLogic;

        public YahooController (IYahooFinanceLogic yahooFinanceLogic)
        {
            _yahooFinanceLogic = yahooFinanceLogic;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get(string ticker)
        {
           var result = await _yahooFinanceLogic.GetYahooFinanceData(ticker);

           return result;
        }
    }
}
