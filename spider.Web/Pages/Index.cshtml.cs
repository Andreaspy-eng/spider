using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using spider.AdvantageModels;
using System.Collections;
using System.Collections.Generic;

namespace spider.Web.Pages;
public class IndexModel : spiderPageModel
{
    private readonly IAdvantageService _advantage;
    public IEnumerable<InvoiceHeader> invoices;
    public IndexModel(IAdvantageService advantage)
    {
        _advantage = advantage;
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
}
