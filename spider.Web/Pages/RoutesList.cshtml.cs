using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
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
            if (routes is null) routes = new();
        }

        public void OnGetDisplay()
        {
            routes = _yandex.GetLastResult();     
        }

        public IActionResult OnPostRoutes(int id)
        {
            return RedirectToPage("/Route", "Display", new { route = id });
        }
    }
}
