using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample.MessageGenerator
{
    public class HandMessageGenerator : IHandMessageGenerator
    {
        private static byte _CommandStart = 0x43;
        public List<IFingerMessageGenerator> Fingers { get; set; }

        public HandMessageGenerator(bool useExtension)
        {
            // Probably want this system to have diferent implimentations so we can work out finger position based on:
            // - Extended
            // - Finger angle
            // We could either make the detection mode based on a flag, or on the type of finger message generator.
            if (!useExtension)
            {
                Fingers = new List<IFingerMessageGenerator>
                {
                    new DirectionalFingerMessageGenerator(Finger.FingerType.TYPE_PINKY, 0, 0.0f, 90.0f),
                    new DirectionalFingerMessageGenerator(Finger.FingerType.TYPE_RING, 1, 0.0f, 90.0f),
                    new DirectionalFingerMessageGenerator(Finger.FingerType.TYPE_MIDDLE, 2, 0.0f, 90.0f),
                    new DirectionalFingerMessageGenerator(Finger.FingerType.TYPE_INDEX, 3, 0.0f, 90.0f),
                    new DirectionalFingerMessageGenerator(Finger.FingerType.TYPE_THUMB, 4,0.0f, 90.0f)
                };
            }
            else
            {
                Fingers = new List<IFingerMessageGenerator>
                {
                    new ExtendedFingerMessageGenerator(Finger.FingerType.TYPE_PINKY, 0),
                    new ExtendedFingerMessageGenerator(Finger.FingerType.TYPE_RING, 1),
                    new ExtendedFingerMessageGenerator(Finger.FingerType.TYPE_MIDDLE, 2),
                    new ExtendedFingerMessageGenerator(Finger.FingerType.TYPE_INDEX, 3),
                    new ExtendedFingerMessageGenerator(Finger.FingerType.TYPE_THUMB, 4)
                };
            }
        }

        public byte[] GetArduinoMessage(IHandProxy hand)
        {
            var bytes = new List<byte>();
            bytes.Add(_CommandStart);
            bytes.AddRange(GetBytesForHand(hand));
            return bytes.ToArray();
        }

        private byte[] GetBytesForHand(IHandProxy hand)
        {
            var bytes = new List<byte>();
            IEnumerable<IFingerProxy> leapFingers = new List<IFingerProxy>();
            if (hand != null)
                leapFingers = hand.Fingers;

            foreach(var finger in Fingers.OrderBy(f => f.ArduinoIndex))
            {
                var leapFinger = leapFingers.FirstOrDefault(f => f.Type == finger.Type);
                bytes.AddRange(finger.GetCommandBytes(leapFinger, hand));
            }

            return bytes.ToArray();
        }
    }
}
