using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class ChartJsTimeScaleModel
    {
        public List<string> labels { get; set; }
        public double[][] number { get; set; }
    }
}
