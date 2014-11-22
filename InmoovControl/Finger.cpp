#include "Finger.h"
#include "Arduino.h"

Finger::Finger(int fingerIndex, int min, int max) : index(fingerIndex) , minPos(min), maxPos(max)
{
	servo = new Servo();
}

void Finger::SetPosition(int position)
{
	
	if (position >= 0 || position <= 255)
	{
		// map the position to the range for this servo
		position = map(position, 0, 255, minPos, maxPos);
		//Serial.print(index);
		//Serial.print(" ");
		//Serial.println(position);
		if (!servo->attached())
			servo->attach(this->index);
		servo->write(position);
	}
	else
	{
		servo->detach();
	}
}

int Finger::GetPosition()
{
	return servo->read();
}