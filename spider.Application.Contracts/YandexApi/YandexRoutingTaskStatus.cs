using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace spider.YandexApi
{
    public class YandexRoutingTaskStatus
    {   
        [NonSerialized]
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public double completed { get; set; }
        public double estimate { get; set; }
        public double calculated { get; set; }
        public double started { get; set; }
        public double queued { get; set; }
        public double matrix_downloaded { get; set; }

        public DateTime estimateDateTimeUTC { get { return ConvertFromUnixDateStampt(completed); } }
        public DateTime completedDateTimeUTC { get { return ConvertFromUnixDateStampt(completed); } }
        public DateTime calculatedDateTimeUTC { get { return ConvertFromUnixDateStampt(calculated); } }
        public DateTime startedDateTimeUTC { get { return ConvertFromUnixDateStampt(started); } }
        public DateTime queuedDateTimeUTC {
            get {
                return ConvertFromUnixDateStampt(queued); } }
        public DateTime matrix_downloadedDateTimeUTC { get { return ConvertFromUnixDateStampt(matrix_downloaded); } }

        private DateTime ConvertFromUnixDateStampt(double timeValue)
        {
           return _epoch.AddSeconds(timeValue);
        }
    }
}
