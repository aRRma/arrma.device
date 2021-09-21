using System;
using System.Collections.Generic;
using Arrma.Device.Core.SerialPort;
using Arrma.Device.Core.Transport.At;
using Arrma.Device.Interfaces.Logger;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Device.Base;

namespace Arrma.Device.Basic.Protocol.At
{
    public class AtProtocol : AtTransport
    {
        private Dictionary<AtCommand, string> _commands;
        private Dictionary<AtCommandEnd, string> _commandsEnd;
        private Dictionary<AtModemAnswer, string> _modemAnswer;

        public AtProtocol(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            _commands = new Dictionary<AtCommand, string>()
            {
                { AtCommand.AT_, "AT\r" },
                { AtCommand.AT_E0, "ATE0\r" },
                { AtCommand.AT_S0, "ATS0=0\r" },
                { AtCommand.AT_CLIP, "AT+CLIP=1\r" },
                { AtCommand.AT_CMGF, "AT+CMGF=1\r" },
                { AtCommand.AT_CREG, "AT+CREG?\r" },
                { AtCommand.AT_CSQ, "AT+CSQ\r" },
                { AtCommand.AT_GMI, "AT+GMI\r" },
                { AtCommand.AT_GMM, "AT+GMM\r" },
                { AtCommand.AT_GMR, "AT+GMR\r" },
                { AtCommand.AT_CGSN, "AT+CGSN\r" },
                { AtCommand.AT_CCID, "AT+CCID\r" },
                { AtCommand.AT_COPS, "AT+COPS?\r" },
                { AtCommand.AT_CIMI, "AT+CIMI\r" },
                { AtCommand.AT_CUSD, "AT+CUSD=1," },
                { AtCommand.AT_REBOOT, "AT#REBOOT\r" },
                { AtCommand.AT_CPMS, "AT+CPMS?\r" },
                { AtCommand.AT_CMGL, "AT+CMGL=\"ALL\"\r" },
                { AtCommand.AT_CMGR, "AT+CMGR=1\r" },
                { AtCommand.AT_CMGD, "AT+CMGD=1,4\r" },
                { AtCommand.AT_CMGS, "AT+CMGS=" },
                { AtCommand.AT_H0, "ATH$\r" }
            };
            _commandsEnd = new Dictionary<AtCommandEnd, string>()
            {
                { AtCommandEnd.CR, "\r" },
                { AtCommandEnd.SUB, "\x1A" },
                { AtCommandEnd.SPACE, "\x20" },
                { AtCommandEnd.LF, "\n" },
                { AtCommandEnd.CRLF, "\r\n"},
            };
            _modemAnswer = new Dictionary<AtModemAnswer, string>()
            {
                { AtModemAnswer.NONE, "" },
                { AtModemAnswer.OK, "\r\nOK\r\n" },
                { AtModemAnswer.ERROR, "\r\nERROR\r\n" },
                { AtModemAnswer.ENTER_SMS, "\r\n>" },
            };
        }

        // методы для базовой настройки модема
        public bool PingModem()
        {
            return false;
        }
        public bool EchoDisable()
        {
            return false;
        }
        public bool AutoAnswerDisable()
        {
            return false;
        }
        public bool AutoNumberDetectionEnable()
        {
            return false;
        }
        public bool SmsTextModeEnable()
        {
            return false;
        }
        public NetworkRegType CheckNetworkRegType()
        {
            return NetworkRegType.UNKNOWN;
        }
        public QosInfo CheckNetworkQoS()
        {
            return new QosInfo();
        }

        // системная информация
        public string GetManufactureId()
        {
            return "";
        }
        public string GetModelId()
        {
            return "";
        }
        public string GetSoftVersion()
        {
            return "";
        }
        public string GetModemSerialNumber()
        {
            return "";
        }
        public string GetSimSerialNumber()
        {
            return "";
        }
        public string GetOperatorInfo()
        {
            return "";
        }
        public string GetOperatorType()
        {
            return "";
        }

        // баланс
        public string GetSimBalance()
        {
            return "";
        }

        // перезагрузка
        public string ModemReboot()
        {
            return "";
        }

        // cмс
        public string GetSmsReposStatus()
        {
            return "";
        }
        public string GetAllSms()
        {
            return "";
        }
        public string GetSingleSms(int number)
        {
            return "";
        }
        public string DeleteAllSms()
        {
            return "";
        }
        public string SendSmsOnNumber()
        {
            return "";
        }

        // звонки
        public string RejectIncomingCall()
        {
            return "";
        }
    }

    /// <summary>
    /// Таймауты задержек at протокола
    /// </summary>
    public static class AtResponseDelays
    {
        public static readonly TimeSpan Short = TimeSpan.FromMilliseconds(500);
        public static readonly TimeSpan Default = TimeSpan.FromSeconds(2);
        public static readonly TimeSpan WaitCall = TimeSpan.FromSeconds(5);
        public static readonly TimeSpan Long = TimeSpan.FromSeconds(10);
    }
}