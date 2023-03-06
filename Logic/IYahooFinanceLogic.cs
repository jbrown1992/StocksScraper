
namespace StocksScraper.Logic
{
    public interface IYahooFinanceLogic
    {
        Task<IEnumerable<string>> GetIncomeStatement(string ticker);
        Task<IEnumerable<string>> GetBalanceSheet(string ticker);
        Task<IEnumerable<string>> GetCashFlowStatement(string ticker);


    }
}