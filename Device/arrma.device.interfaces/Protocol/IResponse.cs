namespace Arrma.Device.Interfaces.Protocol
{
    /// <summary>
    /// Интерфейс пакета ответа
    /// </summary>
    public interface IResponse<T>
    {
        /// <summary>
        /// Обобщенный массив пакет ответа
        /// </summary>
        public T Data { get; }
        /// <summary>
        /// Длина пакета
        /// </summary>
        public int Length { get; }
        /// <summary>
        /// Прошел ли пакет валидацию (CRC или еще что-то)
        /// </summary>
        public bool Valid { get; }
    }
}
