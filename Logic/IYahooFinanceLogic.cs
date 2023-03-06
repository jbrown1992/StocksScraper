
namespace StocksScraper.Logic
{
    public interface IYahooFinanceLogic
    {
        Task<IEnumerable<string>> GetYahooFinanceData(string ticker);
    }
}