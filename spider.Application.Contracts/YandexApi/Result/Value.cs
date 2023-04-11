namespace spider.YandexApi.Result
{

    public class Value
    {
        public int finish_service_duration_s { get; set; }
        public bool flexible_start_time { get; set; }
        public string id { get; set; }
        public Penalty penalty { get; set; }
        public Point point { get; set; }
        public int preliminary_service_duration_s { get; set; }
        public string @ref { get; set; }
        public string routing_mode { get; set; }
        public int service_duration_s { get; set; }
        public string time_window { get; set; }
        public int total_service_duration_s { get; set; }
        public int? added_shared_service_duration_s { get; set; }
        public bool? allow_trailers { get; set; }
        public int? client_service_duration_s { get; set; }
        public string crossdock_mode { get; set; }
        public int? crossdock_service_duration_s { get; set; }
        public int? depot_duration_s { get; set; }
        public string description { get; set; }
        public bool? in_lifo_order { get; set; }
        public int? parking_service_duration_s { get; set; }
        public int? service_waiting_duration_s { get; set; }
        public int? shared_service_duration_s { get; set; }
        public ShipmentSize shipment_size { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public bool? use_in_proximity { get; set; }
    }
}