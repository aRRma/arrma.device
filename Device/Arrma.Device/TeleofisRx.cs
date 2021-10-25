using Arrma.Device.Basic.Protocol.At;
using Arrma.Device.Core.SerialPort;
using Arrma.Device.Core.Transport;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Device;
using Arrma.Device.Interfaces.Device.Base;
using Arrma.Device.Interfaces.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Arrma.Device
{
    /// <summary>
    /// Модем Teleofis серии RX
    /// </summary>
    public class TeleofisRx : AtProtocol, ITeleofisRx
    {
        /// <summary>
        /// Название устройства
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес устройства в сети (не актуально)
        /// </summary>
        public byte Address { get; set; }
        /// <summary>
        /// Статус инициализации устройства
        /// </summary>
        public bool InitDone { get; set; }
        /// <summary>
        /// Серийный номер
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// Версия прошивки
        /// </summary>
        public string FirmwareVersion { get; set; }
        /// <summary>
        /// Тип интерфейса устройства
        /// </summary>
        public InterfaceType Interface { get; set; }
        /// <summary>
        /// Тип протокола устройства
        /// </summary>
        public ProtocolType Protocol { get; set; }
        /// <summary>
        /// Тип регистрации модема в сети
        /// </summary>
        public NetworkRegType NetworkRegistration { get; set; }
        /// <summary>
        /// Качество сигнала в сети
        /// </summary>
        public QosInfo QoSInfo { get; set; }
        /// <summary>
        /// Серийный номер SIM-карты
        /// </summary>
        public string SimSerialId { get; set; }
        /// <summary>
        /// Оператора мобильной связи
        /// </summary>
        public string SimOperatorName { get; set; }
        /// <summary>
        /// Типа оператора мобильной связи
        /// </summary>
        public SimOperator SimOperator { get; set; }
        /// <summary>
        /// Баланс на сим карте
        /// </summary>
        public string Balance { get; set; }
        /// <summary>
        /// Идентификатор производителя модема
        /// </summary>
        public string ManufactureId { get; set; }
        /// <summary>
        /// Идентификатор модели модуля модема
        /// </summary>
        public string ModelId { get; set; }

        /// <summary>
        /// Список смс на отправку
        /// </summary>
        public List<(string number, string text)> OutputSmsList { get; }

        private Timer _tmrScanSmsList;

        /// <summary>
        /// Передайте конфиг com порта и дополнительно логгер для создания объекта модема
        /// </summary>
        /// <param name="config">Конфигурация com порта</param>
        /// <param name="logger">Логгер</param>
        public TeleofisRx(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            Name = nameof(TeleofisRx);
            Address = 1;
            Interface = InterfaceType.COM;
            Protocol = ProtocolType.AT;

            OutputSmsList = new List<(string number, string text)>();
            _tmrScanSmsList = new Timer(TmrScanSmsListHandler);
        }

        /// <summary>
        /// Найти COM порт к которому подключено устройство. Название порта сохранится в соотв. поле объекта.
        /// </summary>
        /// <returns>Если порт устройства найден, то вернется true</returns>
        public bool SearchPort()
        {
            return base.SearchPort(new AtRequest(Commands[AtCommand.AT_], ""), 6);
        }
        /// <summary>
        /// Произвести базовую настройку модема: отключить эхо, автоответ на звонок, включить АОН, текстовый режим. 
        /// </summary>
        /// <returns>Вернет true если настройка прошла успешно</returns>
        public async Task<bool> BaseInitModemAsync()
        {
            if (!base.IsConnected) return false;
            ModemReboot();
            await Task.Delay(10000).ConfigureAwait(false);
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
        /// <summary>
        /// Заполнить базовую информацию о модеме.
        /// </summary>
        public bool FillModemBaseData()
        {
            try
            {
                ManufactureId = GetManufactureId();
                ModelId = GetModelId();
                FirmwareVersion = GetSoftVersion();
                SerialNumber = GetModemSerialNumber();
                SimSerialId = GetSimSerialNumber();
                SimOperatorName = GetOperatorName();
                SimOperator = GetOperatorType();
                Balance = GetSimBalance(SimOperator.Type);
                NetworkRegistration = CheckNetworkRegType();
                QoSInfo = CheckNetworkQoS();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// Запустить таймер сканирования списка смс на отправку
        /// </summary>
        /// <returns>true если таймер успешно запустился</returns>
        public bool StartCheckSmsTimer() => _tmrScanSmsList.Change(0, 100);
        /// <summary>
        /// Остановить таймер сканирования списка смс на отправку
        /// </summary>
        /// <returns>true если таймер успешно остановился</returns>
        public bool StopCheckSmsTimer() => _tmrScanSmsList.Change(Timeout.Infinite, Timeout.Infinite);

        private void TmrScanSmsListHandler(object? state)
        {
            if (!InitDone || NetworkRegistration != NetworkRegType.REGISTERED) return;
            
            try
            {
                StopCheckSmsTimer();

                if (OutputSmsList.Count > 0)
                {
                    if (SendSmsOnNumber((OutputSmsList[0].number, OutputSmsList[0].text)))
                        OutputSmsList.RemoveAt(0);
                    StartCheckSmsTimer();
                }
            }
            catch (Exception e)
            {
                StartCheckSmsTimer();
            }
        }
    }
}
