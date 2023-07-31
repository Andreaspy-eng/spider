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


        public QueryCreateRouteList createQueryToApi(IEnumerable<Counterparty> Clients, IEnumerable<Car> cars, IEnumerable<InvoiceHeader> unfilteredOrders, IEnumerable<InvoiceHeader> all) 
        {
            var query = new QueryCreateRouteList();
            query.depot = new Depot()
            {
                id = "1",
                name= "СКЛАД",
                point = new Point()
                {
                    lat = 56.9884970,
                    lon = 41.0466750
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
                    weight_kg = (int)car.maxWeight<1?5000:(int)car.maxWeight,
                    units=(int)car.maxPackageCount<1?450:(int)car.maxPackageCount,
                };

                vehicles.Add(lacalka);
            }
            query.vehicles = vehicles;

            query.options = new spider.YandexApi.Options()
            {
                quality = "normal",
                routing_mode="truck",
                date=DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")                              //FIXME Вынести в интерфейс
            };

            var locations = new List<Locations>();
            foreach(var client in Clients) 
            {
                foreach(var number in client.InvoiceNumber)
                {
                    var lacalka=new Locations();
                    lacalka.id = number;
                    lacalka.title=client.name is null?"НЕТ ИМЕНИ":client.name.Replace("\""," ");
                    lacalka.clientId=client.codeFromBase;
                    lacalka.description=client.address is null?"НЕТ АДРЕСА":client.address.Replace("\""," ");
                    if (client.latitude == 0.0) client.latitude = 55.733996;
                    if (client.longitude == 0.0) client.longitude= 37.588472;
                    lacalka.point = new Point()
                    {
                        lat = client.latitude,
                        lon = client.longitude,
                    };
                    if (client.workSchedule is not null && client.workSchedule.Count>0)
                    {
                        if(lacalka.time_windows is null)lacalka.time_windows=new();
                        lacalka.time_windows.Add( 
                            new spider.YandexApi.Result.TimeWindow()
                            {
                                time_window=$"{client.workSchedule.FirstOrDefault().openTime}-{client.workSchedule.FirstOrDefault().closeTime}",
                                hard_time_window=$"{client.workSchedule.FirstOrDefault().openTime}-{client.workSchedule.FirstOrDefault().closeTime}"
                            }
                        );
                    }
                    else 
                    {
                        if(lacalka.time_windows is null)lacalka.time_windows=new();
                        lacalka.time_windows.Add( 
                            new spider.YandexApi.Result.TimeWindow()
                            {
                                time_window="08:00-17:00",
                                hard_time_window="08:00-17:00",
                            }
                        );
                    }
                    var inv=all.FirstOrDefault(x => x.UniqueId == number);
                    if(inv is not null)
                    {
                        lacalka.shipment_size=new()
                        {
                            weight_kg=Math.Abs((int)inv.WeightAll),
                            units=Math.Abs(inv.CountAll)
                        };
                    }
                    lacalka.service_duration_s = Convert.ToInt32(_config["App:TimeForOrder"]);
                    lacalka.shared_service_duration_s =  Convert.ToInt32(_config["App:TimeForAdress"]);
                    lacalka.penalty=new()
                    {
                    early = new()
                    {
                        @fixed = 0
                    },

                    late = new()
                    {
                        @fixed = _config["Penalty:fixed"] is null ? 2000: Convert.ToInt32(_config["Penalty:fixed"]),
                        minute = _config["Penalty:minute"] is null ? 100:Convert.ToInt32(_config["Penalty:minute"])
                    },

                    out_of_time = new()
                    {
                        @fixed = 0
                    }
                    };
                    locations.Add( lacalka );
                }
            }
            foreach(var inv in unfilteredOrders)
            {
                var lacalka=new Locations();
                lacalka.id = inv.UniqueId;
                lacalka.title=inv.CounterpartyName is null?"НЕТ ИМЕНИ":inv.CounterpartyName.Trim().Replace("\""," ");
                lacalka.clientId=inv.CounterpartyId;
                lacalka.description=inv.CounterpartyAddress is null?"НЕТ АДРЕСА":inv.CounterpartyAddress.Trim().Replace("\""," ");
                lacalka.point = new Point()
                {
                    lat =  56.9884970,
                    lon =  41.0466750,
                };
                if(inv is not null)
                {
                    lacalka.shipment_size=new()
                    {
                        weight_kg=Math.Abs((int)inv.WeightAll),
                        units=Math.Abs(inv.CountAll)
                    };
                }
                if(lacalka.time_windows is null)lacalka.time_windows=new();
                lacalka.time_windows.Add( 
                    new spider.YandexApi.Result.TimeWindow()
                    {
                        time_window="08:00-17:00",
                        hard_time_window="08:00-17:00",
                    }
                );
                lacalka.service_duration_s = Convert.ToInt32(_config["App:TimeForOrder"]);
                lacalka.shared_service_duration_s = Convert.ToInt32(_config["App:TimeForAdress"]);
                lacalka.penalty=new()
                {
                  early = new()
                  {
                    @fixed = 0
                  },

                    late = new()
                    {
                        @fixed = _config["Penalty:fixed"] is null ? 2000: Convert.ToInt32(_config["Penalty:fixed"]),
                        minute = _config["Penalty:minute"] is null ? 100:Convert.ToInt32(_config["Penalty:minute"])
                    },

                  out_of_time = new()
                  {
                    @fixed = 0
                  }
                };
                locations.Add( lacalka );
            }
            query.locations = locations;
            return query;
        }

        public YandexRoutingResult GetLastResult()
        {
            try
            {
                var Route = new PagedAndSortedResultRequestDto();
                var task = _resultTokenService.GetListAsync(Route).Result
                    .Items.OrderBy(x=>x.CreationDate)
                    .Last().yandex_id;
                var respond = GetResult(task);
                if(respond is not null )
                {
                    if(respond.result is null)
                    {
                      throw new Exception(respond.message);
                    }
                        var pizda = new PagedAndSortedResultRequestDto() { MaxResultCount=1000};
                        var assigned = _assignedRoutesService
                            .GetListAsync(pizda).Result.Items.Where(x => x.yandex_id == respond.id);
                        if(assigned != null )
                        {
                            foreach( var route in assigned )
                            {
                                respond.result.routes.Find(x => x.vehicle_id == route.vehicle_id)
                                    .vehicle_driver = route.driver_name;
                            }
                        }
                }
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
            var Route = new PagedAndSortedResultRequestDto();
            return _resultTokenService.GetListAsync(Route).Result.Items;
        }

        public YandexRoutingTaskCreatedResponse CreateTask(QueryCreateRouteList query)
        {
            var content = JsonConvert.SerializeObject(query).Replace("\\",""); 
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
                var res = response.Content.ReadAsStringAsync().Result;
                throw new Exception(response.StatusCode+"\n"+response.ReasonPhrase+"\n"+res);
            }
        }
        
        public YandexRoutingResult GetResult(string taskGuid)
        {
            taskGuid=GetLastModificationOfTaskGuid(taskGuid);
            var path = _config["Yandex:GetResult"] + taskGuid;
            var respond= GetData<YandexRoutingResult>(path);
            if(respond != null )
            {
                if(respond.result is null)
                {
                  throw new Exception(respond.message);
                }
                    var pizda = new PagedAndSortedResultRequestDto() { MaxResultCount=1000};
                    var assigned = _assignedRoutesService
                        .GetListAsync(pizda).Result.Items.Where(x => x.yandex_id == respond.id);
                    if(assigned != null )
                    {
                        foreach( var route in assigned )
                        {
                            respond.result.routes.Find(x => x.vehicle_id == route.vehicle_id)
                                .vehicle_driver = route.driver_name;
                        }
                    }
            }
            return respond;
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
