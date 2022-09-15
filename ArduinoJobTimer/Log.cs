using System.IO;
using System.Text.Json;

namespace ArduinoJobTimer
{
    public class Log
    {
        public static void GenerateLog(string messsage)
        {
            string strLogText = messsage;

            // Create a writer and open the file:
            StreamWriter log;

            if (!File.Exists("sensor.txt"))
            {
                log = new StreamWriter(@"sensor.txt");
            }
            else
            {
                log = File.AppendText("sensor.txt");
            }

            // Write to the file:
            log.WriteLine(strLogText);
            log.WriteLine();

            // Close the stream:
            log.Close();
        }

       
    }
}
