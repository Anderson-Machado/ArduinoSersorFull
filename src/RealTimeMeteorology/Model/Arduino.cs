using Newtonsoft.Json;

namespace RealTimeMeteorology.Model
{
    public class Arduino
    {
        [JsonProperty("temperatura")]
        public double Temperatura { get; set; }

        [JsonProperty("Umidade")]
        public int Umidade { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("hora")]
        public string Hora { get; set; }
    }
}
