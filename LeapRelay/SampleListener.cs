using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapSample
{
    class SampleListener : Listener
    {
        private Object thisLock = new Object();

        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
        }

        public override void OnConnect(Controller controller)
        {
            SafeWriteLine("Connected");
        }


        public override void OnFrame(Controller controller)
        {
            Frame frame = controller.Frame();
            if (frame.Fingers.Count() == 0)
                return;

            var output = string.Empty;
            output = string.Join("\t", frame.Fingers.Select(f => f.IsExtended.ToString()));
            SafeWriteLine(output);
            //SafeWriteLine("Frame id: " + frame.Id
            //         + ", timestamp: " + frame.Timestamp
            //         + ", hands: " + frame.Hands.Count
            //         + ", fingers: " + frame.Fingers.Count
            //         + ", tools: " + frame.Tools.Count
            //         + ", gestures: " + frame.Gestures().Count);
        }
    }
}
