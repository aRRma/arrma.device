namespace arrma.wc.interfaces.SerialPort
{
    /// <summary>
    /// Интерфейс последовательного COM порта
    /// </summary>
    public interface ISerialPort
    {
        public bool IsConnected { get; set; }
        public string PortName { get; set; }
        public bool Connect();
        public bool Disconnect();
        public bool Reconnect();
    }
}
