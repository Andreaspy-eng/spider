using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace spider.YandexApi;

        public class Locations
        {
            public string id { get; set; }
            public Point point { get; set; }
/*            public int shared_service_duration_s { get; set; }
            public int service_duration_s { get; set; }

            public Shipment shipment_size{ get; set; }*/
            public string time_window { get; set; }
           /* public Penalty penalty { get; set; }
            public string type { get; set; }
            public List<string> load_types { get; set; }       */  
        }

        public class Penalty
        {
            public OutTime out_of_time { get; set; }
            
        }

        public class OutTime
        {
            public int minute { get; set; }
            public int @fixed { get; set; }
        }
