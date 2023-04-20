using spider.AdvantageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace spider
{
    public interface ICounterpartyService
    {
        public IEnumerable<Counterparty> getCounterparties();
    }
}
