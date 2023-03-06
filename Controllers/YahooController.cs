using Microsoft.AspNetCore.Mvc;
using StocksScraper.Logic;
using System.Collections.Generic;

namespace StocksScraper.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class YahooController : Controller
    {
        private readonly IYahooFinanceLogic _yahooFinanceLogic;

        public YahooController (IYahooFinanceLogic yahooFinanceLogic)
        {
            _yahooFinanceLogic = yahooFinanceLogic;
        }

        [HttpGet]
        [ActionName("IncomeStatement")]
        public async Task<IEnumerable<string>> GetIncomeStatement(string ticker)
        {
           var result = await _yahooFinanceLogic.GetIncomeStatement(ticker);

           return result;
        }

        [HttpGet]
        [ActionName("BalanceSheet")]
        public async Task<IEnumerable<string>> GetBalanceSheet(string ticker)
        {
            var result = await _yahooFinanceLogic.GetBalanceSheet(ticker);

            return result;
        }

        [HttpGet]
        [ActionName("CashFlowStatement")]
        public async Task<IEnumerable<string>> GetCashFlowStatement(string ticker)
        {
            var result = await _yahooFinanceLogic.GetCashFlowStatement(ticker);

            return result;
        }
    }
}
