#ifndef Finger_h
#define Finger_h

#include <Servo.h>

class Finger
{
private:
	int index;
	int minPos, maxPos;
	Servo* servo;
public:
	Finger(int fingerIndex) : index(fingerIndex) {}
	Finger(int fingerIndex, int min, int max);
	void SetPosition(int position);
	int GetPosition();
};

#endif