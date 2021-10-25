using Arrma.Device.Core.SerialPort;
using Arrma.Device.Core.Transport;
using Arrma.Device.Core.Transport.At;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Device.Base;
using Arrma.Device.Interfaces.Logger;
using Arrma.Device.Util.AtProtocol;
using System;
using System.Collections.Generic;

namespace Arrma.Device.Basic.Protocol.At
{
    public class AtProtocol : AtTransport
    {
        private Dictionary<AtCommand, string> _commands;
        private Dictionary<AtCommandEnd, string> _commandsEnd;

        /// <summary>
        /// Словарь команд AT протокола
        /// </summary>
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

        #region Базовая настройка модема
        /// <summary>
        /// Пинг модема. Команда AT.
        /// </summary>
        /// <returns></returns>
        public bool PingModem()
        {
            try
            {
                return SendCommand(new AtRequest(_commands[AtCommand.AT_], ""), AtTransport.ShortTimeout).Valid;
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"PingModem\"\n{e.Message}", this.ToString(), e);
                return false;
            }
        }
        /// <summary>
        /// Отключить эхо. Команда ATE0.
        /// </summary>
        /// <returns></returns>
        public bool EchoDisable()
        {
            try
            {
                return SendCommand(new AtRequest(_commands[AtCommand.AT_E], "0"), AtTransport.ShortTimeout).Valid;
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"EchoDisable\"\n{e.Message}", this.ToString(), e);
                return false;
            }
        }
        /// <summary>
        /// Отключение автоответа на звонок (после первого гудка). Команда ATS0=0.
        /// </summary>
        /// <returns></returns>
        public bool AutoAnswerDisable()
        {
            try
            {
                return SendCommand(new AtRequest(_commands[AtCommand.AT_S0], "=0"), AtTransport.ShortTimeout).Valid;
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"AutoAnswerDisable\"\n{e.Message}", this.ToString(), e);
                return false;
            }
        }
        /// <summary>
        /// Включить АОН. Команда AT+CLIP=1.
        /// </summary>
        /// <returns></returns>
        public bool AutoNumberDetectionEnable()
        {
            try
            {
                return SendCommand(new AtRequest(_commands[AtCommand.AT_CLIP], "=1"), AtTransport.ShortTimeout).Valid;
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"AutoNumberDetectionEnable\"\n{e.Message}", this.ToString(), e);
                return false;
            }
        }
        /// <summary>
        /// Перейти в текстовый режим. Команда AT+CMGF=1.
        /// </summary>
        /// <returns></returns>
        public bool SmsTextModeEnable()
        {
            try
            {
                return SendCommand(new AtRequest(_commands[AtCommand.AT_CMGF], "=1"), AtTransport.ShortTimeout).Valid;
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"SmsTextModeEnable\"\n{e.Message}", this.ToString(), e);
                return false;
            }
        }
        #endregion

        #region Информация об модеме
        /// <summary>
        /// Запросить идентификатор производителя модема. Команда AT+GMI
        /// </summary>
        /// <returns>Например строка "Telit"</returns>
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
                _logger?.Error($"Error at method \"GetManufactureId\"\n{e.Message}", this.ToString(), e);
                return "";
            }
        }
        /// <summary>
        /// Запросить идентификатор модели модуля модема. Команда AT+GMM
        /// </summary>
        /// <returns>Например строка "GL865-DUAL-V3.1"</returns>
        public string GetModelId()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_GMM], ""), AtTransport.ShortTimeout, 50);
                if (!response.Valid) return "";
                return response.Data.Replace("\r\n", "").Replace("OK", "");
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"GetModelId\"\n{e.Message}", this.ToString(), e);
                return "";
            }
        }
        /// <summary>
        /// Запросить версию ревизии ПО модуля модема. Команда AT+GMR
        /// </summary>
        /// <returns>Например строка "16.01.173"</returns>
        public string GetSoftVersion()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_GMR], ""), AtTransport.ShortTimeout, 50);
                if (!response.Valid) return "";
                return response.Data.Replace("\r\n", "").Replace("OK", "");
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"GetSoftVersion\"\n{e.Message}", this.ToString(), e);
                return "";
            }
        }
        /// <summary>
        /// Запросить IMEI (серийный номер как на корпусе модема). Команда AT#CGSN
        /// </summary>
        /// <returns>Например строка "353656102026806"</returns>
        public string GetModemSerialNumber()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CGSN], ""), AtTransport.ShortTimeout, 50);
                if (!response.Valid) return "";
                return response.Data.Replace("\r\n", "").Replace("OK", "");
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"GetModemSerialNumber\"\n{e.Message}", this.ToString(), e);
                return "";
            }
        }
        /// <summary>
        /// Запросить серийный номер SIM-карты. Команда AT+CCID
        /// </summary>
        /// <returns>Например строка "8970199200456530032"</returns>
        public string GetSimSerialNumber()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CCID], ""), AtTransport.ShortTimeout, 50);
                if (!response.Valid) return "";
                return response.Data.Replace("\r\n", "").Replace("OK", "").Replace("+CCID: ", "");
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"GetSimSerialNumber\"\n{e.Message}", this.ToString(), e);
                return "";
            }
        }
        /// <summary>
        /// Запросить название оператора связи. Команда AT+COPS?
        /// </summary>
        /// <returns>Имя оператора связи в виде строки</returns>
        public string GetOperatorName()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_COPS], "?"), AtTransport.ShortTimeout, 50);
                if (!response.Valid) return "";
                return response.Data[(response.Data.IndexOf('"') + 1)..response.Data.LastIndexOf('"')];
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"GetOperatorInfo\"\n{e.Message}", this.ToString(), e);
                return "";
            }
        }
        /// <summary>
        /// Запросить IMSI (Mobile Subscriber Identity) тип оператора. Команда AT+CIMI
        /// </summary>
        /// <returns></returns>
        public SimOperator GetOperatorType()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CIMI], ""), AtTransport.ShortTimeout, 50);
                if (!response.Valid) return new SimOperator();
                var mcc = int.Parse(response.Data.Replace("\r\n", "").Replace("OK", "")[0..3]);
                var imsi = int.Parse(response.Data.Replace("\r\n", "").Replace("OK", "")[3..5]);
                return AtProtocolUtil.CimiParse(mcc, imsi);
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"GetOperatorType\"\n{e.Message}", this.ToString(), e);
                return new SimOperator();
            }
        }
        #endregion

        #region Управление модемом
        /// <summary>
        /// Перезагрузка модуля модема. Команда AT#REBOOT
        /// </summary>
        /// <returns></returns>
        public bool ModemReboot()
        {
            try
            {
                return SendCommand(new AtRequest(_commands[AtCommand.AT_REBOOT], ""), AtTransport.ShortTimeout, 0).Valid;
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"ModemReboot\"\n{e.Message}", this.ToString(), e);
                return false;
            }
        }
        /// <summary>
        /// Проверить тип регистрации в сети. Команда AT+CREG?
        /// </summary>
        /// <returns></returns>
        public NetworkRegType CheckNetworkRegType()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CREG], "?"), AtTransport.ShortTimeout, 20);
                if (!response.Valid) return NetworkRegType.UNKNOWN;
                return AtProtocolUtil.CregParse(response.Data.Substring(response.Data.IndexOf(",") + 1, 1));
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"CheckNetworkRegType\"\n{e.Message}", this.ToString(), e);
                return NetworkRegType.UNKNOWN;
            }
        }
        /// <summary>
        /// Проверит качество связи в сети. Команда AT+CSQ
        /// </summary>
        /// <returns></returns>
        public QosInfo CheckNetworkQoS()
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CSQ], ""), AtTransport.ShortTimeout, 21);
                if (!response.Valid) return new QosInfo();
                int rssi = int.Parse(response.Data[response.Data.IndexOf(' ')..response.Data.IndexOf(',')]);
                var temp = response.Data.Substring(response.Data.IndexOf(',') + 1);
                int ber = int.Parse(temp.Remove(temp.IndexOf('\r')));
                return AtProtocolUtil.CsqParse(rssi, ber);
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"CheckNetworkQoS\"\n{e.Message}", this.ToString(), e);
                return new QosInfo();
            }
        }
        /// <summary>
        /// Запросить баланс для конкретного оператора в PDU режиме. Команда AT+CUSD
        /// </summary>
        /// <param name="operatorType">Тип оператора из перечисления</param>
        /// <returns>Баланс в виде строки</returns>
        public string GetSimBalance(SimOperatorType operatorType)
        {
            var data = operatorType switch
            {
                SimOperatorType.MTS or SimOperatorType.MEGAFON => "=1,\"#100#\"",
                SimOperatorType.BEELINE => "=1,\"#102#\"",
                _ => ""
            };

            try
            {
                if (string.IsNullOrWhiteSpace(data)) return "";
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CUSD], data), AtTransport.LongTimeout, 50);
                if (!response.Valid) return "";
                return AtProtocolUtil.CusdParse(response.Data);
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"GetSimBalance\"\n{e.Message}", this.ToString(), e);
                return "";
            }
        }
        /// <summary>
        /// Заапросить баланс через USSD в PDU режиме. Команда AT+CUSD
        /// </summary>
        /// <param name="ussd">Например: #100# для МТС и Мегафон, #102# для Билайн</param>
        /// <returns>Баланс в виде строки</returns>
        public string GetSimBalance(string ussd)
        {
            try
            {
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CUSD], "=1,\"" + ussd + "\""), AtTransport.LongTimeout, 50);
                if (!response.Valid) return "";
                return AtProtocolUtil.CusdParse(response.Data);
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"GetSimBalance\"\n{e.Message}", this.ToString(), e);
                return "";
            }
        }
        #endregion

        #region Работа с смс
        //public string GetSmsReposStatus()
        //{
        //    return "";
        //}
        //public string GetAllSms()
        //{
        //    return "";
        //}
        //public string GetSingleSms(int number)
        //{
        //    return "";
        //}
        //public string DeleteAllSms()
        //{
        //    return "";
        //}
        /// <summary>
        /// Отправить смс на номер в PDU режиме (только латиница). Команда AT+CMGS 
        /// </summary>
        /// <param name="data">Кортеж строк: номер телефона, текст смс</param>
        /// <returns></returns>
        public bool SendSmsOnNumber((string number, string text) data)
        {
            try
            {
                // TODO проверка номера регуляркой
                var response = SendCommand(new AtRequest(_commands[AtCommand.AT_CMGS], "=" + data.number), AtTransport.LongTimeout);
                if (!response.Data.Contains(">"))
                    return false;
                if (SendCommand(new AtRequest(_commands[AtCommand.ENTER_SMS_TEXT], data.text + _commandsEnd[AtCommandEnd.SUB]), AtTransport.LongTimeout).Valid)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                _logger?.Error($"Error at method \"SendSmsOnNumber\"\n{e.Message}", this.ToString(), e);
                return false;
            }
        }
        #endregion

        #region Работа со звонками
        //public string RejectIncomingCall()
        //{
        //    return "";
        //}
        #endregion
    }
}