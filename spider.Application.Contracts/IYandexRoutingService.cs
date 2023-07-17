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
        public QueryCreateRouteList createQueryToApi(IEnumerable<Counterparty> Clients, IEnumerable<Car> cars, IEnumerable<InvoiceHeader> unfilteredOrders);
        public YandexRoutingResult GetLastResult();
        public IEnumerable<ResultTokenDTO> GetAll();
        public YandexRoutingTaskCreatedResponse CreateTask(QueryCreateRouteList query);
        public YandexRoutingResult GetResult(string taskGuid);
        /// <summary>
        /// https://yandex.ru/routing/doc/vrp/ref/ChildTasks/getChildTasks.html
        /// </summary>
        /// <returns></returns>
        public List<ChildTask> GetChildTasks(string taskGuid);
    }
}
