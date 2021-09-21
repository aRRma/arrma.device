using System.Collections.Generic;
using Arrma.Device.Core.SerialPort;
using Arrma.Device.Interfaces.Logger;
using Arrma.Device.Interfaces.Protocol;

namespace Arrma.Device.Core.Transport.At
{
    public class AtTransport : DeviceSerialPort, IProtocol
    {
        public int SendingResponseAttempt
        {
            get => SendingResponseAttempt;
            set => SendingResponseAttempt = value < 1 ? 1 : value;
        }
        
        public AtTransport(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            SendingResponseAttempt = 1;
        }
        
        public bool SearchPort(IRequest request, out string name)
        {
            throw new System.NotImplementedException();
        }

        public IResponse SendCommand(IResponse response, int byteAnswer)
        {
            throw new System.NotImplementedException();
        }
    }
}
