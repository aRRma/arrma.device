using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arrma.wc.interfaces.SerialPort;

namespace arrma.wc.device.core.Port
{
    class PortConfig : ISerialPortConfig
    {
        public string Name { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }

        //public PortConfig(ISerialPortConfig config)
        //{
        //    Name = config.Name;
        //    BaudRate = config.BaudRate;
        //    DataBits = config.DataBits;
        //    Parity = config.Parity;
        //    StopBits = config.StopBits;
        //    ReadTimeout = config.ReadTimeout;
        //    WriteTimeout = config.WriteTimeout;
        //}
    }
}