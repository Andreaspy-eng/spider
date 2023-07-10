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
        public int counter { get; set;}
        public List<Route> route { get; set; }

        public void OnGet()
        {
            var res = _yandex.GetLastResult();
            route = res.result.routes[counter].route;
        }

        public void OnGetDisplay()
        {
            var res = _yandex.GetLastResult();
            route = res.result.routes[counter].route;
            //var v = new YandexRouteListPrintForm(res,  clients);
            //v.VirtualPrint();
        }

    }
}
