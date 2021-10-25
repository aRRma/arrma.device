using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using Arrma.Device.Core.SerialPort;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Logger;
using Arrma.Device.Interfaces.Protocol;

namespace Arrma.Device.Core.Transport.At
{
    public class AtTransport : DeviceSerialPort, IProtocol<string>
    {
        public const int ShortTimeout = 100;
        public const int DefaultTimeout = 2000;
        public const int WaitCallTimeout = 5000;
        public const int LongTimeout = 10000;

        /// <summary>
        /// Количество попыток отправки команд устройству
        /// </summary>
        public int SendingResponseAttempt { get; set; } = 1;

        private int _answerErrors;
        private int _timeoutErrors;

        public AtTransport(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            SendingResponseAttempt = 1;
        }

        /// <summary>
        /// Найти COM порт к которому подключено устройство. 
        /// </summary>
        /// <param name="request">Команда для поиска устройства</param>
        /// <param name="byteAnswer">Длина ответа в байтах для команды поиска устройства</param>
        /// <returns>Если порт устройства найден, то вернется true.</returns>
        public bool SearchPort(IRequest<string> request, int byteAnswer = 0)
        {
            var сomPorts = GetSortComPorts();

            foreach (var item in сomPorts)
            {
                try
                {
                    if (!Connect(item)) continue;
                    if (SendCommand(request, ShortTimeout).Valid)
                        return true;
                }
                catch (Exception e)
                {
                    _logger?.Error($"AT modem com port search error", this.ToString(), e);
                    continue;
                }
                finally
                {
                    var lastPort = PortName;
                    Disconnect();
                }
            }
            _logger?.Error("AT modem com port not found", this.ToString());
            return false;
        }
        /// <summary>
        /// Формирует полностью команду и отправляет ее устройству (если требуется, то читает ответ). Если byteAnswer не задан, то ответ не ожидается.
        /// </summary>
        /// <param name="request">Пакет команды</param>
        /// <param name="timeout">Таймаут ответа на команду</param>
        /// <param name="byteAnswer">Количество байт полного ответа на команду (по умолчанию 6 байт для ответа OK)</param>
        /// <returns>Возвращает пакет ответа</returns>
        public IResponse<string> SendCommand(IRequest<string> request, int timeout = DefaultTimeout, int byteAnswer = 6)
        {
            // если порт не открыт выходим
            if (!_port.IsOpen) return new AtResponse("", false);

            // задаем таймаут
            _port.ReadTimeout = _port.ReadTimeout != timeout ? timeout : _port.ReadTimeout;

            // формируем команду
            List<char> tempReq = new List<char>();
            if (!request.Command.Contains("ENTER_SMS"))
                foreach (var item in request.Command)
                    tempReq.Add(item);
            if (request.Data.Length > 0)
                for (int i = 0; i < request.Data.Length; i++)
                    tempReq.Add(request.Data[i]);
            tempReq.Add('\r');

            Debug.WriteLine(new string('=', 100));
            Debug.WriteLine($"[{_port.PortName}] [{DateTime.Now:T}]\tAT command:\t\"{string.Join("", request)}\"");

            // отправка команды
            _port.DiscardInBuffer();
            _port.DiscardOutBuffer();
            _port.Write(tempReq.ToArray(), 0, tempReq.Count);

            // выходим если не нужно ждать ответ
            if (byteAnswer <= 0)
            {
                Debug.WriteLine(new string('=', 100));
                return new AtResponse("", false);
            }

            // читаем ответ
            List<char> tempResp = new List<char>();
            bool enable = true;
            while (enable)
            {
                try
                {
                    tempResp.Add((char)_port.ReadByte());

                    // проверка для команды запроса баланса
                    if (request.Command.Contains("CUSD"))
                        if (tempResp.Count > 8 && new string(tempResp.ToArray()).EndsWith("\r\n"))
                            enable = false;

                    // проверяем OK на команду (игнорируем при запросе баланса)
                    if (!request.Command.Contains("CUSD") && string.Join("", tempResp).Contains("OK"))
                        enable = false;

                    // проверка символа приглашения на ввод смс
                    if (string.Join("", tempResp).Contains(">"))
                        if (request.Command.Contains("CMGS"))
                        {
                            enable = false;
                            tempResp.Add('O');
                            tempResp.Add('K');
                        }
                        else if (!request.Command.Contains("ENTER_SMS"))
                        {
                            _port.DiscardOutBuffer();
                            _port.Write(new string("\x1A"));
                        }
                }
                catch (TimeoutException e)
                {
                    _logger?.Error("Timeout waiting AT modem answer", this.ToString(), e);
                    _timeoutErrors++;
                    break;
                }
                catch (Exception e)
                {
                    _logger?.Error("Error waiting AT modem answer", this.ToString(), e);
                    break;
                }

                // завершаем ожидание байтов ответа
                if (!enable)
                {
                    _timeoutErrors = 0;
                    _answerErrors = 0;
                }
            }

            Debug.WriteLine($"[{_port.PortName}] [{DateTime.Now:T}]\tAT answer:\t\"{string.Join("", tempResp).Replace("\r\n", " ")}\"");
            Debug.WriteLine(new string('=', 100) + "\n");

            // проверяем принятую пачку
            if (string.Join("", tempResp).Contains("OK"))
                return new AtResponse(string.Join("", tempResp), true);
            if (string.Join("", tempResp).Contains("ERROR"))
                return new AtResponse("", false);

            _logger?.Error($"Broken AT modem answer: {string.Join("", tempResp)}", this.ToString());
            _answerErrors++;

            return new AtResponse("", false);
        }
    }
}
