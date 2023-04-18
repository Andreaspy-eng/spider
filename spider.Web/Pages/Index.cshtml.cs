using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using spider.AdvantageModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace spider.Web.Pages;
public class IndexModel : spiderPageModel
{
    private readonly IAdvantageService _advantage;
    private readonly IYandexRoutingService _yandex;
    public IEnumerable<InvoiceHeader> invoices;
    private IEnumerable<Counterparty> _counterparties;
    public IndexModel(IAdvantageService advantage, IYandexRoutingService yandex)
    {
        _advantage = advantage;
        _yandex = yandex;
        _counterparties=_advantage.getCounterparties();
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
        List<Counterparty> FilteredCounterpaties= new List<Counterparty>();
        if(invoices is not null)
        {
            foreach(var counteraprty in _counterparties) 
            {
                if (invoices.Any(x => x.CounterpartyId == counteraprty.codeFromBase))
                {
                    FilteredCounterpaties.Add(counteraprty);
                }
            }
        }
        var cars = _advantage.GetCars();
        var query = _yandex.createQueryToApi(FilteredCounterpaties, cars);
        var routes = _yandex.GetResultAsync(query);
        return Page();
    }
}
