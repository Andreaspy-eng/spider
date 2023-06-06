using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace spider.Yandex
{
    public class AssignedRoutesService : CrudAppService<
        AssignedRoute,
        AssignedRoutesDTO,
        Guid,
        PagedAndSortedResultRequestDto,
        CrUpAssignedRoutes>,
    IAssignedRoutesService
    {
        public AssignedRoutesService(IRepository<AssignedRoute, Guid> repository)
            : base(repository)
        {

        }
    }
}
