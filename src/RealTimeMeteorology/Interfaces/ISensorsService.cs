using RealTimeMeteorology.Model;
using System.Threading.Tasks;

namespace RealTimeMeteorology.Interfaces
{
    public interface ISensorsService
    {
        Task<Arduino> GetSensors();
    }
}
