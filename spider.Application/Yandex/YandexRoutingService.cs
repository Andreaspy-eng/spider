using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using spider;
using spider.AdvantageModels;
using spider.Yandex;
using spider.YandexApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Volo.Abp.Application.Dtos;
using Serilog;
namespace YandexRouting
{

    public class YandexRoutingService: IYandexRoutingService
    {
        private IConfiguration _config;
        private IResultTokenService _resultTokenService;
        private IAssignedRoutesService _assignedRoutesService;
        private HttpClient _client;

        /*private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        };*/

        public YandexRoutingService(
            IConfiguration config,
            HttpClient client,
            IResultTokenService resultTokenService)
            IResultTokenService resultTokenService,
            IAssignedRoutesService assignedRoutesService)
        {
            _config = config;
            _resultTokenService = resultTokenService;
            client.BaseAddress = new Uri(_config["Yandex:Main"]!);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
            _assignedRoutesService = assignedRoutesService;
        }


        public QueryCreateRouteList createQueryToApi(IEnumerable<Counterparty> Clients, IEnumerable<Car> cars) 
        {
            var query = new QueryCreateRouteList();
            query.depot = new Depot()
            {
                id = "1",
                name= "СКЛАД",
                point = new Point()
                {
                    lat = 56.989620,
                    lon = 41.047034
                },
                time_window = "07:00-18:00"
            };

            var vehicles = new List<Vehicles>();
            foreach (var car in cars)
            {
                var lacalka = new Vehicles();
                lacalka.id = car.number;
                lacalka.return_to_depot = true;
                lacalka.shifts =new Shifts[]
                {   new Shifts()
                    {
                        id = "0",
                        time_window= "06:00:00-17:00:00"
                    }                                               //FIXME Спросить время работы водителей
                };
                lacalka.name = car.model;
                lacalka.capacity = new Shipment()
                {
                    weight_kg = (int)car.maxWeight,
                    units=10,
                    volume=new () 
                    {
                        width_m=1.9,
                        depth_m= 2.7,
                        height_m= 1.5
                    }
                };

                vehicles.Add(lacalka);
            }
            query.vehicles = vehicles;

            query.options = new spider.YandexApi.Options()
            {
                quality = "low",
            };

            var locations = new List<Locations>();
            foreach(var client in Clients) 
            {
                var lacalka=new Locations();
                lacalka.id = client.codeFromBase;
                if (client.latitude == 0.0) client.latitude = 55.733996;
                if (client.longitude == 0.0) client.longitude= 37.588472;
                lacalka.point = new Point()
                {
                    lat = client.latitude,
                    lon = client.longitude,
                };
                lacalka.time_window = "09:00-18:00";
                locations.Add( lacalka );
            }
            query.locations = locations;
            return query;
        }

        public YandexRoutingResult GetLastResult()
        {
            try
            {
                var Hui = new PagedAndSortedResultRequestDto();
                var task = _resultTokenService.GetListAsync(Hui).Result
                    .Items.OrderBy(x=>x.CreationDate)
                    .Last().yandex_id;
                var respond = GetResult(task);
                return respond;
            }
            catch(Exception e)
            {
                Log.Error($"Не удалось получить последний результат \n{e.Message}");
                return null;
            }
        }

        public IEnumerable<ResultTokenDTO> GetAll()
        {
            var Hui = new PagedAndSortedResultRequestDto();
            return _resultTokenService.GetListAsync(Hui).Result.Items;
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

            if(response.StatusCode==System.Net.HttpStatusCode.PaymentRequired)
            {
                throw new Exception("Токен просрочен! Нужна оплатама");
            }
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadFromJsonAsync<YandexRoutingTaskCreatedResponse>().Result;
                var a=_resultTokenService.CreateAsync(new CrUpResultToken() { yandex_id = res.id, message = res.message }).Result;
                return res;
            }
            else
            {
                return default;
            }
        }
        
        public YandexRoutingResult GetResult(string taskGuid)
        {
            CheckTaskGuid(taskGuid);
            GetLastModificationOfTaskGuid(taskGuid);
            var path = _config["Yandex:GetResult"] + taskGuid;
            return GetData<YandexRoutingResult>(path);
        }

        public List<ChildTask> GetChildTasks(string taskGuid)
        {
            CheckTaskGuid(taskGuid);
            var path = @$"{_config["Yandex:GetChildTasks"]}?apikey={_config["Yandex:Key"]}&parent_task_id={taskGuid}";
            return GetData<List<ChildTask>>(path);
        }

        private T GetData <T>(string path)
        {
            using (Stream s = _client.GetStreamAsync(path).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new();
                var result = serializer.Deserialize<T>(reader);
                return result;
            };
        }

        private string GetLastModificationOfTaskGuid(string taskGuid)
        {
            CheckTaskGuid(taskGuid);
            var list=GetChildTasks(taskGuid);
            if(list is null || list.Count<1)return taskGuid;
            else return list.Last().task_id;
        }
        
        private void CheckTaskGuid(string taskGuid)
        {
            if(taskGuid is null) throw new Exception("ИД задачи содержит NULL");
            if(taskGuid.Trim()=="") throw new Exception("ИД задачи пустой");
        }
    }
}
