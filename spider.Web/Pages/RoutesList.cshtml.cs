using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Polly;
using spider.YandexApi;
using spider.YandexApi.Result;
using System.Threading.Tasks;

namespace spider.Web.Pages
{
    
    public class RoutesListModel : PageModel
    {
        private readonly IYandexRoutingService _yandex;
        public YandexRoutingResult routes { get; set; }
        public RoutesListModel(
            IYandexRoutingService yandex)
        {
            _yandex = yandex;
        }

        public void OnGet()
        {
            routes = _yandex.GetLastResult();
        }

        /*public void OnGetRoute(int id)
        {
            Route(id);
        }*/

        public IActionResult OnPostRoutes(int id)
        {
            routes = _yandex.GetLastResult();
            var b = routes.result.routes[id];
            return RedirectToPage("/Route", "Display", new { route = id });
        }
        //public void OnPostRoutes(Routes waybill) { 
            
        //}
    }
}
