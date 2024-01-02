using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class ChartJsDoughnutChartModel
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }
        public List<string> BackgroundColor { get; set; }
    }
}
