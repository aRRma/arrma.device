using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arrma.wc.device.core.DevicePort;

namespace arrma.wc.ui.console_test.TestSerialPort
{
    class Test_DeviceSerialPort : DeviceSerialPort
    {
        public Test_DeviceSerialPort(SerialPortConfig config) : base(config)
        {

        }
    }
}
