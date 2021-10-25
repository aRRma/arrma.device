using Arrma.Device.Interfaces.Protocol;

namespace Arrma.Device.Core.Transport
{
    /// <summary>
    /// Структура пакета запроса AT протокола
    /// </summary>
    public struct AtRequest : IRequest<string>
    {
        /// <summary>
        /// АТ команда
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// Данные запроса
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// Длина запроса
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Передайте АТ команду и данные запроса
        /// </summary>
        /// <param name="command">АТ команда</param>
        /// <param name="data">Данные запроса</param>
        public AtRequest(string command, string data)
        {
            Command = command;
            Data = data;
            Length = command.Length + data.Length;
        }

        public override string ToString() => $"{Command}{Data}";
    }
}
