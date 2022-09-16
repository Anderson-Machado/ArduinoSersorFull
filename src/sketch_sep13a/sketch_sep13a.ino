// DHT Temperature & Humidity Sensor
// Unified Sensor Library Example
// Written by Tony DiCola for Adafruit Industries
// Released under an MIT license.

// REQUIRES the following Arduino libraries:
// - DHT Sensor Library: https://github.com/adafruit/DHT-sensor-library
// - Adafruit Unified Sensor Lib: https://github.com/adafruit/Adafruit_Sensor


#include <DHT.h>
#include <DHT_U.h>
#include <Wire.h> 
#include <ArduinoJson.h>
#include <ThreeWire.h> //INCLUSÃO DA BIBLIOTECA
#include <RtcDS1302.h> //INCLUSÃO DA BIBLIOTECA

//LiquidCrystal_I2C lcd(IC2_ADDR, En, Rw, Rs, D4, D5, D6,D7);

#define DHTPIN A3 // Digital pin connected to the DHT sensor
// Feather HUZZAH ESP8266 note: use pins 3, 4, 5, 12, 13 or 14 --
// Pin 15 can work but DHT must be disconnected during program upload.

// Uncomment the type of sensor in use:
#define DHTTYPE DHT11 // DHT 11
//#define DHTTYPE    DHT22     // DHT 22 (AM2302)
//#define DHTTYPE    DHT21     // DHT 21 (AM2301)

// See guide for details on sensor wiring and usage:
//   https://learn.adafruit.com/dht/overview

DHT_Unified dht(DHTPIN, DHTTYPE);
int ledAzul2 = 10;
int ledLaranja = 11;
int ledAzul = 12;
int ledVermelho = 13;
int ledteste = 7;
uint32_t delayMS;
char caracter;
StaticJsonDocument<200> doc;
ThreeWire myWire(4, 5, 2); //OBJETO DO TIPO ThreeWire
RtcDS1302<ThreeWire> Rtc(myWire); //OBJETO DO TIPO RtcDS1302
#define countof(a) (sizeof(a) / sizeof(a[0]))

void setup()
{

  Serial.begin(115200);
  pinMode(ledAzul, OUTPUT);
  pinMode(ledVermelho, OUTPUT);
  pinMode(ledLaranja, OUTPUT);
  pinMode(ledAzul2, OUTPUT);
  pinMode(ledteste, OUTPUT);
  setupClock();

  //lcd.begin(16,2);
  // Initialize device.
  dht.begin();
  // Print temperature sensor details.
  sensor_t sensor;
  dht.temperature().getSensor(&sensor);
  // Print humidity sensor details.
  dht.humidity().getSensor(&sensor);

  // Set delay between sensor readings based on sensor details.
  delayMS = sensor.min_delay / 1000;
  setupClock();
}

void loop()
{
  //monta monta o formato de data e hora
    RtcDateTime dt = Rtc.GetDateTime();
    char datestring[20]; //VARIÁVEL ARMAZENA AS INFORMAÇÕES DE DATA E HORA
    char horastring[20];
    snprintf_P(datestring, 
            countof(datestring),
            PSTR("%02u/%02u/%04u"), //FORMATO DE EXIBIÇÃO DAS INFORMAÇÕES
            dt.Day(), //DIA
            dt.Month(), //MÊS
            dt.Year() //ANO
            ); 

            snprintf_P(horastring, 
            countof(horastring),
            PSTR("%02u:%02u:%02u"), //FORMATO DE EXIBIÇÃO DAS INFORMAÇÕES
            dt.Hour(), //HORA
            dt.Minute(), //MINUTOS
            dt.Second()); //SEGUNDOS
    
     //envia a json para serial
     if(Serial.available()>0){
     char c = Serial.read();
      if(c=='R'){
      sensors_event_t event;
      dht.temperature().getEvent(&event);
      doc["temperatura"] = event.temperature;
      sensors_event_t events;
      dht.humidity().getEvent(&events);
      doc["umidade"] = events.relative_humidity;
      doc["data"] = datestring;
      doc["hora"] = horastring;
      serializeJsonPretty(doc, Serial);
      }
     }
    
  // Delay between measurements.
  digitalWrite(ledteste, HIGH);
  delay(delayMS);
  // Get temperature event and print its value.
  sensors_event_t event;
  dht.temperature().getEvent(&event);
  
  if (isnan(event.temperature))
  {
    Serial.println(F("Error reading temperature!"));
  }
  else
  {
    if (event.temperature >= 29.50)
    {
      digitalWrite(ledVermelho, HIGH);
      digitalWrite(ledAzul, LOW);
    }
    if (event.temperature >= 27.50 && event.temperature < 29.00)
    {
      digitalWrite(ledVermelho, LOW);
      digitalWrite(ledAzul, HIGH);
    }
    //Serial.println(F("°C"));
  }
  // Get humidity event and print its value.
  dht.humidity().getEvent(&event);
  if (isnan(event.relative_humidity))
  {
    Serial.println(F("Error reading humidity!"));
  }
  else
  {  
    if (event.relative_humidity <= 45.00)
    {
      digitalWrite(ledAzul2, HIGH);
      digitalWrite(ledLaranja, LOW);
    }
    if (event.relative_humidity >= 50.00)
    {
      digitalWrite(ledAzul2, LOW);
      digitalWrite(ledLaranja, HIGH);
    }
 
  }

  Serial.flush();

}

