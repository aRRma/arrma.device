namespace Arrma.Device.Interfaces.Protocol
{
    /// <summary>
    /// Обобщенный интерфейс протокола
    /// </summary>
    public interface IProtocol<T>
    {
        /// <summary>
        /// Найти COM порт к которому подключено устройство. 
        /// </summary>
        /// <param name="request">Команда для поиска устройства</param>
        /// <param name="byteAnswer">Длина ответа в байтах для команды поиска устройства</param>
        /// <returns>Если порт устройства найден, то вернется true.</returns>
        public bool SearchPort(IRequest<T> request, int byteAnswer);
        /// <summary>
        /// Формирует полностью команду и отправляет ее устройству (если требуется, то читает ответ). Если byteAnswer не задан, то ответ не ожидается.
        /// </summary>
        /// <param name="request">Пакет команды</param>
        /// <param name="timeout">Таймаут ответа на команду</param>
        /// <param name="byteAnswer">Количество байт полного ответа на команду (по умолчанию 6 байт для ответа OK)</param>
        /// <returns>Возвращает пакет ответа</returns>
        public IResponse<T> SendCommand(IRequest<T> request, int timeout, int byteAnswer);
    }
}
