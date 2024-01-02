using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class ChartJsDoughnutChartResponseModel
    {
        public int DataCount { get; set; }

        public List<string> Labels { get; set; }

        public List<ChartJsDoughnutChartModel> DataSet { get; set; }
    }
}
