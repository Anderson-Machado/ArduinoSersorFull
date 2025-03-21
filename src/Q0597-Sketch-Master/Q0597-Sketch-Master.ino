#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include <ESP8266mDNS.h>
#include <DS1307.h>

ESP8266WebServer server(80);
char caracter;
String conteudo = "";
void setup(void) {
  Serial.begin(115200);
  Serial.println("setup");
  
  WiFi.mode(WIFI_STA);
  WiFi.begin("Promisse", "abc12318*");
  
  // Wait for connection
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println(WiFi.localIP());

  if (MDNS.begin("esp8266")) {
    Serial.println("MDNS responder started");
  }

  server.on("/", helloworld);
  server.begin();  
}

void loop(void) {
  server.handleClient();
    MDNS.update();
    /*while (Serial.available() == 0) {}
    String teststr = Serial.readString();
    teststr.trim();
    server.send(200, "text/x-json", teststr);
  if(Serial.available()==0){
    caracter = Serial.read();
    if(caracter!='\n'){
    conteudo.concat(caracter);
    server.send(200, "text/x-json", conteudo);
   }
  }*/
}

void helloworld() {
  Serial.print('R');  
  while (Serial.available() == 0) {}
    String teststr = Serial.readString();
    teststr.trim();
    server.send(200, "text/x-json", teststr);
    teststr = "";
  //server.send(200, "text/x-json", "{\"hello\":\"world\"}");
 }
