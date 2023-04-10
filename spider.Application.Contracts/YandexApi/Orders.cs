using Newtonsoft.Json;

namespace spider.YandexApi
{
    public class Orders
    {
        public string id { get; set; }

        [JsonProperty("point.lat")]
        public double lat { get; set; }

        [JsonProperty("point.lon")]
        public double lon { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public string time_window { get; set; }
        public object comments { get; set; }
        public bool hard_window { get; set; }
        public int shared_service_duration_s { get; set; }
        public int service_duration_s { get; set; }

        [JsonProperty("shipment_size.weight_kg")]
        public int shipment_sizeweight_kg { get; set; }

        [JsonProperty("shipment_size.units")]
        public object shipment_sizeunits { get; set; }

        [JsonProperty("shipment_size.volume.width_m")]
        public object shipment_sizevolumewidth_m { get; set; }

        [JsonProperty("shipment_size.volume.depth_m")]
        public object shipment_sizevolumedepth_m { get; set; }

        [JsonProperty("shipment_size.volume.height_m")]
        public object shipment_sizevolumeheight_m { get; set; }
    }


}
