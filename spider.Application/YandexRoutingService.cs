using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using spider;
using spider.AdvantageModels;
using spider.YandexApi;
using spider.YandexApi.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YandexRouting
{

    public class YandexRoutingService: IYandexRoutingService
    {
        private IConfiguration _config;
        private DateTime _estimatedComplitedTime;
        private string _requestId;
        private HttpClient _client;

        /*private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        };*/

        public YandexRoutingService(IConfiguration config, HttpClient client)
        {
            _config = config;
            client.BaseAddress = new Uri(_config["Yandex:Main"]!);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;

        }

        public void ClearAllSavedData()
        {
            _estimatedComplitedTime = new DateTime();
            _requestId = null;
        }

        public QueryCreateRouteList createQueryToApi(IEnumerable<Counterparty> Clients, IEnumerable<Car> cars) 
        {
            var query = new QueryCreateRouteList();
            query.depot = new Depot()
            {
                id = "1",
                point = new Point()
                {
                    lat = 55.733996,
                    lon = 37.588472
                },
                time_window = "07:00-18:00"
            };

            query.vehicles = new List<Vehicles>()
            {
                new Vehicles(){ id = "1"},
                 new Vehicles(){ id = "2"},
                  new Vehicles(){ id = "3"}
            };

            query.options = new() { };

            var locations = new List<Locations>();
            foreach(var client in Clients) 
            {
                var lacalka=new Locations();
                lacalka.id = client.codeFromBase;
                lacalka.point = new Point()
                {
                    lat = client.latitude,
                    lon = client.longitude
                };
                lacalka.time_window = "09:00-18:00";
                locations.Add( lacalka );
            }
            query.locations = locations;
            return query;
        }
        public async Task<YandexRoutingResult> GetResultAsync(QueryCreateRouteList request)
        {
            ClearAllSavedData();
            var CreatedTask=CreateTask(request);

            await Task.Run(() => { Thread.Sleep(1500); });

            var respond = GetResult(_requestId);

            while (respond is null)
            {
                await Task.Delay(5000);
                respond = GetResult(_requestId);
            }
            return respond;

        }

        public YandexRoutingTaskCreatedResponse CreateTask(QueryCreateRouteList query)
        {
            var content = JsonConvert.SerializeObject(query); 
            var requestUri = QueryHelpers.AddQueryString(_config["Yandex:AddTask"],"apikey", _config["Yandex:Key"]);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Content = new StringContent(
                content, 
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = _client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<YandexRoutingTaskCreatedResponse>().Result;
            }
            else
            {
                return default;
            }
        }

/*        private async YandexRoutingResult GetResponseAsync()
        {
            int delay = 0;

            if (_estimatedComplitedTime !=  new DateTime())
            {
                delay = (int)(_estimatedComplitedTime - DateTime.UtcNow).TotalMilliseconds;
                if(delay < 0 ) delay = 0;
            }

            await Task.Run(() => { Thread.Sleep(delay);});

            var respond = GetResult(_requestId);

            while (respond is null)
            {
                await Task.Delay(5000);
                respond = GetResult(_requestId);
            }

            return respond;

        }*/

        public YandexRoutingResult GetResult(string taskGuid)
        {
            using (Stream s = _client.GetStreamAsync(_config["Yandex:GetTask"] + taskGuid).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var InvoiceList = serializer.Deserialize<YandexRoutingResult>(reader);
                return InvoiceList;
            };
        }
    }
}
