using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RealTimeMeteorology.Facedes;
using RealTimeMeteorology.Interfaces;
using RealTimeMeteorology.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace RealTimeMeteorology.Service
{
    public class SensorsService : ISensorsService
    {
        private readonly IArduino _arduino;

        private readonly List<ChartModel> _listChart;
        public SensorsService(IArduino arduino, List<ChartModel> listChart)
        {
            _arduino = arduino;
            _listChart = listChart;

        }

        public async Task<List<ChartModel>> GetSensors()
        {
            var _chart = new ChartModel();
            var ret = await _arduino.ReadSensors();
            Random r = new Random();
            var BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), 0);
            if (ret.StatusCode == System.Net.HttpStatusCode.OK && ret.Content is not null)
            {

                _chart.Data.Add(ret.Content.Temperatura);
                _chart.Label = ret.Content.Hora;
                _chart.BackgroundColor = ColorToHex(BackColor); 
                _listChart.Add(_chart);
                return _listChart;
            }
            else
            {
                return new List<ChartModel>();
            }
        }

        private string ColorToHex( Color color)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb()));
        }
    }
}
