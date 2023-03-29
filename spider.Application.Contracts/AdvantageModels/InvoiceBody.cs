using System;
using System.Collections.Generic;
using System.Text;

namespace spider.AdvantageModels
{
    /// <summary>
    /// Детали накладной T_MOVE из учетной системы.
    /// </summary>
    public class InvoiceBody
    {
        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public decimal Count { get; set; }

        public decimal PackageCount { get; set; }

        public Product Product { get; set; }
    }
}
