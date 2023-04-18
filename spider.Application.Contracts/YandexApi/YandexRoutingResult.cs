
using spider.YandexApi.Result;

namespace spider.YandexApi
{
    public class YandexRoutingResult
    {
        public string id { get; set; }
        public YandexRoutingTaskStatus status { get; set; }
        public string message { get; set; }

        public MatrixStatistics matrix_statistics { get; set; }
        public RouteResult result { get; set; }

    }
}