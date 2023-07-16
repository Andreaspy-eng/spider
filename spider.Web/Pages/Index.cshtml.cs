using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using spider.AdvantageModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace spider.Web.Pages;
public class IndexModel : spiderPageModel
{
    private readonly IAdvantageService _advantage;
    private readonly IYandexRoutingService _yandex;
    private readonly ILocarusService _locarus;
    public IEnumerable <Car> _cars;
    public IEnumerable <InvoiceHeader> Invoices;
    public IEnumerable <InvoiceHeader> lastInvoices;
    private IEnumerable<Counterparty> _counterparties;

    public IndexModel(IEnumerable<Car> cars)
    {
        this._cars = cars;
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
        _cars = _locarus.GetCars();
        var invoices = _advantage.getInvoices();
        var date = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd");
        Invoices = invoices.Where(x => x.ShipmentDate.ToString("yyyy-MM-dd")==date).ToList();
        lastInvoices = invoices.Where(x => x.ShipmentDate.ToString("yyyy-MM-dd")!=date).ToList();
        _counterparties = counterparty.getCounterparties();
        
    }


    public void OnGet()
    {
        _cars = new List<Car>();
    }

    public IActionResult OnPostInvoices()
    {
        _cars = _locarus.GetCars();
        return new IndexModel(_cars).Page();
    }

    public IActionResult OnPostRoutes(string[] checkboxes)
    {
        List<Counterparty> FilteredCounterpaties = new() { };
        List <Car> FilteredCars = new() { };
        try
        {
            _cars = _locarus.GetCars();
            Invoices = _advantage.getInvoices();
            if (Invoices is not null)
            {
                foreach (var counteraprty in _counterparties)
                {
                    if (Invoices.Any(x => x.CounterpartyId == counteraprty.codeFromBase))
                    {
                        var inv=Invoices.FirstOrDefault(x => x.CounterpartyId == counteraprty.codeFromBase);
                        counteraprty.InvoiceNumber=$"{inv.CodeOperation}{inv.ComputerNumber}{inv.Date.Year}{inv.DocumentNumber.PadLeft(6)}";
                        FilteredCounterpaties.Add(counteraprty);
                    }
                }
                FilteredCars = _cars.Where(x => checkboxes.Contains(x.imei)).ToList();
                if (checkboxes.Length == 0) FilteredCars = _cars.DistinctBy(x => x.number).ToList();
            }
            var query = _yandex.createQueryToApi(FilteredCounterpaties, FilteredCars);
            var CreatedTask = _yandex.CreateTask(query);
            var routes = _yandex.GetLastResult();
            return Page();
        }
        catch(Exception e)
        {
            ViewData["Error"] = e.Message;
            return Page();
        }
       
    }
}
