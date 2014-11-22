using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample
{
    public interface IFingerProxy
    {
        Leap.Finger.FingerType Type { get; }
        Vector Direction { get; }
    }
}
