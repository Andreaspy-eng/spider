using System;
using System.Collections.Generic;
using System.Text;

namespace spider.AdvantageModels
{
    public class Counterparty
    {
        public int id { get; set; }
        public string codeFromBase { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int type { get; set; }
        public List<WorkSchedule> workSchedule { get; set; }

         public string InvoiceNumber {get;set;}
    }

    public class WorkSchedule
    {
        public int id { get; set; }
        public int counterpartyId { get; set; }
        public int day { get; set; }
        public string openTime { get; set; }
        public string closeTime { get; set; }
    }
}
