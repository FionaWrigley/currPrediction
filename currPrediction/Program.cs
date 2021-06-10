using System;
using System.Threading.Tasks;

//use newtonsoft to convert json to c# objects.
//using Newtonsoft.Json.Linq;

namespace currPrediction
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Crypto currency forecaster");
            //CryptoDataFetcher bob = new CryptoDataFetcher("bitcoin");
            await CryptoDataFetcher.GetCryptoData("USD", "BTC");

        }

    }
}
