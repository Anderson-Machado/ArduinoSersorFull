using RealTimeMeteorology.Facedes;
using RealTimeMeteorology.Interfaces;
using RealTimeMeteorology.Model;
using System.Threading.Tasks;

namespace RealTimeMeteorology.Service
{
    public class SensorsService : ISensorsService
    {
        private readonly IArduino _arduino;

        public SensorsService(IArduino arduino)
        {
            _arduino = arduino;
        }

        public async Task<Arduino> GetSensors()
        {
            var ret = await _arduino.ReadSensors();
            if (ret.StatusCode == System.Net.HttpStatusCode.OK && ret.Content is not null)
            {
                return ret.Content;
            }
            else
            {
                return new Arduino();
            }
        }
    }
}
