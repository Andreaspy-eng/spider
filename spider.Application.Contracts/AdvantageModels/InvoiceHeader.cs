using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace spider.AdvantageModels
{
    /// <summary>
    /// Накладная T_OUT из учетной системы.
    /// </summary>
    public class InvoiceHeader : EntityDto<Guid>
    {
        public DateTime Date { get; set; }

        public DateTime ShipmentDate { get; set; }

        public TimeSpan Time { get; set; }

        public string CodeOperation { get; set; }

        public string ComputerNumber { get; set; }

        public string DocumentNumber { get; set; }

        public string Owner { get; set; }

        public string CounterpartyId { get; set; }

        public string CounterpartyName { get; set; }

        public string CounterpartyAddress { get; set; }

        public decimal Amount { get; set; }

        public string UniqueId { get; set; }

        //INN
        public string iNN { get; set; }
        //КПП
        public string kPP { get; set; }

        public string contractNo { get; set; }

        public DateTime contractDate { get; set; }

        public string SRC_DOC_ID { get; set; }

        public List<InvoiceBody> Details { get; set; }
    }
}
