using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample
{
    class FingerMotionThing
    {
        private static byte _CommandStart = 0x43;
        public List<FingerModel> Fingers { get; set; }

        public FingerMotionThing()
        {
            // Probably want this system to have diferent implimentations so we can work out finger position based on:
            // - Extended
            // - Finger angle

            Fingers = new List<FingerModel>
            {
                new FingerModel(Finger.FingerType.TYPE_PINKY, 0, 0.0f, 2.2f),
                new FingerModel(Finger.FingerType.TYPE_RING, 1, 0.0f, 2.2f),
                new FingerModel(Finger.FingerType.TYPE_MIDDLE, 2, 0.0f, 2.2f),
                new FingerModel(Finger.FingerType.TYPE_INDEX, 3, 0.0f, 2.2f),
                new FingerModel(Finger.FingerType.TYPE_THUMB, 4,0.0f, 0.5f)
            };
        }

        private byte[] GetArduinoMessage(Hand hand)
        {
            var bytes = new List<byte>();
            bytes.Add(_CommandStart);
            bytes.AddRange(GetBytesForHand(hand));
            return bytes.ToArray();
        }

        private byte[] GetBytesForHand(Hand hand)
        {
            var bytes = new List<byte>();

            foreach(var finger in hand.Fingers)
            {
                var fingerModel = Fingers.First(f => f.Type == finger.Type());
                bytes.AddRange(fingerModel.GetBytes(finger.Direction, hand.Direction));
            }

            return bytes.ToArray();
        }
    }
}
