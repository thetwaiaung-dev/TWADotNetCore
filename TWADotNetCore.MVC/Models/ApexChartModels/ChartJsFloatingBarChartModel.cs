using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class ChartJsFloatingBarChartModel
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }
        public string BorderColor { get; set; }
        public string BackgroundColor { get; set; }
    }
}
