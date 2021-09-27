using System;
using System.IO.Ports;
using System.Threading;
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

            // проверка поиска, подключения и настройки модема
            while (!modem.IsConnected)
            {
                if (modem.SearchPort())
                    modem.Connect();
                else
                    Console.WriteLine("Port :(");

                Console.WriteLine($"Modem port: {modem.PortName}");

                if (modem.IsConnected)
                {
                    Console.WriteLine($"Ping: {modem.PingModem()}");
                    Thread.Sleep(500);
                    Console.WriteLine($"Echo off: {modem.EchoDisable()}");
                    Thread.Sleep(500);
                    Console.WriteLine($"Auto Answer Disable: {modem.AutoAnswerDisable()}");
                    Thread.Sleep(500);
                    Console.WriteLine($"Auto Number Detection Enable: {modem.AutoNumberDetectionEnable()}");
                    Thread.Sleep(500);
                    Console.WriteLine($"Sms Text Mode Enable: {modem.SmsTextModeEnable()}");
                    Thread.Sleep(500);
                    Console.WriteLine($"Check Network Reg Type: {modem.CheckNetworkRegType()}");
                    Thread.Sleep(500);
                }
            }


            // проверка автообновления списка доступных портов в системе
            while (true)
            {
                foreach (var item in DeviceSerialPort.AvailableComPorts)
                {
                    Console.WriteLine($"Port {item.Key}; Status: {item.Value}");
                }
                Console.WriteLine($"\n");
                Thread.Sleep(5000);
            }
        }
    }
}
