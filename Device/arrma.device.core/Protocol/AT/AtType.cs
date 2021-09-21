using System;

namespace Arrma.Device.Core.Protocol.At
{
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
    /// <summary>
    /// Таймауты задержек at протокола
    /// </summary>
    public static class AtResponseDelays
    {
        public static readonly TimeSpan Short = TimeSpan.FromMilliseconds(500);
        public static readonly TimeSpan Default = TimeSpan.FromSeconds(2);
        public static readonly TimeSpan WaitCall = TimeSpan.FromSeconds(5);
        public static readonly TimeSpan Long = TimeSpan.FromSeconds(10);
    }
}
