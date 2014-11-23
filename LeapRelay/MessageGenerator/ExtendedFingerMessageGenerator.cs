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
            return ConvertShortToBytes(finger.Extended ? (short)255 : (short)1);
        }
    }
}
