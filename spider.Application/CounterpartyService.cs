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
    public class CounterpartyService : ICounterpartyService
    {
        private readonly HttpClient _counterpartyClient;
        private readonly IConfiguration _config;

        public CounterpartyService(
            HttpClient client,
            IConfiguration configuration
            )
        {
            _config = configuration;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _counterpartyClient = client;
            _counterpartyClient.BaseAddress = new Uri(_config["Counterparty:Base"]);
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
    }
}
