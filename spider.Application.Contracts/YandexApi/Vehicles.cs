using Newtonsoft.Json;

namespace spider.YandexApi
{

    public class Vehicles
    {
        public object id { get; set; }

        [JsonProperty("ref")]
        public string name { get; set; }

        public Cost cost { get; set; }
        public Shipment capacity { get; set; }

        public bool return_to_depot { get; set; }

        public Shifts[] shifts { get; set; }
    }

    public class Shifts
    {
        public object id { get; set; }
        public int minimal_stops{get;set;}
        public int maximal_stops{get;set;}
        public string time_window { get; set; }
        public PenaltyZ penalty { get; set; }
    }
    public class PenaltyZ
    {
        public StopLack stop_lack { get; set; }
        public StopExcess stop_excess{get;set;}
    }
    
    public class StopExcess
    {
        public int per_stop { get; set; }
    }
    public class StopLack
    {
        public int per_stop { get; set; }
    }

    public class Cost
    {
      public int km { get; set; }
      public int hour { get; set; }
      public int @fixed { get; set; }
      public int tonne_km { get; set; }
    }
}
