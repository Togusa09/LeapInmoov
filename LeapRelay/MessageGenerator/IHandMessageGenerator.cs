using System;
namespace LeapSample.MessageGenerator
{
    interface IHandMessageGenerator
    {
        byte[] GetArduinoMessage(IHandProxy hand);
    }
}
