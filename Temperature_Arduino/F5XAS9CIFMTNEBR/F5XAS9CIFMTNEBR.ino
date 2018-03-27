#include <OneWire.h>
#include <DallasTemperature.h>
 
#define ONE_WIRE_BUS 2
 
OneWire oneWire(ONE_WIRE_BUS);
 
DallasTemperature sensors(&oneWire);
 
void setup(void)
{
  Serial.begin(9600);
  pinMode(3, OUTPUT);
  sensors.begin();
}

String inputString = "";
 
void loop(void)
{
  delay(2000);

  sensors.requestTemperatures();

  Serial.println(sensors.getTempCByIndex(0));

  if (inputString.indexOf("ON") > -1 ) {
    digitalWrite(3, HIGH);
    inputString = "";
  }
  if (inputString.indexOf("OFF") > -1) {
    digitalWrite(3, LOW);
    inputString = "";
  }
}

void serialEvent() {
  while (Serial.available() > 0) {
    char inChar = (char)Serial.read();
    inputString += inChar;
  }
}
