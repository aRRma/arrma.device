namespace arrma.device.interfaces
{
    /// <summary>
    /// Интерфейс протокола
    /// </summary>
    public interface IProtocol
    {
        public bool SearchPort(IRequest request, out string name);
        public IResponse SendCommand(IResponse response, int byteAnswer);
    }
}
