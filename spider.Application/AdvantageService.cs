using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using spider.AdvantageModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace spider
{
    public class AdvantageService : IAdvantageService
    {
        private readonly HttpClient _advantageClient;
        private readonly HttpClient _locarusClient;
        private readonly HttpClient _counterpartyClient;
        private readonly IConfiguration _config;

        public AdvantageService(
            HttpClient client,
            IConfiguration configuration
            )
        {
            _config = configuration;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _advantageClient = client;
            _locarusClient = client;
            _counterpartyClient = client;
            _advantageClient.BaseAddress = new Uri(_config["Advantage:Base"]);
            _counterpartyClient.BaseAddress = new Uri(_config["Counterparty:Base"]);
            _locarusClient.BaseAddress = new Uri(_config["Locarus:Base"]);
        }
        public IEnumerable<InvoiceHeader> getInvoices()
        {
            using (Stream s = _advantageClient.GetStreamAsync(_config["Advantage:Invoices"] + "?Start=2023-01-04&End=2023-01-04&Codes=Т-р").Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var InvoiceList = serializer.Deserialize<IEnumerable<InvoiceHeader>>(reader);
                return InvoiceList;
            };
        }

        public IEnumerable<Counterparty> getCounterparties()
        {
            using (Stream s = _counterpartyClient.GetStreamAsync(_config["Counterparty:All"]).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                };
                var CounterpartyList = serializer.Deserialize<IEnumerable<Counterparty>>(reader);
                return CounterpartyList;
            };
        }

        public IEnumerable<Car> GetCars()
        {
            using (Stream s = _locarusClient.GetStreamAsync(_config["Locarus:All"]).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var CarList = serializer.Deserialize<IEnumerable<Car>>(reader);
                List<Car> CarResult= new List<Car>();

                Regex regex = new Regex(@"\d");

                foreach (var Car in CarList)
                {
                    if (regex.IsMatch(Car.number)) CarResult.Add(Car);
                }                
                return CarResult;
            };
        }
    }
}
