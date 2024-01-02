using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class ChartJsScatterChartResponseModel
    {
        public string Label { get; set; }
        public string backgroundColor { get; set; }
        public List<ChartJsScatterChartModel> Data { get; set; }
    }
}
