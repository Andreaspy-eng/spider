using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spider.YandexApi
{
    public class YandexRoutingTaskCreatedResponse
    {
        public string id { get; set; }
        public YandexRoutingTaskStatus status {get;set;}
        public string message {get; set;}
    }
}
