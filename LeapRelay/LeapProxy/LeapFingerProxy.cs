using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample.LeapProxy
{
    class LeapFingerProxy : IFingerProxy
    {
        private Finger _Finger;

        public LeapFingerProxy(Finger finger)
        {
            _Finger = finger;
        }

        public Finger.FingerType Type
        {
            get { return _Finger.Type(); }
        }

        public Vector Direction { get { return _Finger.Direction; } }


        public bool Extended
        {
            get { return _Finger.IsExtended; }
        }
    }
}
