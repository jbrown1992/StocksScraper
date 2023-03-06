using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StocksScraper.Logic
{
    public class YahooFinanceLogic : IYahooFinanceLogic
    {
        public YahooFinanceLogic()
        {

        }

        public async Task<IEnumerable<string>> GetYahooFinanceData(string ticker)
        {
            var url = $"https://finance.yahoo.com/quote/{ticker}/financials?p={ticker}";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var parsed = ParseHtml(response);
            return parsed;
        }

        private List<string> ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var rows = htmlDoc.DocumentNode.SelectNodes("//div[@data-test='fin-row']");

            List<string> allNodes = new List<string>();
            List<string> revenueTable = new List<string>();

            var count = 0;

            foreach (var row in rows)
            {
                var tableChildNodes = row.ChildNodes[0];

                count = tableChildNodes.ChildNodes.Count;


                foreach (var item in tableChildNodes.ChildNodes)
                {
                    //TODO
                    allNodes.Add(item.InnerText.Replace("&amp;", "And").Replace(",", ""));
                }
            }

            for (int i = 0; i < allNodes.Count; i+=count)
            {

                var dataString = "";

                for (int j = 0; j < count; j++)
                {
                    dataString = dataString + allNodes[i+j];

                    if(j != count-1)
                    {
                        dataString = dataString + ",";
                    }
                }

                revenueTable.Add(dataString);

            }

            return revenueTable;
        }
    }
}
