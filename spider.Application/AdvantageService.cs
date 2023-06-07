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
        public IEnumerable<InvoiceHeader> getInvoices()
        {
            string thisDay = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string beforeDay = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");

            using (Stream s = _advantageClient.GetStreamAsync(_config["Advantage:Invoices"] + $"?Start={beforeDay}&End={thisDay}&Codes=Т-р&Codes=Т-Р").Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var InvoiceList = serializer.Deserialize<IEnumerable<InvoiceHeader>>(reader);
                return InvoiceList;
            };
        }
        public IEnumerable<InvoiceHeader> getInvoices(string thisDay)
        {
          DateTime dResult = DateTime.ParseExact(thisDay, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
          string beforeDay = dResult.AddDays(-1).ToString("yyyy-MM-dd");

            using (Stream s = _advantageClient.GetStreamAsync(_config["Advantage:Invoices"] + $"?Start={beforeDay}&End={thisDay}&Codes=Т-р&Codes=Т-Р").Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var InvoiceList = serializer.Deserialize<IEnumerable<InvoiceHeader>>(reader);
                return InvoiceList;
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
    }
}
