using spider.AdvantageModels;
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
        public YandexRoutingTaskCreatedResponse CreateTask(QueryCreateRouteList query);
        public YandexRoutingResult GetResult(string taskGuid);

    }
}
