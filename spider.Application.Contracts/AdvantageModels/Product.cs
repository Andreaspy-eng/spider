using System;
using System.Collections.Generic;
using System.Text;

namespace spider.AdvantageModels
{
    /// <summary>
    /// Товар VI из учетной системы.
    /// </summary>
    public class Product
    {
        public string CodeGroup { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal CountInPackage { get; set; }
        public int MinCount { get; set; }
        public int NP { get; set; }
        public string CertificateNumber { get; set; }
        public string SertDatv { get; set; }
        public string SertDate { get; set; }
        public string K_Sert_Org { get; set; }
        public int VatRate { get; set; }
        public string Notes { get; set; }
        public string Country { get; set; }
        public string Gtd_No { get; set; }
        public decimal MaxDiscount { get; set; }
        public string C1 { get; set; }
        public string C2 { get; set; }
        public string C3 { get; set; }
        public string WarehouseCode { get; set; }
        public string C4 { get; set; }
        public string C5 { get; set; }
        public string C6 { get; set; }
        public string C7 { get; set; }
        public string C8 { get; set; }
        public string Category { get; set; }
        public string Ku_Datv { get; set; }
        public string ImplementationPeriod { get; set; }
        public decimal Weight { get; set; }
        public decimal WeightBrutto { get; set; }
        public string TypePackage { get; set; }
        public string SCode { get; set; }
        public string K_VI_POST { get; set; }
        public string K_VI_POST2 { get; set; }
        public string K_VI_POST3 { get; set; }
        public decimal CountInPackage2 { get; set; }
        public decimal Volume { get; set; }
        public string SCode2 { get; set; }
        public decimal MinPrice { get; set; }
    }
}