using System;
using Volo.Abp.Domain.Entities;

namespace spider.Products;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public string BarcodeUnit { get; set; }
    public string BarcodePackage { get; set; }
    public string BushId { get; set; }
    /// <summary>
    ///  Общий вес товара
    /// </summary>
    /// <value></value>
    public decimal WeightBrutto { get; set; }
     /// <summary>
    ///  "Чистый" вес товара без учета упаковки
    /// </summary>
    /// <value></value>
    public decimal WeightNetto { get; set; }
    public uint CountInPackage { get; set; }
    /// <summary>
    /// Код общероссийского классификатора единиц измерения
    /// https://classifikators.ru/okei
    /// </summary>
    /// <value></value>
    public uint OKEICode {get;set;}
    public uint VatRate {get;set;}

}
