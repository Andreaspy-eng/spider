using Newtonsoft.Json;
using spider.AdvantageModels;
using spider.YandexApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace spider.Products;

public class ProductAppService :
    CrudAppService<
        Product, //The Book entity
        InvoiceHeader, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CrUpProductDto>, //Used to create/update a book
    IProductAppService //implement the IBookAppService
{
    public static IEnumerable<InvoiceHeader> getInvoices()
    {
        using (StreamWriter sw = new StreamWriter(@"C:\Users\Andrew\Desktop\api.txt"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            JsonSerializer serializer = new();
            serializer.Serialize(writer, new QueryCreateRoteList());
        }
        using (HttpClient Client = new())
        {

            Client.BaseAddress = new Uri("http://192.168.1.222:8079/advantage/");
            using (Stream s = Client.GetStreamAsync("api/Invoices/all?Start=2023-01-04&End=2023-01-04&Codes=Ò-ð").Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                Newtonsoft.Json.JsonSerializer serializer = new();
                var InvoiceList = serializer.Deserialize<IEnumerable<InvoiceHeader>>(reader);
                return InvoiceList;
            };

        }
    }
    public ProductAppService(IRepository<Product, Guid> repository)
        : base(repository)
    {
    }
}
