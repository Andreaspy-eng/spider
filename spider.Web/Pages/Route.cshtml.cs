using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using spider.AdvantageModels;
using spider.YandexApi.Result;
using System.Collections.Generic;

namespace spider.Web.Pages
{
    public class RouteModel : PageModel
    {
        private IYandexRoutingService _yandex;
        public IEnumerable<Counterparty> clients;
        public RouteModel(
            IYandexRoutingService yandex,
            ICounterpartyService counterparty)
        {
            _yandex = yandex;
            clients=counterparty.getCounterparties();
        }

        [BindProperty(Name="route", SupportsGet = true)]
        public int hui { get; set;}
        public Routes route { get; set; }

        public void OnGet()
        {
            route = _yandex.GetLastResult().result.routes[hui];
        }

        public void OnGetDisplay()
        {
            route = _yandex.GetLastResult().result.routes[hui];
        }

    }
}
