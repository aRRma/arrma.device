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
        public string ManufactureId { get; set; }
        /// <summary>
        /// Идент. модели модуля модема
        /// </summary>
        public string ModelId { get; set; }
    }
}
