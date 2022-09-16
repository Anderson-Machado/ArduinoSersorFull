using RealTimeMeteorology.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealTimeMeteorology.Interfaces
{
    public interface ISensorsService
    {
        Task<List<ChartModel>> GetSensors();
    }
}
