using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class HighChartsPieWithLegendResponseModel
    {
        public string ChartTitle { get; set; }
        public List<HighChartsPieWithLegendModel> ChartData { get; set; }
    }
}
