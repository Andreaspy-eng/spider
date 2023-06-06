using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace spider.Yandex
{
    public interface IAssignedRoutesService : ICrudAppService<
            AssignedRoutesDTO,
            Guid,
            PagedAndSortedResultRequestDto,
            CrUpAssignedRoutes>
    {
    }
}
