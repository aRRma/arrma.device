namespace Arrma.Device.Interfaces.Enum
{
    /// <summary>
    /// Перечисление типов интерфейса
    /// </summary>
    public enum InterfaceType
    {
        /// <summary>
        /// Неизвестно
        /// </summary>
        NONE,
        /// <summary>
        /// USB порт
        /// </summary>
        USB,
        /// <summary>
        /// COM порт (RS232)
        /// </summary>
        COM,
        /// <summary>
        /// MultiDrop шина
        /// </summary>
        MDB
    }
    /// <summary>
    /// Перечисление типов протоколов
    /// </summary>
    public enum ProtocolType
    {
        /// <summary>
        /// Неизвестно
        /// </summary>
        NONE,
        /// <summary>
        /// Протокол Импульс
        /// </summary>
        IMPULS,
        /// <summary>
        /// Протокол ccTalk
        /// </summary>
        CCTALCK,
        /// <summary>
        /// Протокол ccNet
        /// </summary>
        CCNET,
        /// <summary>
        /// Протокол MDB
        /// </summary>
        MDB,
        /// <summary>
        /// Протокол AT
        /// </summary>
        AT,
        /// <summary>
        /// Протокол импульсный
        /// </summary>
        PULSE
    }
}
