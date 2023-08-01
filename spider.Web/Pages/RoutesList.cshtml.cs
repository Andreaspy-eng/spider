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
using System.Net;
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
            try
            {
              drivers = _advantage.GetDrivers();
              routes = _yandex.GetLastResult();
              if (routes is null) routes = new();
            }
            catch(Exception e)
            {
              ViewData["Error"] = e.Message;
            }
        }

        public void OnGetDisplay(string id)
        {
            try
            {
              routes = _yandex.GetResult(id);     
              drivers = _advantage.GetDrivers();
              if (routes is null) routes = new();
            }
            catch(Exception e)
            {
              ViewData["Error"] = e.Message;
            }
        }

        public IActionResult OnPostRoutes(int id)
        {
            return RedirectToPage("/Route", "Display", new { route = id });
        }


        public async Task<IActionResult> OnGetChangeName(string yandex_id,string id,string name)
        {
            var pizda = new PagedAndSortedResultRequestDto() { MaxResultCount = 1000 };
            var assigned = await _assignedRoutesService
                .GetListAsync(pizda);
            var to_add0 = assigned.Items.AsEnumerable().Where(x => x.yandex_id == yandex_id);
            var to_add = to_add0.FirstOrDefault(x => x.driver_name == name); //?
            if(to_add is null)
            {          
            await  _assignedRoutesService.CreateAsync(
                  new CrUpAssignedRoutes()
                  {
                      driver_name = name,
                      vehicle_id = id,
                      yandex_id = yandex_id
                  });
            }
            return RedirectToPage("/RoutesList", "Display",new { id = yandex_id});
        }

        public async Task<IActionResult> OnGetDeleteName(string yandex_id, string id)
        {
            var pizda = new PagedAndSortedResultRequestDto() { MaxResultCount = 1000 };
            var assigned = _assignedRoutesService
                .GetListAsync(pizda).Result.Items.Where(x => x.yandex_id == yandex_id);
            if(assigned is not null)
            {
                var to_del = assigned.First(x => x.vehicle_id == id);
                if (to_del != null)
                {
                    await _assignedRoutesService.DeleteAsync(to_del.Id);
                }
            }
            return RedirectToPage("/RoutesList", "Display",new { id = yandex_id});
        }

        public void OnPostTextFile(string id)
        {
            routes = _yandex.GetResult(id);     
            List<string> numbers = new ();  
            //var inv= _advantage.getInvoices(routes.result.options.date).GroupBy(p => p.CounterpartyId).ToList();
            foreach (var item in routes.result.routes)
            { 
              List<string> points = new();
              string DriverCode = item.vehicle_driver.Substring(0,2);
              string path=@$"\\{_config["App:FTP"]}\{_config["App:DriverFolder"]}\{DriverCode}"; 
              var tps=item.route.Where(x=>x.node.@type!="depot").GroupBy(p => p.node.value.@ref);
              int i = 1;
              foreach(var tp in tps)
              {
                foreach(var one in tp)
                {
                  if(one is not null)points.Add(one.node.value.id.Replace(" ",string.Empty)+";"+i); 
                }
                i++;
              }
              var userName = "test";
              var password = "12345678";
              var domain = "ALTOPT";
              var networkCredential = new NetworkCredential(userName, password, domain);
              if(points is not null && points.Count>0)BushFileService.createBushFile(points,path,networkCredential);
            }               
        }
    }
}
