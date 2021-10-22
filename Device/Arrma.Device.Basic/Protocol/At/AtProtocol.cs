using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Arrma.Device.Core.SerialPort;
using Arrma.Device.Core.Transport;
using Arrma.Device.Core.Transport.At;
using Arrma.Device.Interfaces.Logger;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Device.Base;
using Arrma.Device.Util.AtProtocol;

namespace Arrma.Device.Basic.Protocol.At
{
    public class AtProtocol : AtTransport
    {
        private Dictionary<AtCommand, string> _commands;
        private Dictionary<AtCommandEnd, string> _commandsEnd;

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
                { AtCommand.AT_CUSD, "AT+CUSD" },
                { AtCommand.AT_REBOOT, "AT#REBOOT" },
                { AtCommand.AT_CPMS, "AT+CPMS?" },
                { AtCommand.AT_CMGL, "AT+CMGL=\"ALL\"" },
                { AtCommand.AT_CMGR, "AT+CMGR" },
                { AtCommand.AT_CMGD, "AT+CMGD" },
                { AtCommand.AT_CMGS, "AT+CMGS" },
                { AtCommand.ENTER_SMS_TEXT, "ENTER_SMS" },
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
        }

        // методы для базовой настройки модема
        /// <summary>
        /// Пинг модема. Команда AT.
        /// </summary>
        /// <returns></returns>
        public bool PingModem()
        {
            return SendCommand(new AtRequest(_commands[AtCommand.AT_], ""), AtTransport.ShortTimeout).Valid;
        }
        /// <summary>
        /// Отключить эхо. Команда ATE0.
        /// </summary>
        /// <returns></returns>
        public bool EchoDisable()
        {
            return SendCommand(new AtRequest(_commands[AtCommand.AT_E], "0"), AtTransport.ShortTimeout).Valid;
        }
        /// <summary>
        /// Отключение автоответа на звонок (после первого гудка). Команда ATS0=0.
        /// </summary>
        /// <returns></returns>
        public bool AutoAnswerDisable()
        {
            return SendCommand(new AtRequest(_commands[AtCommand.AT_S0], "=0"), AtTransport.ShortTimeout).Valid;
        }
        /// <summary>
        /// Включить АОН. Команда AT+CLIP=1.
        /// </summary>
        /// <returns></returns>
        public bool AutoNumberDetectionEnable()
        {
            return SendCommand(new AtRequest(_commands[AtCommand.AT_CLIP], "=1"), AtTransport.ShortTimeout).Valid;
        }
        /// <summary>
        /// Перейти в текстовый режим. Команда AT+CMGF=1.
        /// </summary>
        /// <returns></returns>
        public bool SmsTextModeEnable()
        {
            return SendCommand(new AtRequest(_commands[AtCommand.AT_CMGF], "=1"), AtTransport.ShortTimeout).Valid;
        }
        /// <summary>
        /// Проверить тип регистрации в сети. Команда AT+CREG?
        /// </summary>
        /// <returns></returns>
        public NetworkRegType CheckNetworkRegType()
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CREG], "?"), AtTransport.ShortTimeout, 20);
            if (!response.Valid) return NetworkRegType.UNKNOWN;
            return AtProtocolUtil.CregParse(response.Data.Substring(response.Data.IndexOf(",") + 1, 1));
        }
        /// <summary>
        /// Проверит качество связи в сети. Команда AT+CSQ
        /// </summary>
        /// <returns></returns>
        public QosInfo CheckNetworkQoS()
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CSQ], ""), AtTransport.ShortTimeout, 21);
            if (!response.Valid) return new QosInfo();

            int rssi = int.Parse(response.Data[response.Data.IndexOf(' ')..response.Data.IndexOf(',')]);
            var temp = response.Data.Substring(response.Data.IndexOf(',') + 1);
            int ber = int.Parse(temp.Remove(temp.IndexOf('\r')));

            return AtProtocolUtil.CsqParse(rssi, ber);
        }

        // системная информация
        public string GetManufactureId()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_GMI], ""), AtTransport.ShortTimeout, 50);
                if (!response.Valid) return "";
                return response.Data.Replace("\r\n", "").Replace("OK", "");
            }
            catch (Exception e)
            {
                _logger?.Error("Error at method \"GetManufactureId\"", this.ToString());
                throw;
            }
        }
        public string GetModelId()
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_GMM], ""), AtTransport.ShortTimeout, 50);
            if (!response.Valid) return "";
            return response.Data.Replace("\r\n", "").Replace("OK", "");
        }
        public string GetSoftVersion()
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_GMR], ""), AtTransport.ShortTimeout, 50);
            if (!response.Valid) return "";
            return response.Data.Replace("\r\n", "").Replace("OK", "");
        }
        public string GetModemSerialNumber()
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CGSN], ""), AtTransport.ShortTimeout, 50);
            if (!response.Valid) return "";
            return response.Data.Replace("\r\n", "").Replace("OK", "");
        }
        public string GetSimSerialNumber()
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CCID], ""), AtTransport.ShortTimeout, 50);
            if (!response.Valid) return "";
            return response.Data.Replace("\r\n", "").Replace("OK", "").Replace("+CCID: ", "");
        }
        public string GetOperatorInfo()
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_COPS], "?"), AtTransport.ShortTimeout, 50);
            if (!response.Valid) return "";
            return response.Data[(response.Data.IndexOf('"') + 1)..response.Data.LastIndexOf('"')];
        }
        public SimOperator GetOperatorType()
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CIMI], ""), AtTransport.ShortTimeout, 50);
            if (!response.Valid) return new SimOperator();

            // Mobile Country Code — мобильный код страны
            var mcc = int.Parse(response.Data.Replace("\r\n", "").Replace("OK", "")[0..3]);
            // International Mobile Subscriber Identity (IMSI) — международный идентификатор мобильного абонента 
            var imsi = int.Parse(response.Data.Replace("\r\n", "").Replace("OK", "")[3..5]);

            return AtProtocolUtil.CimiParse(mcc, imsi);
        }

        // баланс
        public string GetSimBalance(SimOperatorType operatorType)
        {
            var data = operatorType switch
            {
                SimOperatorType.MTS or SimOperatorType.MEGAFON => "=1,\"#100#\"",
                SimOperatorType.BEELINE => "=1,\"#102#\"",
                _ => ""
            };

            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CUSD], data), AtTransport.LongTimeout, 50);
            if (!response.Valid) return "";

            return AtProtocolUtil.CusdParse(response.Data);
        }

        // перезагрузка
        public bool ModemReboot()
        {
            return SendCommand(new AtRequest(_commands[AtCommand.AT_REBOOT], ""), AtTransport.ShortTimeout, 0).Valid;
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
        public bool SendSmsOnNumber((string number, string text) sms)
        {
            var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CMGS], "=" + sms.number), AtTransport.LongTimeout);

            if (!response.Data.Contains(">"))
                return false;

            if (SendCommand(new AtRequest(_commands[AtCommand.ENTER_SMS_TEXT], sms.text + _commandsEnd[AtCommandEnd.SUB]), AtTransport.LongTimeout).Valid)
                return true;

            return false;
        }

        // звонки
        public string RejectIncomingCall()
        {
            return "";
        }
    }
}