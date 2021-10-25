using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
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

        static async Task Main(string[] args)
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
                Console.WriteLine(new string('=', 100) + "\n");
            }

            #region Методы для тестов
            //TestBaseAtProtocolMethods();
            await TestBaseAtModemMethods();
            #endregion

            while (true)
            {
                foreach (var item in DeviceSerialPort.GetSortComPorts())
                {
                    Console.WriteLine($"Port [{item}];");
                }
                Console.WriteLine($"\n");
                await Task.Delay(5000);
            }
        }

        private static void TestBaseAtProtocolMethods()
        {
            if (modem.IsConnected)
            {
                Console.WriteLine("Start \"TestAllBaseMethods\".");
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
                Console.WriteLine($"GetOperatorInfo: {modem.GetOperatorName()}");
                Thread.Sleep(delay);
                Console.WriteLine($"GetOperatorType: {modem.GetOperatorType()}");
                Thread.Sleep(delay);
                Console.WriteLine($"GetSimBalance: {modem.GetSimBalance(SimOperatorType.BEELINE)} руб.");
                Thread.Sleep(delay);
                //Console.WriteLine($"ModemReboot: {modem.ModemReboot()}");
                //Thread.Sleep(delay);
                Console.WriteLine($"Send sms: {modem.SendSmsOnNumber(("+79161884955", $"[{DateTime.Now}] TEST"))}");
                Thread.Sleep(delay);
                Console.WriteLine("Press something to continue.\n");
                Console.ReadKey();
            }
        }
        private static async Task TestBaseAtModemMethods()
        {
            Console.WriteLine("Start \"TestBaseAtModemMethods\".");
            Console.WriteLine($"BaseInitModemAsync return: {await modem.BaseInitModemAsync()}");
            Console.WriteLine($"FillModemBaseData return: {modem.FillModemBaseData()}");
            Console.WriteLine("Start test CheckSmsTimer");
            modem.OutputSmsList.Add(("+79161884955", $"[{DateTime.Now}] SMS1"));
            modem.OutputSmsList.Add(("+79161884955", $"[{DateTime.Now}] SMS2"));
            modem.OutputSmsList.Add(("+79161884955", $"[{DateTime.Now}] SMS3"));
            modem.StartCheckSmsTimer();
            while (modem.OutputSmsList.Count > 0)
                await Task.Delay(1000);
            Console.WriteLine("All sms in send");

            Console.WriteLine("Done!!! Press something to continue.\n");
            Console.ReadKey();
        }
    }
}
