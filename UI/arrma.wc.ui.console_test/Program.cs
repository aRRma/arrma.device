using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using arrma.wc.ui.console_test.TestSerialPort;

namespace arrma.wc.ui.console_test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Console test arrma.wc.device.core!!!");
            SerialPort01 serialPort = new SerialPort01(new PortConfig()
            {
                Name = "COM5",
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 100,
                WriteTimeout = 100
            });


            //serialPort.ReadByEvent();
            using CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            try
            {
                await serialPort.ReadStreamAsync(60, token).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            Console.WriteLine("Abort?");
            Console.ReadKey();
            cts.Cancel();
            Console.WriteLine("ERRRRR");

            //while (true)
            //{
            //    Console.WriteLine("LOL");
            //    Thread.Sleep(1000);
            //    //serialPort.ReadBytesToRead(50, 10);
            //}

            Console.ReadKey();
        }
    }
}
