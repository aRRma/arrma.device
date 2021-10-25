namespace Arrma.Device.Interfaces.Protocol
{
    /// <summary>
    /// Интерфейс пакета запроса
    /// </summary>
    public interface IRequest<T>
    {
        /// <summary>
        /// Команда запроса
        /// </summary>
        public T Command { get; set; }
        /// <summary>
        /// Данные запроса
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Длина пакета запроса
        /// </summary>
        public int Length { get; }
    }
}
