using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillTracker.Models
{
    public class TimelineData
    {
        public string time { get; set; }
        public string formattedTime { get; set; }
        public string formattedAxisTime { get; set; }
        public List<int> value { get; set; }
        public List<bool> hasData { get; set; }
        public List<string> formattedValue { get; set; }
    }

    public class Default
    {
        public List<TimelineData> timelineData { get; set; }
        public List<int> averages { get; set; }
    }

    public class Root
    {
        public Default @default { get; set; }
    }
}