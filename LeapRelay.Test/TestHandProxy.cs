using Leap;
using LeapSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapRelay.Test
{
    class TestHandProxy : IHandProxy
    {
        public IEnumerable<IFingerProxy> Fingers
        {
            get;
            set;
        }

        public Leap.Vector Direction
        {
            get;
            set;
        }
    }
}
