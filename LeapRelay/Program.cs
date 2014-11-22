using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.IO.Ports;
using System.Threading;

namespace LeapSample
{
    class Program
    {
        static Controller controller;
        static SerialPort serialPort;
        static Object thisLock = new Object();

        static FingerMotionThing allFingers;

        static void Main(string[] args)
        {
            controller = new Controller();
            //SampleListener listener = new SampleListener();
            //controller.AddListener (listener);

            //// Keep this process running until Enter is pressed
            //Console.WriteLine("Press Enter to quit...");
            //Console.ReadLine();

            //controller.RemoveListener(listener);
            //controller.Dispose();

            serialPort = new SerialPort();
            serialPort.PortName = "COM17";
            serialPort.BaudRate = 57600;
            serialPort.DtrEnable = true;
            serialPort.DataReceived += serialPort_DataReceived;
            serialPort.Open();

            if (!controller.IsConnected)
                Console.WriteLine("Leap controller is not connected");

            allFingers = new FingerMotionThing();

            while(true)
            {
                UpdateArdiuno();
                Thread.Sleep(100);
            }

            // Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
        }

        private static void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
           // SafeWriteLine(indata);
        }

        static void UpdateArdiuno()
        {
            Frame frame = controller.Frame();
           
            byte[] output;
            if (frame.Hands.Count() == 0)
            {
                // -1 per finger
                output = new byte[] {
                    0x43,
                    0xFF, 0xFF, 
                                        0xFF, 0xFF, 
                                        0xFF, 0xFF, 
                                        0xFF, 0xFF, 
                                        0xFF, 0xFF};
            }
            else
            {
                var hand = frame.Hands.First();
                // pass the position values as shorts
                //output = hand.Fingers.Select(f => f.IsExtended ? (byte)0x00 : (byte)0xFF)
                //    .SelectMany(f => new byte[] { f, 0x00 }).ToArray();
               
                

                var angles = hand.Fingers.Select(f => hand.Direction.AngleTo(f.Direction).Remap(0.0f, 2.2f, 0.0f, 255));
                SafeWriteLine(string.Join("\t", angles));
                var temp = angles.SelectMany(f => new byte[] { (byte)(f), 0x00 }).ToList();
                temp.Insert(0, 0x43);
                output = temp.ToArray();
            }
            serialPort.Write(output, 0, output.Length);
        }

        private static void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
        }

        
    }
}
