using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace currPrediction
{
    public class CryptoDataFetcher
    {
        private const string V = "https://crypto-asset-market-data-unified-apis-for-professionals.p.rapidapi.com/api/v1/blockfacts/price/snapshot?";
        private static readonly HttpClient client = new HttpClient();

        //Define the constructor of DataFetcher which is the same name as class, and is not returning anything.
        //Will take a string name, and url as a argument.
        public CryptoDataFetcher(string name)
        {
            Name = name;
           // Denominator = denominator;
           // Asset = asset;

        }

       // public static object Denomiator { get; private set; }

        //Properties are auto-implemented.
        public string Name { get; set; }

        public static async Task GetCryptoData(string denominator, string asset)
        {
            string baseUrl = V;
            try
            {
                Console.WriteLine("Fetching " + asset + " data, in " + denominator + ", from " + baseUrl);

                client.DefaultRequestHeaders.Accept.Clear();

                var request = new HttpRequestMessage
                {
                Method = HttpMethod.Get,
                RequestUri = new Uri(baseUrl + "denominator=" + denominator + "&asset=" + asset),
                    Headers =
                 {
                     { "x-api-key", Environment.GetEnvironmentVariable("X-API-KEY")},
                     { "x-api-secret", Environment.GetEnvironmentVariable("X-API-SECRET") },
                     { "x-rapidapi-key", Environment.GetEnvironmentVariable("X-RAPIDAPI-KEY") },
                     { "x-rapidapi-host", Environment.GetEnvironmentVariable("X-RAPIDAPI-HOST") },
                 },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    dynamic body = await response.Content.ReadAsStringAsync();

                    string filepath = @"..\..\..\Data\" + asset+".json";
                    //write string to file
                    System.IO.File.WriteAllText(filepath, body);
                    Console.WriteLine(body);
                }
            }

            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
            }
        }
    }
}
