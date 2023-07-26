using System.Collections.Generic;

public class InvoicePartialModel
{
    public IEnumerable<spider.AdvantageModels.InvoiceHeader> Invoices {get;set;}
    public bool isTodayInvoices{get;set;}
    public string Id {get { return isTodayInvoices?"checkboxes-today":"checkboxes-other";}}

}