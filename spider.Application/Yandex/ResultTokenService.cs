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
    public class ResultTokenService : CrudAppService<
        ResultToken,
        ResultTokenDTO, 
        Guid,
        PagedAndSortedResultRequestDto, 
        CrUpResultToken>, 
    IResultTokenService 
    {
        public ResultTokenService(IRepository<ResultToken, Guid> repository)
            : base(repository)
        {

        }
    }
}
