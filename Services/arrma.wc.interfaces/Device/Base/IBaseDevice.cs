using arrma.wc.interfaces.Enum;

namespace arrma.wc.interfaces.Device.Base
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
        /// Серийный номер
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// Версия прошивки
        /// </summary>
        public string FirmwareVersion { get; set; }
        /// <summary>
        /// Версия железа
        /// </summary>
        public string HardwareVersion { get; set; }
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
