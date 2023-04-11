namespace spider.YandexApi.Result
{

    public class Route
    {
        public int arrival_time_s { get; set; }
        public int departure_time_s { get; set; }
        public FailedTimeWindow failed_time_window { get; set; }
        public Node node { get; set; }
        public int transit_distance_m { get; set; }
        public int transit_duration_s { get; set; }
        public List<object> violations { get; set; }
        public int waiting_duration_s { get; set; }
        public bool? multi_order { get; set; }
        public int? stop_sequence_number { get; set; }
        public ProbablyFailedTimeWindow probably_failed_time_window { get; set; }
    }
}