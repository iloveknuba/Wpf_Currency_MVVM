using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Collections.ObjectModel;

namespace Wpf_Currency_MVVM.Model
{
    public class APIRequests
    {
       
        public static async Task<List<string>> GetSearchList()
        {
           var list = new List<string>();
          
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.coincap.io/v2/assets");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        JObject jsonObject = JObject.Parse(responseBody);

                        // Отримати масив об'єктів валют
                        JArray data = (JArray)jsonObject["data"];
                        if (data != null)
                        {
                            // Пройтися по кожному об'єкту валюти
                            foreach (JToken currency in data)
                            {

                                list.Add(currency["name"].ToString()); 
                                
                            }
                           
                        }

                    }
                }
                catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            return list;
            

        }
        public static async Task<CurrencyInfo> GetCurrencyInfo(string currencyId)
        {
            var currencyInfo = new CurrencyInfo();
        
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage assetResponse = await client.GetAsync($"https://api.coincap.io/v2/assets/{currencyId}");
            
                    if (assetResponse.IsSuccessStatusCode)
                    {
                        var assetResponseBody = await assetResponse.Content.ReadAsStringAsync();
                     

                        JObject assetJsonObject = JObject.Parse(assetResponseBody);
                     

                        // Отримати масив об'єктів валют
                        JToken data = (JToken)assetJsonObject["data"];
                       

                        if (data != null)
                        {
                           
                            currencyInfo.price = (string)data["priceUsd"];

                            currencyInfo.name = (string)data["name"];
                            currencyInfo.id = currencyId;
                            currencyInfo.symbol = (string)data["symbol"];

                           
                                currencyInfo.volume24h = (string)data["volumeUsd24Hr"];

                            if (int.TryParse((string)data["rank"], out int rank))
                                currencyInfo.rank = rank;
                        }
                      
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            return currencyInfo;
        }
        public static async Task<ObservableCollection<Market>> GetCurrencyMarkets(string currencyId)
        {
          
            var marketsList = new ObservableCollection<Market>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                   
                    HttpResponseMessage marketsResponse = await client.GetAsync($"https://api.coincap.io/v2/assets/{currencyId}/markets");

                    if ( marketsResponse.IsSuccessStatusCode)
                    {
                       
                        var marketsResponseBody = await marketsResponse.Content.ReadAsStringAsync();

                     
                        JObject marketsJsnoObject = JObject.Parse(marketsResponseBody);

                        // Отримати масив об'єктів валют
                      
                        JArray markets = (JArray)marketsJsnoObject["data"];

                     
                        if (markets != null)
                        {
                            foreach (var market in markets)
                            {
                                if (market["exchangeId"] != null && market["priceUsd"] != null)
                                {                                   
                                      marketsList.Add(new Market() { name = market["exchangeId"].ToString(), price = market["priceUsd"].ToString() });
                                }
                            }
                           
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            return marketsList;
        }



        public static async Task<string> GetCurrencyId(string currencyName)
        {
           
            string currencyId = "";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage assetResponse = await client.GetAsync("https://api.coincap.io/v2/assets");
           
                    if (assetResponse.IsSuccessStatusCode)
                    {
                        var responseBody = await assetResponse.Content.ReadAsStringAsync();

                        JObject jsonObject = JObject.Parse(responseBody);

                        // Отримати масив об'єктів валют
                        JArray data = (JArray)jsonObject["data"];
                        if (data != null)
                        {
                            // Пройтися по кожному об'єкту валюти
                            foreach (JToken currency in data)
                            {
                                if (currency["name"].ToString() == currencyName) currencyId = currency["id"].ToString();
                                
                               

                            }

                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            return currencyId;

        }
       
        public static async Task<List<string>> GetTopNCurrencies(int count)
        {
            List<string> topNCurrencies = new List<string>();

            // Створити екземпляр HttpClient
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    // Виконати GET-запит до API
                    HttpResponseMessage response = await client.GetAsync("https://api.coincap.io/v2/assets");

                    // Перевірити статус-код відповіді
                    if (response.IsSuccessStatusCode)
                    {
                        // Отримати вміст відповіді як рядок
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Розпарсити JSON
                        JObject jsonObject = JObject.Parse(responseBody);

                        // Отримати масив об'єктів валют
                        JArray data = (JArray)jsonObject["data"];
                        if (data != null)
                        {
                            // Пройтися по кожному об'єкту валюти
                            foreach (JToken currency in data)
                            {
                                // Отримати значення поля "rank"
                                int rank = (int)currency["rank"];

                                // Додати валюту до списку, якщо поле "rank" менше або дорівнює 10
                                if (rank <= count)
                                {
                                    topNCurrencies.Add(currency["name"].ToString());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);
                }
            }

            return topNCurrencies;
        }
    }
}
