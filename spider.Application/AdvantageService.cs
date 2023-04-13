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
using System.Threading.Tasks;

namespace spider
{
    public class AdvantageService : IAdvantageService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public AdvantageService(
            HttpClient client,
            IConfiguration configuration
            )
        {
            _config = configuration;
            client.BaseAddress = new Uri(_config["Advantage:Base"]!);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }
        public IEnumerable<InvoiceHeader> getInvoices()
        {
            using (Stream s = _client.GetStreamAsync(_config["Advantage:Invoices"]+"?Start=2023-01-04&End=2023-01-04&Codes=Т-р").Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var InvoiceList = serializer.Deserialize<IEnumerable<InvoiceHeader>>(reader);
                return InvoiceList;
            };
        }
    }
}
