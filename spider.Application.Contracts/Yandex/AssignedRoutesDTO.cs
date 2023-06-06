using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace spider.Yandex
{
    public class AssignedRoutesDTO : EntityDto<Guid>
    {
        public string yandex_id { get; set; }
        public string vehicle_id { get; set; }
        public string driver_name { get; set; }
    }
}
