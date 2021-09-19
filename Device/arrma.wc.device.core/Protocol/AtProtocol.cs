using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arrma.wc.device.core.DevicePort;
using arrma.wc.interfaces.Logger;

namespace arrma.wc.device.core.Protocol
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
