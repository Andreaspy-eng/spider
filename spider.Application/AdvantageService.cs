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
        private readonly IConfiguration _config;

        public AdvantageService(
            HttpClient client,
            IConfiguration configuration
            )
        {
            _config = configuration;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _advantageClient = client;
            _advantageClient.BaseAddress = new Uri(_config["Advantage:Base"]);
        }
        // Send to Yandex
        public IEnumerable<InvoiceHeader> getInvoices()
        {
            DateTime thisDay = DateTime.UtcNow.AddDays(1);

            using (Stream s = _advantageClient.GetStreamAsync(_config["Advantage:Invoices"] + $"?ShipmentDate={thisDay.ToString("yyyy-MM-dd")}").Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var InvoiceList = serializer.Deserialize<IEnumerable<InvoiceHeader>>(reader);
                return InvoiceList.ToList();
            };
        }

        // Get by date from Yandex result
        public IEnumerable<InvoiceHeader> getInvoices(string thisDay)
        {
          DateTime dResult = DateTime.ParseExact(thisDay, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).AddDays(1);

            using (Stream s = _advantageClient.GetStreamAsync(_config["Advantage:Invoices"] + $"?ShipmentDate={dResult.ToString("yyyy-MM-dd")}").Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var InvoiceList = serializer.Deserialize<IEnumerable<InvoiceHeader>>(reader);
                return InvoiceList.ToList();
            };
        }
        public IEnumerable<Driver> GetDrivers()
        {
            using (Stream s = _advantageClient.GetStreamAsync(_config["Advantage:Drivers"]).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var result = serializer.Deserialize<IEnumerable<Driver>>(reader);
                return result;
            };
        }
        public IEnumerable<Client> GetClients()
        {
            using (Stream s = _advantageClient.GetStreamAsync("api/Clients/all").Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var result = serializer.Deserialize<IEnumerable<Client>>(reader);
                return result;
            };
        }
    }
}
