using System;

namespace Arrma.Device.Interfaces.Logger
{
    /// <summary>
    /// Интерфейс логгера
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Запуск логгера
        /// </summary>
        public void RunLogger();

        /// <summary>
        /// Записать любое сообщение в логи
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        public void Write(string message, string source);

        /// <summary>
        /// Записать сообщение в логи в режиме отладки
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        public void Debug(string message, string source);

        /// <summary>
        /// Записать сообщение в логи
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        public void Information(string message, string source);

        /// <summary>
        /// Записать предупреждение в логи 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        public void Warning(string message, string source);

        /// <summary>
        /// Записать ошибку в логи
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public void Error(string message, string source, Exception ex = null);

        /// <summary>
        /// Записать критическую ошибку в логи
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public void Fatal(string message, string source, Exception ex = null);
    }
}
