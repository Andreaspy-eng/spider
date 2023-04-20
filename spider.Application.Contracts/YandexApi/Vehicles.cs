using Newtonsoft.Json;

namespace spider.YandexApi
{

    public class Vehicles
    {
        public object id { get; set; }

        [JsonProperty("ref")]
        public string name { get; set; }

        public Shipment capacity { get; set; }

        public bool return_to_depot { get; set; }

        public Shifts[] shifts { get; set; }
    }

    public class Shifts
    {
        public object id { get; set; }
        public string time_window { get; set; }
    }
}
