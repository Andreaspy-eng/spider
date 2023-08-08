using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using spider.AdvantageModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spider.Web.Pages;
public class IndexModel : spiderPageModel
{
    private readonly IAdvantageService _advantage;
    private readonly IYandexRoutingService _yandex;
    private readonly ILocarusService _locarus;
    private readonly ICounterpartyService _counterparty;
    public IEnumerable <Car> _cars;
    public IEnumerable <InvoiceHeader> Invoices;
    public IEnumerable <InvoiceHeader> lastInvoices;
    private IEnumerable<Counterparty> _counterparties;
    public IEnumerable <Driver> _drivers;

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
        _counterparty=counterparty;
         var date = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd");
        var carsGet = Task.Run(
          () =>
          _cars = _locarus.GetCars()
        );
        var invoicesGet = Task.Run(
          () =>
          {
            var invoices = _advantage.getInvoices();
            Invoices = invoices.Where(x => x.ShipmentDate.ToString("yyyy-MM-dd")==date).ToList();
            lastInvoices = invoices.Where(x => x.ShipmentDate.ToString("yyyy-MM-dd")!=date).ToList();
          }
        );
        var counterpartyGet = Task.Run(
          () =>
           _counterparties = _counterparty.getCounterparties()
        );
        var driversGet = Task.Run(
          () =>
           _drivers = _advantage.GetDrivers()
        );
        counterpartyGet.Wait();
        invoicesGet.Wait();
        carsGet.Wait();   
        driversGet.Wait();
    }

    public void OnGet()
    {
    }

    public IActionResult OnPostRoutes(string[] checkboxes,List<string> invoices, string[] checkboxes_priorety,int minStops,int maxStops)
    {
        List<Counterparty> FilteredCounterpaties = new(){}, WarningCounterparties = new(){};
        List<InvoiceHeader> FilteredInvoice = new() { };
        List <Car> FilteredCars = new() { };
        int countInvoice=0;
        try
        {
            invoices=invoices.Select(x=>x.Replace("_"," ")).ToList();
                   var carsGet = Task.Run(
              () =>
              _cars = _locarus.GetCars()
            );
            var invoicesGet = Task.Run(
              () =>
              {
                var invoices = _advantage.getInvoices();
                Invoices = invoices.Where(x => x.ShipmentDate.ToString("yyyy-MM-dd")==DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")).ToList();
                lastInvoices = invoices.Where(x => x.ShipmentDate.ToString("yyyy-MM-dd")!=DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")).ToList();
              }
            );
            var counterpartyGet = Task.Run(
              () =>
              _counterparties = _counterparty.getCounterparties()
            );
            counterpartyGet.Wait();
            invoicesGet.Wait();
            carsGet.Wait();  

            if (Invoices is not null)
            {
                foreach(var number in invoices)
                {
                    var inv=Invoices.FirstOrDefault(x => x.UniqueId == number);
                    if(inv is not null)FilteredInvoice.Add(inv);
                }
                countInvoice=FilteredInvoice.Count;
                int count=0;
                foreach (var counteraprty in _counterparties)
                {
                    var inv=FilteredInvoice.Where(x => x.CounterpartyId.Trim() == counteraprty.codeFromBase.Trim()).ToList();
                    if(inv is not null)
                    {
                        foreach(var i in inv)
                        {
                          if(counteraprty.InvoiceNumber is null)counteraprty.InvoiceNumber=new(){};
                          counteraprty.InvoiceNumber.Add($"{i.CodeOperation}{i.ComputerNumber}{i.Date.Year}{i.DocumentNumber.PadLeft(6)}");
                          FilteredCounterpaties.Add(counteraprty);
                          FilteredInvoice.Remove(i);
                        }
                    }
                }
                var c=_advantage.GetClients();
                foreach(var cl in c)
                {
                    var cig=_counterparties.FirstOrDefault(x => x.codeFromBase.Trim() == cl.Id.Trim());
                    if(cig is null)count++;
                }            
                FilteredCars = _cars.Where(x => checkboxes.Contains(x.imei)).ToList();
                if (checkboxes.Length == 0) FilteredCars = _cars.DistinctBy(x => x.number).ToList();
                foreach(var item in FilteredCars)
                {
                  item.minStops=minStops;
                  item.maxStops=maxStops;
                  if(checkboxes_priorety.Contains(item.imei))item.maxStops+=10;
                }
            }
            var query = _yandex.createQueryToApi(FilteredCounterpaties.DistinctBy(x=>x.codeFromBase), FilteredCars,FilteredInvoice,Invoices);
            var CreatedTask = _yandex.CreateTask(query);
            var routes = _yandex.GetLastResult();
            TempData["info"]=$"Успешно отправлено в Яндекс. Машин:{FilteredCars.Count}  Накладных:{countInvoice} ";
            return Page();
        }
        catch(Exception e)
        {
            ViewData["Error"] = e.Message;
            return Page();
        }
    }
}
