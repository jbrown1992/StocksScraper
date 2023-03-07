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

        public async Task<IEnumerable<string>> GetIncomeStatement(string ticker)
        {
            var url = $"https://finance.yahoo.com/quote/{ticker}/financials?p={ticker}";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var parsed = ParseFinancialsHtml(response);
            return parsed;
        }

        public async Task<IEnumerable<string>> GetBalanceSheet(string ticker)
        {
            var url = $"https://finance.yahoo.com/quote/{ticker}/balance-sheet?p={ticker}";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var parsed = ParseFinancialsHtml(response);
            return parsed;
        }

        public async Task<IEnumerable<string>> GetCashFlowStatement(string ticker)
        {
            var url = $"https://finance.yahoo.com/quote/{ticker}/cash-flow?p={ticker}";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var parsed = ParseFinancialsHtml(response);
            return parsed;
        }

        public async Task<IEnumerable<string>> GetAnalystEstimates(string ticker)
        {
            var url = $"https://finance.yahoo.com/quote/{ticker}/analysis?p={ticker}";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var parsed = ParseAnalysisHtml(response);
            return parsed;
        }

        private List<string> ParseFinancialsHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            List<string> allNodes = new List<string>();
            List<string> revenueTable = new List<string>();

            var tableHeaders = htmlDoc.DocumentNode.SelectNodes("//div[@class='D(tbhg)']")[0].ChildNodes[0].ChildNodes;
            var dateString = "";

            for (int i = 1; i < tableHeaders.Count; i++)
            {

                if (i == 1)
                {
                    dateString = "Dates,";
                }

                dateString = dateString + tableHeaders[i].InnerText;

                if (i != tableHeaders.Count - 1)
                {
                    dateString = dateString + ",";
                }
            }

            revenueTable.Add(dateString);


            var tableRows = htmlDoc.DocumentNode.SelectNodes("//div[@data-test='fin-row']");




            var count = 0;

            foreach (var row in tableRows)
            {
                var tableChildNodes = row.ChildNodes[0];

                count = tableChildNodes.ChildNodes.Count;


                foreach (var item in tableChildNodes.ChildNodes)
                {
                    //TODO
                    allNodes.Add(item.InnerText.Replace("&amp;", "And").Replace(",", ""));
                }
            }

            for (int i = 0; i < allNodes.Count; i += count)
            {

                var dataString = "";

                for (int j = 0; j < count; j++)
                {
                    dataString = dataString + allNodes[i + j];

                    if (j != count - 1)
                    {
                        dataString = dataString + ",";
                    }
                }

                revenueTable.Add(dataString);

            }

            return revenueTable;
        }
        private List<string> ParseAnalysisHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            List<string> allNodes = new List<string>();
            List<string> revenueTable = new List<string>();

            var tables = htmlDoc.DocumentNode.SelectNodes("//table");
            var dataString = "";

            foreach (var table in tables)
            {

                var headerNodes = table.ChildNodes[0];
                var bodyNodes = table.ChildNodes[1];

                var headers = headerNodes.ChildNodes[0].ChildNodes;

                for (int i = 0; i < headers.Count; i++)
                {
                    dataString = dataString + headers[i].InnerText;


                    if (i != headers.Count - 1)
                    {
                        dataString = dataString + ",";
                    }
                }

                revenueTable.Add(dataString.Replace("&amp;", "&"));
                dataString = "";


                foreach (var bodyChildNode in bodyNodes.ChildNodes)

                {
                    var bodies = bodyChildNode.ChildNodes;

                    for (int i = 0; i < bodies.Count; i++)
                    {
                        dataString = dataString + bodies[i].InnerText;


                        if (i != bodies.Count - 1)
                        {
                            dataString = dataString + ",";
                        }
                    }

                    revenueTable.Add(dataString.Replace("&amp;", "&"));
                    dataString = "";
                }

            }
            return revenueTable;
        }

    }
}
