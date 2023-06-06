using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace spider.Yandex
{
    public class AssignedRoute : Entity<Guid>
    {
        public string yandex_id { get; set; }
        public string vehicle_id { get; set; }
        public string driver_name { get;set; }
    }
}
