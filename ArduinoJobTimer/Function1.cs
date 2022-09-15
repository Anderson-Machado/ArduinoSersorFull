using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArduinoJobTimer
{
    public class Function1
    {
        [FunctionName("Function1")]
        //0 0 * * * * - a cada hora
        // 20 0/1 * * * * - a cada 20 segundos
        public async Task RunAsync([TimerTrigger("0 0 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var client = RestService.For<IArduinoRefit>("http://esp8266.local");
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var obj = await client.GetSensorAsync();

            if(obj.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (obj.Content is not null)
                {
                    log.LogInformation($"Gerou leitura dos sensores com sucesso em {DateTime.Now}");
                    log.LogInformation($"Temperatura de {obj.Content.Temperatura}ºC e umidade {obj.Content.Umidade}% em {obj.Content.Hora} do dia {obj.Content.Data}");
                    Log.GenerateLog($"Temperatura de {obj.Content.Temperatura}º e umidade {obj.Content.Umidade}% em {obj.Content.Hora} do dia {obj.Content.Data}");
                }
                else
                {
                    log.LogError($"Content retornou vazio.");
                }
            }
            else
            {
                log.LogError($"A consulta dos sensores falhou retornando StatusCode:{obj.StatusCode}");
            }
            
        }
    }
}
