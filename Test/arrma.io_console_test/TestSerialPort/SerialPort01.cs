using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace arrma.wc.ui.console_test.TestSerialPort
{
    internal class SerialPort01
    {
        private static SerialPort _serialPort;
        public SerialPort01()
        {
            _serialPort = new SerialPort();
        }

        public SerialPort01(PortConfig portConfig)
        {
            _serialPort = new SerialPort();

            _serialPort.PortName = portConfig.Name;
            _serialPort.BaudRate = portConfig.BaudRate;
            _serialPort.DataBits = portConfig.DataBits;
            _serialPort.Parity = portConfig.Parity;
            _serialPort.StopBits = portConfig.StopBits;
            _serialPort.ReadTimeout = portConfig.ReadTimeout;
            _serialPort.WriteTimeout = portConfig.WriteTimeout;

            Console.WriteLine("Try open port " + portConfig.Name + "at " + DateTime.Now);

            while (!_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Open();
                    Thread.Sleep(100);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error open port " + _serialPort.PortName + ": " + e.Message);
                }
            }

            _serialPort.DiscardInBuffer();
            _serialPort.DiscardOutBuffer();
            Console.WriteLine("Port " + portConfig.Name + "is open at " + DateTime.Now);
        }

        public void ReadBytesToRead(int numBytes, int bytesInPack)
        {
            _serialPort.DiscardInBuffer();
            Console.WriteLine("START read bytes");

            while (_serialPort.BytesToRead < numBytes)
            {
                Console.WriteLine($"_serialPort.BytesToRead: {_serialPort.BytesToRead} {DateTime.Now}.{DateTime.Now.Millisecond}");
                Thread.Sleep(250);
            }

            int totalBytes = _serialPort.BytesToRead;
            byte[] buff = new byte[255];
            _serialPort.Read(buff, 0, _serialPort.BytesToRead);
            Console.WriteLine($"_serialPort.BytesToRead after read: {_serialPort.BytesToRead}");
            Console.WriteLine($"DATA:\n");
            for (int i = 0; i < totalBytes; i += bytesInPack)
            {
                Console.WriteLine($"Pack {i / bytesInPack}: {BitConverter.ToString(buff[i..(i + 9)]).Replace("-", " ")}\n");
            }
            Console.WriteLine("END read bytes");
            Console.ReadKey();
        }


        public void ReadByEvent()
        {
            _serialPort.DataReceived += OnDataReceivedHandler;
        }

        private void OnDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine($"Data Received: {((SerialPort)sender).BytesToRead} + {DateTime.Now}.{DateTime.Now.Millisecond}");

            if (((SerialPort)sender).BytesToRead >= 50)
            {
                int totalBytes = _serialPort.BytesToRead;
                byte[] buff = new byte[255];
                _serialPort.Read(buff, 0, _serialPort.BytesToRead);
                Console.WriteLine($"_serialPort.BytesToRead after read: {_serialPort.BytesToRead}");
                Console.WriteLine($"DATA:\n");
                for (int i = 0; i < totalBytes; i += 10)
                {
                    Console.WriteLine($"Pack {i / 10}: {BitConverter.ToString(buff[i..(i + 9)]).Replace("-", " ")}\n");
                }
                Console.WriteLine("END read bytes");
                Console.ReadKey();
            }
        }
        public async Task ReadStreamAsync(int count, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Start async read");
            byte[] buff = await ReadAsync(count, cancellationToken).ConfigureAwait(false);
            if (cancellationToken.IsCancellationRequested)
                return;
            Console.WriteLine($"DATA:\n");
            for (int i = 0; i < count; i += 10)
            {
                Console.WriteLine($"Pack {i / 10}: {BitConverter.ToString(buff[i..(i + 9)]).Replace("-", " ")}\n");
            }
            Console.WriteLine("END read bytes");
            Console.ReadKey();

        }

        private async Task<byte[]> ReadAsync(int count, CancellationToken cancellationToken)
        {
            var buffer = new byte[count];
            await ReadAsync(buffer, 0, count, cancellationToken).ConfigureAwait(false);
            if (cancellationToken.IsCancellationRequested)
                return new byte[0];
            return buffer;
        }

        private async Task ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var bytesToRead = count;
            var temp = new byte[count];

            while (bytesToRead > 0)
            {
                var readBytes = await _serialPort.BaseStream.ReadAsync(temp, 0, bytesToRead, cancellationToken).ConfigureAwait(false);
                if (cancellationToken.IsCancellationRequested)
                    return;
                Array.Copy(temp, 0, buffer, offset + count - bytesToRead, readBytes);
                bytesToRead -= readBytes;
            }
        }
    }


    internal class PortConfig
    {
        public string Name { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }
        public string EndLine { get; set; }
    }
}
