using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace spider.Yandex
{
    public interface IResultTokenService:ICrudAppService< 
            ResultTokenDTO, 
            Guid, 
            PagedAndSortedResultRequestDto, 
            CrUpResultToken>
    {
    }
}
