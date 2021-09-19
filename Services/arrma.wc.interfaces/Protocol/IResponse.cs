namespace arrma.wc.interfaces.Protocol
{
    /// <summary>
    /// Интерфейс пакета ответа
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Пакет ответа
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// Длина пакета
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Прошел ли пакет проверку CRC
        /// </summary>
        public bool Valid { get; set; }
    }
}