void setupClock(){
    Rtc.Begin(); //INICIALIZA O RTC
    Serial.print("Compilado em: "); //IMPRIME O TEXTO NO MONITOR SERIAL
    RtcDateTime compiled = RtcDateTime(__DATE__, __TIME__); //VARIÁVEL RECEBE DATA E HORA DE COMPILAÇÃO
    
    if(Rtc.GetIsWriteProtected()){ //SE O RTC ESTIVER PROTEGIDO CONTRA GRAVAÇÃO, FAZ
        Serial.println("RTC está protegido contra gravação. Habilitando a gravação agora..."); //IMPRIME O TEXTO NO MONITOR SERIAL
        Rtc.SetIsWriteProtected(false); //HABILITA GRAVAÇÃO NO RTC
        Serial.println(); //QUEBRA DE LINHA NA SERIAL
    }

    if(!Rtc.GetIsRunning()){ //SE RTC NÃO ESTIVER SENDO EXECUTADO, FAZ
        Serial.println("RTC não está funcionando de forma contínua. Iniciando agora..."); //IMPRIME O TEXTO NO MONITOR SERIAL
        Rtc.SetIsRunning(true); //INICIALIZA O RTC
        Serial.println(); //QUEBRA DE LINHA NA SERIAL
    }

    RtcDateTime now = Rtc.GetDateTime(); //VARIÁVEL RECEBE INFORMAÇÕES
    if (now < compiled) { //SE A INFORMAÇÃO REGISTRADA FOR MENOR QUE A INFORMAÇÃO COMPILADA, FAZ
        Serial.println("As informações atuais do RTC estão desatualizadas. Atualizando informações..."); //IMPRIME O TEXTO NO MONITOR SERIAL
        Rtc.SetDateTime(compiled); //INFORMAÇÕES COMPILADAS SUBSTITUEM AS INFORMAÇÕES ANTERIORES
        Serial.println(); //QUEBRA DE LINHA NA SERIAL
    }
    else if (now > compiled){ //SENÃO, SE A INFORMAÇÃO REGISTRADA FOR MAIOR QUE A INFORMAÇÃO COMPILADA, FAZ
        Serial.println("As informações atuais do RTC são mais recentes que as de compilação. Isso é o esperado."); //IMPRIME O TEXTO NO MONITOR SERIAL
        Serial.println(); //QUEBRA DE LINHA NA SERIAL
    }
    else if (now == compiled) { //SENÃO, SE A INFORMAÇÃO REGISTRADA FOR IGUAL A INFORMAÇÃO COMPILADA, FAZ
        Serial.println("As informações atuais do RTC são iguais as de compilação! Não é o esperado, mas está tudo OK."); //IMPRIME O TEXTO NO MONITOR SERIAL
        Serial.println(); //QUEBRA DE LINHA NA SERIAL
    }
  }

 
