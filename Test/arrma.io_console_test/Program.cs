using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using arrma.device.core;

namespace arrma.wc.ui.console_test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Console test arrma.wc.device.core!!!\n\n");

            #region Test_DeviceSerialPort
            TestDevicePotocol testDeviceSerialPort = new TestDevicePotocol(new SerialPortConfig()
            {
                Name = "COM5"
            });
            Console.WriteLine($"Start get ports name\n");
            foreach (var item in TestDevicePotocol.ComPortsDictionary)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");   
            }

            Console.WriteLine($"Start open {testDeviceSerialPort.PortName}");
            testDeviceSerialPort.Connect();
            Console.WriteLine($"Port {testDeviceSerialPort.PortName} is connections: {testDeviceSerialPort.IsConnected}\n");

            Console.WriteLine($"Start closed {testDeviceSerialPort.PortName}");
            testDeviceSerialPort.Disconnect();
            Console.WriteLine($"Port {testDeviceSerialPort.PortName} is connections: {testDeviceSerialPort.IsConnected}\n");

            Console.WriteLine($"Start reconnect {testDeviceSerialPort.PortName}");
            testDeviceSerialPort.Connect();
            Console.WriteLine($"Port {testDeviceSerialPort.PortName} is connections: {testDeviceSerialPort.IsConnected}");
            await testDeviceSerialPort.Reconnect();
            Console.WriteLine($"Port {testDeviceSerialPort.PortName} is connections: {testDeviceSerialPort.IsConnected}\n");
            #endregion 
            

            Console.ReadKey();
        }
    }
}
