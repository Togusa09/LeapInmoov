using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample.LeapProxy
{
    class LeapHandProxy : IHandProxy
    {
        private Hand _Hand;

        public LeapHandProxy(Hand hand)
        {
            _Hand = hand;
        }

        public IEnumerable<IFingerProxy> Fingers
        {
            get { return _Hand.Fingers.Select(f => new LeapFingerProxy(f)); }
        }


        public Vector Direction
        {
            get
            {
                return _Hand.Direction;
            }
        }
    }
}
