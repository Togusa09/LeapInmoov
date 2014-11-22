using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample
{
    public interface IHandProxy
    {
        IEnumerable<IFingerProxy> Fingers { get; }

        Vector Direction { get; }
    }
}
