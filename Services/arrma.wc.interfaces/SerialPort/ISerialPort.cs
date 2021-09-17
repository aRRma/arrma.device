namespace arrma.wc.interfaces.SerialPort
{
    /// <summary>
    /// Интерфейс последовательного COM порта
    /// </summary>
    public interface ISerialPort
    {
        public string PortName { get; }
        public bool PortIsOpen { get; }
        public string[] GetPortNames();
        public bool Connect(IRequest request, IResponse response);
        public bool Disconnect();
        public bool Reconnect();
    }
}
