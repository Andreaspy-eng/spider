using spider.AdvantageModels;
using spider.YandexApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace spider
{
    public interface IYandexRoutingService
    {
        public void ClearAllSavedData();
        public QueryCreateRouteList createQueryToApi(IEnumerable<Counterparty> Clients, IEnumerable<Car> cars);
        public  Task<YandexRoutingResult> GetResultAsync(QueryCreateRouteList request);
        public YandexRoutingTaskCreatedResponse CreateTask(QueryCreateRouteList query);
        public YandexRoutingResult GetResult(string taskGuid);

    }
}
