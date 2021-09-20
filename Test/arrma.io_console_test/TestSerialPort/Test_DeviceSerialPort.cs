using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arrma.device.core;

namespace arrma.wc.ui.console_test
{
    class Test_DeviceSerialPort : DeviceSerialPort
    {
        public Test_DeviceSerialPort(SerialPortConfig config) : base(config)
        {

        }
    }
}
