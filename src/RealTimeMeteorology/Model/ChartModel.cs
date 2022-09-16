using System.Collections.Generic;

namespace RealTimeMeteorology.Model
{
    public class ChartModel
    {
        public List<double> Data { get; set; }
        public string? Label { get; set; }
        public string? BackgroundColor { get; set; }
        public ChartModel()
        {
            Data = new List<double>();
        }
    }
}
