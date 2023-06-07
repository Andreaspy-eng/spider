using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
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
        public IConfiguration _config;
        public RoutesListModel(
            IConfiguration config,
            IYandexRoutingService yandex,
            IAdvantageService advantage,
            IAssignedRoutesService assignedRoutes)
        {
            _config=config;
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

        public void OnGetDisplay(string id)
        {
            routes = _yandex.GetResult(id);     
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
            return RedirectToPage("/RoutesList", "Display",new { id = yandex_id});
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
            return RedirectToPage("/RoutesList", "Display",new { id = yandex_id});
        }

        public void OnPostTextFile(string id)
        {
            routes = _yandex.GetResult(id);     
            List<string> numbers = new ();
            
            var inv= _advantage.getInvoices(routes.result.options.date).GroupBy(p => p.CounterpartyId);
            foreach (var item in routes.result.routes)
            { 
              List<string> points = new();
              string DriverCode = item.vehicle_driver.Substring(0,2);
              string path=@$"\\{_config["App:FTP"]}\{_config["App:DriverFolder"]}\{DriverCode}"; 
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
              if(points is not null && points.Count>0)BushFileService.createBushFile(points,path);
            }               
        }
    }
}
