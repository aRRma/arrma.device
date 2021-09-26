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
        public int SendingResponseAttempt { get; set; } = 1;

        public AtTransport(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            SendingResponseAttempt = 1;
        }

        public bool SearchPort(IRequest<string> request, int byteAnswer = 0)
        {
            try
            {
                foreach (var item in ComPortsDictionary)
                {
                    if (!Connect(item.Key)) continue;
                    if (SendCommand(request, byteAnswer).Valid)
                    {
                        ComPortsDictionary[PortName] = true;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                Disconnect();
            }
            return false;
        }
        
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
            Debug.WriteLine($"At command {request.Command.ToString()}: {string.Join("", request)}");

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
                    _logger.Error("Timeout waiting AT modem answer", LogSource.SERIAL_PORT, e);
                    break;
                }
                catch (Exception e)
                {
                    break;
                }
            }

            Debug.WriteLine($"AT modem answer: {string.Join("", tempResp)}");
            Debug.WriteLine(new string('=', 100) + "\n");

            // проверяем принятую пачку
            if (string.Join("", tempResp).Contains(AtModemAnswer.OK.ToString()))
                return new AtResponse(string.Join("", tempResp), true);
            else
                _logger.Error($"Broken AT modem answer: {string.Join("", tempResp)}", LogSource.SERIAL_PORT);

            return new AtResponse("", false);
        }
    }
}
