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
    public class LocarusService : ILocarusService
    {
        private readonly HttpClient _locarusClient;
        private readonly IConfiguration _config;

        public LocarusService(
            HttpClient client,
            IConfiguration configuration
            )
        {
            _config = configuration;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _locarusClient = client;
            _locarusClient.BaseAddress = new Uri(_config["Locarus:Base"]);
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
                    if (regex.IsMatch(Car.number) && Car.number!= "Н322УВ37" && Car.number!= "О016АТ37") CarResult.Add(Car);
                }                
                return CarResult;
            };
        }
    }
}
