using System;
using arrma.wc.interfaces.Enum;

namespace arrma.wc.interfaces.Logger
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
        public void Write(string message, LogSource source = LogSource.OTHER);

        /// <summary>
        /// Записать сообщение в логи в режиме отладки
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        public void Debug(string message, LogSource source = LogSource.OTHER);

        /// <summary>
        /// Записать сообщение в логи
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        public void Information(string message, LogSource source = LogSource.OTHER);

        /// <summary>
        /// Записать предупреждение в логи 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        public void Warning(string message, LogSource source = LogSource.OTHER);

        /// <summary>
        /// Записать ошибку в логи
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public void Error(string message, LogSource source = LogSource.OTHER, Exception ex = null);

        /// <summary>
        /// Записать критическую ошибку в логи
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public void Fatal(string message, LogSource source = LogSource.OTHER, Exception ex = null);
    }
}
