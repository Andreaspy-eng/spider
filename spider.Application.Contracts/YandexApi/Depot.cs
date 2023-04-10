using Newtonsoft.Json;

namespace spider.YandexApi
{
    public class Depot
    {
        public object id { get; set; }
        [JsonProperty("ref")]
        public string name { get; set; }

        public Point point { get; set; }    

        public string time_window { get; set; }
    }
}
