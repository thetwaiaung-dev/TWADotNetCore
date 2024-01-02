using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
	public class CanvasJsSplineChartResponseModel
	{
		public string Title { get; set; }
		public string AxisYTitle { get; set; }
		public List<CanvasJsSplineChartModel> DataPoints { get; set; }
	}
}
