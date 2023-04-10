using Newtonsoft.Json;

namespace spider.YandexApi
{

    public class Vehicles
    {
        public object id { get; set; }

        [JsonProperty("ref")]
        public string name { get; set; }

        public Shipment capacity { get; set; }

        public string return_to_depot { get; set; }

        [JsonProperty("shifts.0.time_window")]
        public string shifts0time_window { get; set; }
    }

}
