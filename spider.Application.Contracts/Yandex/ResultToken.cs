using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace spider.Yandex
{
    public class ResultTokenDTO:EntityDto<Guid>
    {
        public DateTime CreationDate { get; set; }
        public string yandex_id { get; set; }
        public string message { get; set; }
    }
}
