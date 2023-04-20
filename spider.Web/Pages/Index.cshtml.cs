using Azure.Core;
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
    private readonly ILocarusService _locarus;
    public IEnumerable<InvoiceHeader> invoices;
    private IEnumerable<Counterparty> _counterparties;

    public IndexModel(IEnumerable<InvoiceHeader> invoices)
    {
        this.invoices = invoices;
    }

    public IndexModel(
        IAdvantageService advantage,
        ICounterpartyService counterparty,
        ILocarusService locarus,
        IYandexRoutingService yandex)
    {
        _advantage = advantage;
        _yandex = yandex;
        _locarus = locarus;
        _counterparties= counterparty.getCounterparties();
    }


    public void OnGet()
    {
        invoices = new List<InvoiceHeader>();
    }

    public IActionResult OnPostInvoices()
    {
        invoices = _advantage.getInvoices();
        return new IndexModel(invoices).Page();
    }

    public IActionResult OnPostRoutes()
    {
        List<Counterparty> FilteredCounterpaties= new List<Counterparty>();
        //invoices = _advantage.getInvoices();
        if (invoices is not null)
        {
            foreach(var counteraprty in _counterparties) 
            {
                if (invoices.Any(x => x.CounterpartyId == counteraprty.codeFromBase))
                {
                    FilteredCounterpaties.Add(counteraprty);
                }
            }
        }
        var cars = _locarus.GetCars().Take(2);
        var query = _yandex.createQueryToApi(FilteredCounterpaties, cars);
        var CreatedTask = _yandex.CreateTask(query);
        var routes = _yandex.GetLastResult();
        return Page();
    }
}
