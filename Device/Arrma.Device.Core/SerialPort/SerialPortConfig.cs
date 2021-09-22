using System.IO.Ports;

namespace Arrma.Device.Core.SerialPort
{
    public class SerialPortConfig
    {
        public string Name { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }

        /// <summary>
        /// Стандартные настройки 9600, 8бит, без четности, 1 стоповый, таймауты 100мс, порт не известен
        /// </summary>
        public SerialPortConfig()
        {
            Name = "COM1";
            BaudRate = 9600;
            DataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.One;
            ReadTimeout = 100;
            WriteTimeout = 100;
        }

        /// <summary>
        /// Стандартные настройки 9600, 8бит, без четности, 1 стоповый, таймауты 100мс, порт не известен
        /// </summary>
        public SerialPortConfig(SerialPortConfig config)
        {
            Name = config?.Name ?? "";
            BaudRate = config?.BaudRate ?? 9600;
            DataBits = config?.DataBits ?? 8;
            Parity = config?.Parity ?? Parity.None;
            StopBits = config?.StopBits ?? StopBits.One;
            ReadTimeout = config?.ReadTimeout ?? 100;
            WriteTimeout = config?.WriteTimeout ?? 100;
        }

        public override string ToString() => $"{(string.IsNullOrWhiteSpace(Name) ? "NONE" : Name)},{BaudRate},{DataBits},{Parity},{StopBits},{ReadTimeout},{WriteTimeout}";
    }
}