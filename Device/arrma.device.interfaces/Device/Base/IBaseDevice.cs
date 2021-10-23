using Arrma.Device.Enum;

namespace Arrma.Device.Interfaces.Device.Base
{
    /// <summary>
    /// Базовый интерфейс устройства
    /// </summary>
    public interface IBaseDevice
    {
        /// <summary>
        /// Название устройства
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес устройства в сети
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
    }
}
