namespace spider.YandexApi.Result
{
    public class Driving
    {
        public string requested_router { get; set; }
        public string used_router { get; set; }
        public int total_distances { get; set; }
        public int geodesic_distances { get; set; }
        public int slice_count { get; set; }

    }
}