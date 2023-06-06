using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Polly;
using spider.Application;
using spider.YandexApi;
using spider.YandexApi.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace spider.Web.Pages
{
    
    public class RoutesListModel : PageModel
    {
        private readonly IYandexRoutingService _yandex;
        private readonly IAdvantageService _advantage;
        public YandexRoutingResult routes { get; set; }
        public RoutesListModel(
            IYandexRoutingService yandex,
            IAdvantageService advantage)
        {
            _advantage= advantage;
            _yandex = yandex;
        }

        public void OnGet()
        {
            routes = _yandex.GetLastResult();
            if (routes is null) routes = new();
        }

        public void OnGetDisplay(string id)
        {
            routes = _yandex.GetResult(id);     
        }

        public IActionResult OnPostRoutes(int id)
        {
            return RedirectToPage("/Route", "Display", new { route = id });
        }

        public void OnPostTextFile(string id)
        {
            routes = _yandex.GetResult(id);     
            List<string> numbers = new ();
            
            var inv= _advantage.getInvoices(routes.result.options.date).GroupBy(p => p.CounterpartyId);
            foreach (var item in routes.result.routes)
            { 
              List<string> points = new();
              string DriverCode = routes.result.vehicles
                .Where(x=>x.id==item.vehicle_id)
                .FirstOrDefault().@ref
                .Substring(0,2);
              var tps=item.route.Where(x=>x.node.@type!="depot");
              int i = 1;
              foreach(var tp in tps)
              {
                var clientInv = inv.FirstOrDefault(x=>x.Key==tp.node.value.id).ToList();
                foreach(var one in clientInv)
                {
                  if(one is not null)points.Add(one.UniqueId.Replace(" ",string.Empty)+";"+i); 
                }
                i++;
              }
              if(points is not null && points.Count>0)BushFileService.createBushFile(points,DriverCode);
            }               
        }
    }
}
