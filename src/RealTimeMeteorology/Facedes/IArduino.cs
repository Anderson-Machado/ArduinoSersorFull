using RealTimeMeteorology.Model;
using Refit;
using System.Threading.Tasks;

namespace RealTimeMeteorology.Facedes
{
    public interface IArduino
    {
        [Get("/")]
        Task<ApiResponse<Arduino>> ReadSensors();
    }
}
