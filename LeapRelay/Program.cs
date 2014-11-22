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
           // serialPort.DataReceived += serialPort_DataReceived;
            serialPort.Open();

            if (!controller.IsConnected)
                Console.WriteLine("Leap controller is not connected");

            while(true)
            {
                UpdateArdiuno();
                Thread.Sleep(300);
            }

            // Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
        }

        static void UpdateArdiuno()
        {
            Frame frame = controller.Frame();
            if (frame.Fingers.Count() == 0)
            {
                serialPort.WriteLine("-1 -1 -1 -1 -1");
                return;
            }

            var output = string.Empty;
            output = string.Join("\t", frame.Fingers.Select(f => f.IsExtended ? 10 : 128));
            serialPort.WriteLine(output);
        }
    }
}
