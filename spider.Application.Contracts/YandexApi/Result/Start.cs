namespace spider.YandexApi.Result
{

    public class Start
    {
        public double arrival_time_s { get; set; }
        public double departure_time_s { get; set; }
        public FailedTimeWindow failed_time_window { get; set; }
        public Node node { get; set; }
        public double transit_distance_m { get; set; }
        public double transit_duration_s { get; set; }
        public List<object> violations { get; set; }
        public int waiting_duration_s { get; set; }
    }
}