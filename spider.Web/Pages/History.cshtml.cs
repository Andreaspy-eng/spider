using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using spider.Yandex;
using System.Collections.Generic;
using System.Linq;

namespace spider.Web.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly IYandexRoutingService _yandex;
        public List<ResultTokenDTO> Results { get; set; }
        public HistoryModel(IYandexRoutingService yandex)
        {
            _yandex = yandex;
        }

        public void OnGet()
        {
            Results = _yandex.GetAll().ToList();
        }
    }
}
