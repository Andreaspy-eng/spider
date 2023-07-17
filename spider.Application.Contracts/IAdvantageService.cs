using spider.AdvantageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace spider
{
    public interface IAdvantageService
    {
        public IEnumerable<InvoiceHeader> getInvoices();
        public IEnumerable<InvoiceHeader> getInvoices(string thisDay);
        public IEnumerable<Driver> GetDrivers();
        public IEnumerable<Client> GetClients();
    }
}
