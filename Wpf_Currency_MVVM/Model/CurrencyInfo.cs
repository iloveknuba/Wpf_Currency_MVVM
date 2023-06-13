using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Currency_MVVM.Model
{
    public class CurrencyInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public string price { get; set; }
        public string symbol { get; set; }
        public string volume24h { get; set; }
      

      /*  public static CurrencyInfo GetCurrencyInfo()
        {
            var currencyInfo = new CurrencyInfo()
            {
                id = "bitcoin",
                rank = 1,
                name = "Bitcoin",
                symbol = "BTC",
              
                markets = new List<Market>() { new Market() { name = "Binance", price = 6200 }
                , new Market() { name = "Huobu", price = 6500 } }
            };

                
            return currencyInfo;

        }
      */
       
    }
       
}
