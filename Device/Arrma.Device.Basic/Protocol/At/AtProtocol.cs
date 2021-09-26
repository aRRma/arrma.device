﻿using System;
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

        public Dictionary<AtCommand, string> Commands => _commands;

        public AtProtocol(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            _commands = new Dictionary<AtCommand, string>()
            {
                { AtCommand.AT_, "AT" },
                { AtCommand.AT_E, "ATE" },
                { AtCommand.AT_S0, "ATS0" },
                { AtCommand.AT_CLIP, "AT+CLIP" },
                { AtCommand.AT_CMGF, "AT+CMGF" },
                { AtCommand.AT_CREG, "AT+CREG" },
                { AtCommand.AT_CSQ, "AT+CSQ" },
                { AtCommand.AT_GMI, "AT+GMI" },
                { AtCommand.AT_GMM, "AT+GMM" },
                { AtCommand.AT_GMR, "AT+GMR" },
                { AtCommand.AT_CGSN, "AT+CGSN" },
                { AtCommand.AT_CCID, "AT+CCID" },
                { AtCommand.AT_COPS, "AT+COPS" },
                { AtCommand.AT_CIMI, "AT+CIMI" },
                { AtCommand.AT_CUSD, "AT+CUSD," },
                { AtCommand.AT_REBOOT, "AT#REBOOT" },
                { AtCommand.AT_CPMS, "AT+CPMS?" },
                { AtCommand.AT_CMGL, "AT+CMGL=\"ALL\"" },
                { AtCommand.AT_CMGR, "AT+CMGR" },
                { AtCommand.AT_CMGD, "AT+CMGD" },
                { AtCommand.AT_CMGS, "AT+CMGS" },
                { AtCommand.AT_H, "ATH" }
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