using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample.MessageGenerator
{
    class ExtendedFingerMessageGenerator : BaseFingerMessageGenerator, IFingerMessageGenerator
    {
        public ExtendedFingerMessageGenerator(Finger.FingerType type, int index) 
            : base(type, index)
        {

        }

        public byte[] GetCommandBytes(IFingerProxy finger, IHandProxy hand)
        {
            // If finger isn't found, return -1
            if (finger == null || hand == null)
                return ConvertShortToBytes(-1);

            if (finger.Extended)
                return ConvertShortToBytes((short)255);

            return ConvertShortToBytes(0);
        }
    }
}
