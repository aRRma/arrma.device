using arrma.wc.interfaces.SerialPort;
using System;
using System.IO.Ports;
using arrma.wc.interfaces.Logger;

namespace arrma.wc.device.core.Port
{
    public class PortBase : ISerialPort
    {
        private readonly SerialPort _port;
        private readonly PortConfig _config;
        private string _portName;
        private ILogger _logger;
        private int _attempt = 5;

        public bool PortIsOpen => _port?.IsOpen ?? false;
        public string PortName => _port?.PortName;

        public PortBase(PortBase port)
        {
            _port = port._port;
            _config = port._config;
            _logger = port._logger;
        }

        public PortBase(ISerialPortConfig config, ILogger logger = null)
        {
            _port = new SerialPort();
            _config = (PortConfig)config;
            _logger = logger;
        }
        
        public string[] GetPortNames()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                Array.Sort(ports);
                return ports;
            }
            catch (Exception ex)
            {
                return new[] { "" };
            }
        }

        public bool Connect(IRequest request, IResponse response)
        {
            throw new NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool Reconnect()
        {
            throw new NotImplementedException();
        }
    }
}
