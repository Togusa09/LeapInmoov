using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.IO.Ports;
using System.Threading;
using LeapSample.MessageGenerator;
using LeapSample.LeapProxy;

namespace LeapSample
{
    class Program
    {
        static Controller controller;
        static SerialPort serialPort;
        static Object thisLock = new Object();

        static HandMessageGenerator handMessageGenerator;

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
            serialPort.PortName = "COM7";
            serialPort.BaudRate = 57600;
            serialPort.DtrEnable = true;
            serialPort.DataReceived += serialPort_DataReceived;
            serialPort.Open();

            if (!controller.IsConnected)
                Console.WriteLine("Leap controller is not connected");

            handMessageGenerator = new HandMessageGenerator(true);
            Thread.Sleep(1000);
            while(true)
            {
                UpdateArdiuno();
                Thread.Sleep(100);
            }
        }

        private static void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            SafeWriteLine(indata);
        }

        static void UpdateArdiuno()
        {
            Frame frame = controller.Frame();
            // We just want the first hand at the moment, and to pass on null if there aren't any
            var hand = frame.Hands.Any() ? new LeapHandProxy(frame.Hands.First()) : null;

            byte[] output;
            //var handMessageGenerator = new HandMessageGenerator(false);
            output = handMessageGenerator.GetArduinoMessage(hand);

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
