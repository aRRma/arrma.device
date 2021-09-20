using arrma.device.interfaces;

namespace arrma.device.core.Protocol
{
    class AtProtocol : DeviceSerialPort
    {
        public AtProtocol(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
        }

        public void Test()
        {
            
        }
    }
}
