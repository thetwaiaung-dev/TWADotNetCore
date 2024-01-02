using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class ChartJsStackedLineChartModel
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public string BorderColor { get; set; }
        public string BackgroundColor { get; set; }
        public bool Fill { get; set; }
    }
}