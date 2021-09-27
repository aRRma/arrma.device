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
                    int delay = 50;
                    Console.WriteLine($"Ping: {modem.PingModem()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"Echo off: {modem.EchoDisable()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"Auto Answer Disable: {modem.AutoAnswerDisable()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"Auto Number Detection Enable: {modem.AutoNumberDetectionEnable()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"Sms Text Mode Enable: {modem.SmsTextModeEnable()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"Check Network Reg Type: {modem.CheckNetworkRegType()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"Check Network QoS: {modem.CheckNetworkQoS()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"GetManufactureId: {modem.GetManufactureId()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"GetModelId: {modem.GetModelId()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"GetSoftVersion: {modem.GetSoftVersion()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"GetModemSerialNumber: {modem.GetModemSerialNumber()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"GetSimSerialNumber: {modem.GetSimSerialNumber()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"GetOperatorInfo: {modem.GetOperatorInfo()}");
                    Thread.Sleep(delay);
                    Console.WriteLine($"GetOperatorType: {modem.GetOperatorType()}");
                    Thread.Sleep(delay);

                    Console.WriteLine($"GetSimBalance: {modem.GetSimBalance(SimOperatorType.BEELINE)}");
                    Thread.Sleep(delay);

                    Console.WriteLine($"ModemReboot: {modem.ModemReboot()}");
                    Thread.Sleep(delay);
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
