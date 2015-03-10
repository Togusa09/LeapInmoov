#include <Servo.h>
#include "Finger.h"

// PWM pins for leonardo
int fingerPins[] = {2,3,4,5,6,7,8,9,10,11};
//Servo* fingerServos[5];
Finger* fingers[5];

void setup()
{
	for (int finger = 0; finger < 5; finger++)
	{
		// Am using 180 degree servos. Would be 255 for 90 degree
	    fingers[finger] = new Finger(fingerPins[finger], 10, 255);
	}
        //for (int finger = 5; finger < 10; finger++)
	//{
		// Am using 180 degree servos. Would be 255 for 90 degree
	 //   fingers[finger] = new Finger(fingerPins[finger], 10, 128);
	//}

	//Initialize serial and wait for port to open:
	Serial.begin(57600);
	//while (!Serial) {
	//	;
	//}
}

void loop()
{

	// Check if any serial messages
	
	if (Serial.available() > 0)
	{
		//Serial.println("Serial Recieved");
		while (Serial.available() && Serial.read() != 0x43) {  }
		Serial.println("now");


		short buffer[5];
		Serial.readBytes((char*)buffer, 5 * sizeof(short));


		Serial.println("Recieved");

		for (int finger = 0; finger < 5; finger++)
		{
			Serial.print(buffer[finger]);
			Serial.print("\t");
			fingers[finger]->SetPosition(buffer[finger]);
		}
		Serial.println("");


		// Delay after change to give servos time to act
		delay(15);
		
	}
	delay(10);
}


