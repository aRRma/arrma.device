using System.IO.Ports;

namespace arrma.wc.interfaces.SerialPort
{
    /// <summary>
    /// Интерфейс конфигурации последовательного COM порта
    /// </summary>
    public interface ISerialPortConfig
    {
        public string Name { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }
    }
}
