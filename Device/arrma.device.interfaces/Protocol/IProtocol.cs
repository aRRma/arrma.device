using System.Runtime.InteropServices.ComTypes;

namespace Arrma.Device.Interfaces.Protocol
{
    /// <summary>
    /// Интерфейс протокола
    /// </summary>
    public interface IProtocol<T>
    {
        public bool SearchPort(IRequest<T> request, int byteAnswer);
        public IResponse<T> SendCommand(IRequest<T> request, int timeout, int byteAnswer);
    }
}
