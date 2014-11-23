using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample.MessageGenerator
{
    public class BaseFingerMessageGenerator
    {
        public Finger.FingerType Type { get; private set; }
        public int ArduinoIndex { get; private set; }

        protected BaseFingerMessageGenerator(Leap.Finger.FingerType type, int index)
        {
            Type = type;
            ArduinoIndex = index;
        }

        protected byte[] ConvertShortToBytes(short number)
        {
            return BitConverter.GetBytes(number);
        }
    }
}
