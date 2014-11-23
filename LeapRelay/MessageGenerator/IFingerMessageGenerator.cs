using System;
namespace LeapSample.MessageGenerator
{
    public interface IFingerMessageGenerator
    {
        int ArduinoIndex { get; }
        byte[] GetCommandBytes(LeapSample.IFingerProxy finger, LeapSample.IHandProxy hand);
        Leap.Finger.FingerType Type { get; }
    }
}
