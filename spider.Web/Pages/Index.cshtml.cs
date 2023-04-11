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
        invoices = _advantage.getInvoices();
    }
}
