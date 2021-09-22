using System;
using System.IO.Ports;
using Arrma.Device;
using Arrma.Device.Basic;
using Arrma.Device.Core;
using Arrma.Device.Core.SerialPort;
using Arrma.Device.Core.Transport;
using Arrma.Device.Enum;

namespace Arrma.Test.TeleofisRx
{
    class Program
    {
        public static Device.TeleofisRx modem;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            modem = new Device.TeleofisRx(new SerialPortConfig()
            {
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 100,
                WriteTimeout = 100
            });
            
            modem.SearchPort(new AtRequest("AT", ""), 6);
            modem.SendCommand(new AtRequest(modem.Commands[AtCommand.AT_], ""));
        }
    }
}
