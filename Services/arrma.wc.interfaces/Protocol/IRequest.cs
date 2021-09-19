namespace arrma.wc.interfaces.Protocol
{
    /// <summary>
    /// Интерфейс пакета запроса
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Код команды запроса
        /// </summary>
        public byte Command { get; set; }
        /// <summary>
        /// Массив данных запроса
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// Длина запроса
        /// </summary>
        public int Length { get; set; }
    }
}
