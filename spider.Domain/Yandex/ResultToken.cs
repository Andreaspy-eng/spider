using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace spider.Yandex
{
    public class ResultToken:Entity<Guid>
    {
        public DateTime CreationDate { get; set; }
        public string yandex_id { get; set; }
        public string message { get; set; }
    }
}
