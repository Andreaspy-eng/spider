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
            List<string> points = new();
            int i = 1;
            foreach (var item in _advantage.getInvoices().GroupBy(p => p.CounterpartyId)) 
            {
                foreach (var person in item)
                {
                    numbers.Add(person.UniqueId.Replace(" ",string.Empty)+";"+i);
                }
                i++;
            }
            BushFileService.createBushFile(numbers,"�������������");
        }
    }
}
