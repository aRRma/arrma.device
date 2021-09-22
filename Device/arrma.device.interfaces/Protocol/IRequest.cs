using System;

namespace Arrma.Device.Interfaces.Protocol
{
    /// <summary>
    /// Интерфейс пакета запроса
    /// </summary>
    public interface IRequest<T>
    {
        /// <summary>
        /// Код команды запроса
        /// </summary>
        public T Command { get; set; }
        /// <summary>
        /// Обобщенный массив пакета запроса
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Длина запроса
        /// </summary>
        public int Length { get; set; }
    }
}
