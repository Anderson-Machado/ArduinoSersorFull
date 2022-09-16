using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RealTimeMeteorology.HubConfig;
using RealTimeMeteorology.Interfaces;
using RealTimeMeteorology.Timer;
using System.Threading.Tasks;

namespace RealTimeMeteorology.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastController : Controller
    {
        private readonly ISensorsService _service;
        private readonly ILogger<ForecastController> _logger;
        private readonly IHubContext<ChartHub> _hub;
        private readonly TimerManager _timer;

        public ForecastController(ISensorsService service, ILogger<ForecastController> logger, TimerManager timer, IHubContext<ChartHub> hub)
        {
            _service = service;
            _logger = logger;
            _hub = hub; 
            _timer = timer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            if (!_timer.IsTimerStarted)
                _timer.PrepareTimer(async () => await _hub.Clients.All.SendAsync("TransferChartData", await _service.GetSensors()));
            return Ok(new { Message = "Request Completed" });
        }
    }
}
