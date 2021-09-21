using Arrma.Device.Interfaces.Device.Base;

namespace Arrma.Device.Interfaces.Device
{
    /// <summary>
    /// Интерфейс модема Teleofis серии RX
    /// </summary>
    public interface ITeleofisRx : IBaseModem
    {
        /// <summary>
        /// Идент. производителя модема
        /// </summary>
        public string ManufIdent { get; set; }
        /// <summary>
        /// Идент. модели модуля модема
        /// </summary>
        public string ModelIdent { get; set; }
        /// <summary>
        /// Серийный номер IMEI модема
        /// </summary>
        public string ModemIMEI { get; set; }
    }
}
