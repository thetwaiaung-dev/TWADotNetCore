using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class ChartJsFloatingBarChartResponseModel
    {
        public int DataCount { get; set; }
        public List<string> Labels { get; set; }
        public List<ChartJsFloatingBarChartModel> DataSets { get; set; }
    }
}
