#include <Servo.h>
#include "Finger.h"

// PWM pins for leonardo
int fingerPins[] = {3, 5, 6, 7, 9, 10, 11, 13};
//Servo* fingerServos[5];
Finger* fingers[5];

void setup()
{
	for (int finger = 0; finger < 5; finger++)
	{
		// Am using 180 degree servos. Would be 255 for 90 degree
		fingers[finger] = new Finger(fingerPins[finger], 10, 128);
	}

	//Initialize serial and wait for port to open:
	Serial.begin(9600);
	while (!Serial) {
		;
	}
}

void loop()
{

	// Check if any serial messages
	
	if (Serial.available() > 0)
	{
		Serial.println("Serial Recieved");
		short buffer[5];
		Serial.readBytes((char*)buffer, 5 * sizeof(short));
		Serial.println((char*)buffer);

		for (int finger = 0; finger < 5; finger++)
		{
			Serial.println(buffer[finger]);
			fingers[finger]->SetPosition(buffer[finger]);
		}


		// Delay after change to give servos time to act
		//delay(10);
		
	}
	delay(10);
}


