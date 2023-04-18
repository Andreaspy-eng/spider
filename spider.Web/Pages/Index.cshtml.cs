using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using spider.AdvantageModels;
using System.Collections;
using System.Collections.Generic;

namespace spider.Web.Pages;
public class IndexModel : spiderPageModel
{
    private readonly IAdvantageService _advantage;
    private readonly IYandexRoutingService _yandex;
    public IEnumerable<InvoiceHeader> invoices;
    public IndexModel(IAdvantageService advantage, IYandexRoutingService yandex)
    {
        _advantage = advantage;
        _yandex = yandex;
    }


    public void OnGet()
    {
        invoices = new List<InvoiceHeader>();
    }

    public IActionResult OnPostInvoices()
    {
        invoices = _advantage.getInvoices();
        return Page();
    }

    public IActionResult OnPostRoutes()
    {
        var routes = _yandex.GetResultAsync();
        return Page();
    }
}
