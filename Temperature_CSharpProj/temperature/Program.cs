using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;

namespace temperature
{
    class Program
    {
        static SerialPort _port;

        static void Main(string[] args)
        {            
            _port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            if (!_port.IsOpen)
                _port.Open();
            _port.DataReceived += port_DataReceived;
            Console.ReadLine();
        }

        static void SendData(string data)
        {
            if (_port != null && _port.IsOpen)
                _port.Write(data);
            else
                throw new Exception("Port is closed");
        }

        static void RefrigerateOn()
        {
            SendData("ON");
        }

        static void RefrigerateOff()
        {
            SendData("OFF");
        }


        static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string line = _port.ReadLine();
            double temperatureCel;
            if (double.TryParse(line, out temperatureCel))
            {
                if(temperatureCel > 30.00)
                    RefrigerateOn();
                else
                    RefrigerateOff();
                //Thread.Sleep(5000);
                //Thread.Sleep(5000);
            }
            Console.Clear();
            Console.WriteLine(line);
        }
    }
}
