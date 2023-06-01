using Microsoft.AspNetCore.Mvc.RazorPages;
using spider.YandexApi;
using System.Collections.Generic;

namespace spider.Web.Pages;
public class ChildTasksModel : PageModel
{
    private readonly IYandexRoutingService _yandex;
    public List<ChildTask> Results{ get; set; }
    public string taskGuid {get;set;}
    public ChildTasksModel(IYandexRoutingService yandex)
    {
          _yandex = yandex;
    }

    public void OnGetDisplay(string id)
    {
        taskGuid=id;
        Results = _yandex.GetChildTasks(id);
    }
}
