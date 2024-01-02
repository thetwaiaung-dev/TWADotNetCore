using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class HighChartLollipopResponseModel
    {
        public string XAxisTitle { get; set; }
        public List<HighChartLollipopModel> Data { get; set; }
    }
}
