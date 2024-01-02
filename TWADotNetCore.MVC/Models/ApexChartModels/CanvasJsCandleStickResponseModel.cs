using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class CanvasJsCandleStickResponseModel
    {
        public string Text { get; set; }
        public CandleStickAxisXModel AxisX { get; set; }
        public CandleStickAxisYModel AxisY { get; set; }
        public List<CanvasJsCandleStickModel> Data { get; set; }
    }
}
