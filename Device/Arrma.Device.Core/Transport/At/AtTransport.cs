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
        /// <summary>
        /// Количество попыток отправки команд устройству
        /// </summary>
        public int SendingResponseAttempt { get; set; } = 1;

        public AtTransport(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            SendingResponseAttempt = 1;
        }

        /// <summary>
        /// Ищет COM порт к которому подключено устройство
        /// </summary>
        /// <param name="request">Пакет команды для запроса</param>
        /// <param name="byteAnswer">Количество байт полного ответа на команду запроса</param>
        /// <returns></returns>
        public bool SearchPort(IRequest<string> request, int byteAnswer = 0)
        {
            foreach (var item in AvailableComPorts)
            {
                try
                {
                    if (!Connect(item.Key)) continue;
                    if (SendCommand(request, byteAnswer).Valid)
                    {
                        AvailableComPorts[PortName] = true;
                        return true;
                    }
                }
                catch (Exception e)
                {
                    _logger?.Error($"At modem com port search error", LogSource.SERIAL_PORT, e);
                    continue;
                }
                finally
                {
                    var lastPort = PortName;
                    if (Disconnect())
                        AvailableComPorts[lastPort] = false;
                }
            }
            _logger?.Error("At modem com port not found", LogSource.SERIAL_PORT);
            return false;
        }
        /// <summary>
        /// Формирует полностью команду и отправляет ее устройству (если требуется, то читает ответ). Если byteAnswer не задан, то ответ не ожидается.
        /// </summary>
        /// <param name="request">Пакет команды</param>
        /// <param name="byteAnswer">Количество байт полного ответа на команду</param>
        /// <returns></returns>
        public IResponse<string> SendCommand(IRequest<string> request, int byteAnswer = 0)
        {
            // формируем команду
            List<char> tempReq = new List<char>();
            foreach (var item in request.Command)
                tempReq.Add(item);
            if (request.Data.Length > 0)
                for (int i = 0; i < request.Data.Length; i++)
                    tempReq.Add(request.Data[i]);
            tempReq.Add('\r');

            Debug.WriteLine(new string('=', 100));
            Debug.WriteLine($"At command {request.Command.ToString()}: {string.Join("", request)}\t{DateTime.Now}.{DateTime.Now.Millisecond}");

            // если порт не открыт выходим
            if (!_port.IsOpen) return new AtResponse("", false);

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
                    if (string.Join("", tempResp).Contains(AtModemAnswer.OK.ToString())) enable = false;
                }
                catch (TimeoutException e)
                {
                    _logger?.Error("Timeout waiting AT modem answer", LogSource.SERIAL_PORT, e);
                    break;
                }
                catch (Exception e)
                {
                    break;
                }
            }

            Debug.WriteLine($"AT modem answer: {string.Join("", tempResp).Replace("\r\n", " ")}\t{DateTime.Now}.{DateTime.Now.Millisecond}");
            Debug.WriteLine(new string('=', 100) + "\n");

            // проверяем принятую пачку
            if (string.Join("", tempResp).Contains(AtModemAnswer.OK.ToString()))
                return new AtResponse(string.Join("", tempResp), true);
            if (string.Join("", tempResp).Contains(AtModemAnswer.ERROR.ToString()))
                return new AtResponse("", false);
            
            _logger?.Error($"Broken AT modem answer: {string.Join("", tempResp)}", LogSource.SERIAL_PORT);

            return new AtResponse("", false);
        }
    }
}
