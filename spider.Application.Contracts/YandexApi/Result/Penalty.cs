namespace spider.YandexApi.Result;
public class Penalty
    {
        public int hour { get; set; }
        public int stop { get; set; }
        public DropPenaltyPercentage drop_penalty_percentage { get; set; }
        public OutOfTime out_of_time { get; set; }
        public Throughput throughput { get; set; }
        public int? drop { get; set; }
    }
