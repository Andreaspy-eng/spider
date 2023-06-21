using Newtonsoft.Json;
namespace spider.YandexApi.Result
{
    public class Vehicle
    {
        public Capacity capacity { get; set; }
        public Cost cost { get; set; }
        public string depot_id { get; set; }
        public string id { get; set; }
        public int max_runs { get; set; }
        [JsonProperty("ref")]
        public string car_name { get; set; }
        public bool return_to_depot { get; set; }
        public string routing_mode { get; set; }
        public List<Shift> shifts { get; set; }
        public List<object> tags { get; set; }
        public List<object> visited_locations { get; set; }
    }

}