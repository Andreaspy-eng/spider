using spider.AdvantageModels;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace spider.Products;

public interface IProductAppService :
    ICrudAppService< //Defines CRUD methods
        InvoiceHeader, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CrUpProductDto> //Used to create/update a book
{

}
