namespace spider.YandexApi.Result
{

    public class Start
    {
        public int arrival_time_s { get; set; }
        public int departure_time_s { get; set; }
        public FailedTimeWindow failed_time_window { get; set; }
        public Node node { get; set; }
        public int transit_distance_m { get; set; }
        public int transit_duration_s { get; set; }
        public List<object> violations { get; set; }
        public int waiting_duration_s { get; set; }
    }
}