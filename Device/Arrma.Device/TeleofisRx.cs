using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Arrma.Device.Basic.Protocol.At;
using Arrma.Device.Core.SerialPort;
using Arrma.Device.Core.Transport;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Device;
using Arrma.Device.Interfaces.Device.Base;
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
        public InterfaceType Interface { get; set; }
        public ProtocolType Protocol { get; set; }
        public NetworkRegType NetworkRegistration { get; set; }
        public QosInfo QoSInfo { get; set; }
        public string SimSerialId { get; set; }
        public string SimOperatorName { get; set; }
        public SimOperator SimOperator { get; set; }
        public string Balance { get; set; }
        public string ManufactureId { get; set; }
        public string ModelId { get; set; }
        
        public TeleofisRx(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            Name = nameof(TeleofisRx);
            Address = 1;
            Interface = InterfaceType.COM;
            Protocol = ProtocolType.AT;
        }

        public bool SearchPort()
        {
            return base.SearchPort(new AtRequest(Commands[AtCommand.AT_], ""), 6);
        }

        public async Task<bool> BaseInitModemAsync()
        {
            if (!base.IsConnected) return false;
            ModemReboot();
            await Task.Delay(5000).ConfigureAwait(false);
            if (EchoDisable()
                && AutoAnswerDisable()
                && AutoNumberDetectionEnable()
                && SmsTextModeEnable())
            {
                InitDone = true;
                return true;
            }

            return false;
        }

        public void FillModemBaseData()
        {
            ManufactureId = GetManufactureId();
            ModelId = GetModelId();
            FirmwareVersion = GetSoftVersion();
            SerialNumber = GetModemSerialNumber();
            SimSerialId = GetSimSerialNumber();
            SimOperatorName = GetOperatorName();
            SimOperator = GetOperatorType();
            //Balance = GetSimBalance(SimOperator.Type);
            NetworkRegistration = CheckNetworkRegType();
            QoSInfo = CheckNetworkQoS();
        }
    }
}
