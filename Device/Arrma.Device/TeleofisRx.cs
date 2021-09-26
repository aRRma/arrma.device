using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arrma.Device.Basic.Protocol.At;
using Arrma.Device.Core.SerialPort;
using Arrma.Device.Core.Transport;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Device;
using Arrma.Device.Interfaces.Logger;
using QosInfo = Arrma.Device.Interfaces.Device.Base.QosInfo;

namespace Arrma.Device
{
    public class TeleofisRx : AtProtocol, ITeleofisRx
    {
        public string Name { get; set; }
        public byte Address { get; set; }
        public bool InitDone { get; set; }
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public string HardwareVersion { get; set; }
        public InterfaceType Interface { get; set; }
        public ProtocolType Protocol { get; set; }
        public NetworkRegType NetworkRegistration { get; set; }
        public QosInfo QoSInfo { get; set; }
        public string SimSerialId { get; set; }
        public SimOperatorType SimOperator { get; set; }
        public double Balance { get; set; }
        public string ManufIdent { get; set; }
        public string ModelIdent { get; set; }
        public string ModemIMEI { get; set; }

        public TeleofisRx(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            Address = 1;
            Interface = InterfaceType.COM;
            Protocol = ProtocolType.AT;
        }

        public bool SearchPort()
        {
            return base.SearchPort(new AtRequest(Commands[AtCommand.AT_], ""), 6);
        }
    }
}
