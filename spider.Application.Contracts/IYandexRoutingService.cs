using spider.AdvantageModels;
using spider.Yandex;
using spider.YandexApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace spider
{
    public interface IYandexRoutingService
    {
        public QueryCreateRouteList createQueryToApi(IEnumerable<Counterparty> Clients, IEnumerable<Car> cars);
        public YandexRoutingResult GetLastResult();
        public IEnumerable<ResultTokenDTO> GetAll();
        public YandexRoutingTaskCreatedResponse CreateTask(QueryCreateRouteList query);
        public YandexRoutingResult GetResult(string taskGuid);

    }
}
