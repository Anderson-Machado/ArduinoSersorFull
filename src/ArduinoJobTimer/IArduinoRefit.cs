using ArduinoJobTimer.Model;
using Refit;
using System.Threading.Tasks;

namespace ArduinoJobTimer
{
    public interface IArduinoRefit
    {
        [Get("/")]
        Task<ApiResponse<Arduino>> GetSensorAsync();
    }
}
