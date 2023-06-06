using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Polly;
using spider.AdvantageModels;
using spider.Application;
using spider.Yandex;
using spider.YandexApi;
using spider.YandexApi.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace spider.Web.Pages
{
    
    public class RoutesListModel : PageModel
    {
        private readonly IYandexRoutingService _yandex;
        private readonly IAdvantageService _advantage;
        private readonly IAssignedRoutesService _assignedRoutesService;
        public IEnumerable<Driver> drivers;
        public YandexRoutingResult routes { get; set; }
        public RoutesListModel(
            IYandexRoutingService yandex,
            IAdvantageService advantage,
            IAssignedRoutesService assignedRoutes)
        {
            _assignedRoutesService=assignedRoutes;
            _advantage= advantage;
            _yandex = yandex;
        }

        public void OnGet()
        {
            drivers = _advantage.GetDrivers();
            routes = _yandex.GetLastResult();
            if (routes is null) routes = new();
        }

        public void OnGetDisplay()
        {
            routes = _yandex.GetLastResult();
            drivers = _advantage.GetDrivers();
            if (routes is null) routes = new();
        }

        public IActionResult OnPostRoutes(int id)
        {
            return RedirectToPage("/Route", "Display", new { route = id });
        }

        public IActionResult OnGetChangeName(string yandex_id,string id,string name)
        {
            _assignedRoutesService.CreateAsync(
                new CrUpAssignedRoutes()
                {
                    driver_name = name,
                    vehicle_id = id,
                    yandex_id = yandex_id
                });
            return RedirectToPage("/RoutesList", "Display");
        }

        public IActionResult OnGetDeleteName(string yandex_id, string id)
        {
            var pizda = new PagedAndSortedResultRequestDto() { MaxResultCount = 1000 };
            var assigned = _assignedRoutesService
                .GetListAsync(pizda).Result.Items.Where(x => x.yandex_id == yandex_id);
            if(assigned is not null)
            {
                var to_del = assigned.First(x => x.vehicle_id == id);
                if (to_del != null)
                {
                    _assignedRoutesService.DeleteAsync(to_del.Id);
                }
            }
            return RedirectToPage("/RoutesList", "Display");
        }

        public void OnPostTextFile()
        {
            routes = _yandex.GetLastResult();
            List<string> numbers = new ();
            List<string> points = new();
           
            var invoices = _advantage.getInvoices();
            foreach (var route in routes.result.routes) 
            {
                var inv = invoices.IntersectBy(
                    route.route.Select(x => x.node.value.id),
                    second => second.CounterpartyId);
                int i = 1;
                foreach (var item in inv.GroupBy(p => p.CounterpartyId))
                {
                    foreach (var person in item)
                    {
                        numbers.Add(person.UniqueId.Replace(" ", string.Empty) + ";" + i);
                    }                    
                    i++;
                }
                BushFileService.createBushFile(numbers, route.vehicle_driver.Substring(0,2));

            }
            
           
        }
    }
}
