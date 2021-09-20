using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arrma.device.core;

namespace arrma.wc.ui.console_test
{
    class TestDevicePotocol : DeviceSerialPort
    {
        public TestDevicePotocol(SerialPortConfig config) : base(config)
        {

        }
    }
}
