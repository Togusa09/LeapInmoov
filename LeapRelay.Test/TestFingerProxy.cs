using Leap;
using LeapSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapRelay.Test
{
    class TestFingerProxy : IFingerProxy
    {
        public Leap.Finger.FingerType Type
        {
            get;
            set;
        }

        public Vector Direction { get; set;  }


        public bool Extended
        {
            get;
            set;
        }
    }
}
