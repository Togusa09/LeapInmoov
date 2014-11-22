using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace LeapSample
{
    public class FingerModel
    {
        public Finger.FingerType Type { get; private set; }
        public int ArduinoIndex { get; private set; }
        public float MinRange { get; private set; }
        public float MaxRange { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type">Type of finger</param>
        /// <param name="index">Arduino finger index</param>
        /// <param name="minRange">Minimum movement angle from palm</param>
        /// <param name="maxRange">Maximum movement angle from palm</param>
        public FingerModel(Finger.FingerType type, int index, float minRange, float maxRange)
        {
            Type = type;
            ArduinoIndex = index;
            MinRange = minRange;
            MaxRange = maxRange;
        }

        /// <summary>
        /// Returns the byte array use for controlling the position of this finger on the arduino
        /// </summary>
        /// <param name="finger">Leap finger</param>
        /// <param name="hand">Leap hand</param>
        /// <returns></returns>
        public IEnumerable<byte> GetBytes(Vector fingerDirection, Vector handDirection)
        {
            var angleRad = handDirection
                .AngleTo(fingerDirection);
            var angleDeg = (angleRad / (float)Math.PI) * 180;
            var angleMapped = angleDeg.Remap(MinRange, MaxRange, 0.0f, 255);

            return new byte[] { (byte)(angleMapped), 0x00 };
        }
    }
}
