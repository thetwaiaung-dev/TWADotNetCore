using System;
using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class CanvasJsChartAreaChartResponseModel
    {
        public DateTime MinimumDate { get; set; }
        public DateTime MaximumDate { get; set; }
        public List<CanvasJsChartAreaChartModel> Data { get; set; }
    }
}
