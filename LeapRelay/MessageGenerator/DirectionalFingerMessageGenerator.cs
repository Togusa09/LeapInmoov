using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace LeapSample.MessageGenerator
{
    /// <summary>
    /// Generates the message per finger that is sent to the arduino
    /// </summary>
    public class DirectionalFingerMessageGenerator : BaseFingerMessageGenerator, IFingerMessageGenerator
    {
        public float MinRange { get; private set; }
        public float MaxRange { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type">Type of finger</param>
        /// <param name="index">Arduino finger index</param>
        /// <param name="minRange">Minimum movement angle from palm</param>
        /// <param name="maxRange">Maximum movement angle from palm</param>
        public DirectionalFingerMessageGenerator(Finger.FingerType type, int index, float minRange, float maxRange) : base(type, index)
        {
            MinRange = minRange;
            MaxRange = maxRange;
        }

        /// <summary>
        /// Returns the byte array use for controlling the position of this finger on the arduino
        /// </summary>
        /// <param name="finger">Leap finger</param>
        /// <param name="hand">Leap hand</param>
        /// <returns></returns>
        private byte[] GetCommandBytes(Vector fingerDirection, Vector handDirection)
        {
            var angleRad = handDirection
                .AngleTo(fingerDirection);
            var angleDeg = ConvertRadiansToDegrees(angleRad);
            var angleMapped = (short)angleDeg.Remap(MinRange, MaxRange, 0.0f, 255);

            return ConvertShortToBytes(angleMapped);
        }

        public byte[] GetCommandBytes(IFingerProxy finger, IHandProxy hand)
        {
            // If finger isn't found, return -1
            if (finger == null || hand == null)
                return ConvertShortToBytes(-1);

            return GetCommandBytes(finger.Direction, hand.Direction);
        }

        private float ConvertRadiansToDegrees(float rads)
        {
            return (rads / (float)Math.PI) * 180.0f;
        }

        
    }
}
