#include <Servo.h>

// Add something for storing the min/max for each finger

int fingerPins[] = {3, 5, 6, 7, 9, 10, 11, 13};
Servo* fingerServos[5];

void setup()
{
	for (int finger = 0; finger < 5; finger++)
	{
		fingerServos[finger] = new Servo();
		//fingerServos[finger]->attach(fingerPins[finger]);
	}

	//Initialize serial and wait for port to open:
	Serial.begin(9600);
	while (!Serial) {
		;
	}
}

void loop()
{

  /* add main program code here */

	if (Serial.available())
	{
		for (int finger = 0; finger < 5; finger++)
		{
			// Finger position between 0 and 255
			int fingerPosition = Serial.parseInt();
			SetFingerPosition(fingerServos[finger], finger, fingerPosition);
			delay(50);
		}
	}
	delay(10);
}

void SetFingerPosition(Servo* finger, int index, int position)
{
	if (position >= 0 && position <= 255)
	{
		if (!finger->attached())
			finger->attach(fingerPins[index]);
		finger->write(position);
	}
	else
		finger->detach();
}
