using Arrma.Device.Enum;

namespace Arrma.Device.Interfaces.Device.Base
{
    /// <summary>
    /// Базовый интерфейс типового модема
    /// </summary>
    public interface IBaseModem : IBaseDevice
    {
        /// <summary>
        /// Тип регистрации модема в сети
        /// </summary>
        public NetworkRegType NetworkRegistration { get; set; }
        /// <summary>
        /// Качество сигнала
        /// </summary>
        public QosInfo QoSInfo { get; set; }
        /// <summary>
        /// Серийный номер SIM-карты
        /// </summary>
        public string SimSerialId { get; set; }
        /// <summary>
        /// Типа оператора мобильной связи
        /// </summary>
        public SimOperatorType SimOperator { get; set; }
        /// <summary>
        /// Баланс на сим карте
        /// </summary>
        public double Balance { get; set; }
    }

    /// <summary>
    /// Структура описывающая качество связи модема в сети
    /// </summary>
    public struct QosInfo
    {
        /// <summary>
        /// Arbitrary strength unit (количество делений связи)
        /// </summary>
        public byte Asu { get; set; }
        /// <summary>
        /// Error percent (верхн. граница ошибки)	
        /// </summary>
        public double Error { get; set; }
    }
}
