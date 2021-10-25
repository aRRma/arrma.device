using Arrma.Device.Interfaces.Protocol;

namespace Arrma.Device.Core.Transport
{
    /// <summary>
    /// Структура пакета ответа AT протокола
    /// </summary>
    public struct AtResponse : IResponse<string>
    {
        /// <summary>
        /// Данные пакет ответа
        /// </summary>
        public string Data { get; init; }
        /// <summary>
        /// Длина пакета ответа
        /// </summary>
        public int Length { get; init; }
        /// <summary>
        /// Прошел ли пакет валидацию
        /// </summary>
        public bool Valid { get; init; }

        /// <summary>
        /// Передайте данные ответа и статус проверки пакета
        /// </summary>
        /// <param name="data"></param>
        /// <param name="valid"></param>
        public AtResponse(string data, bool valid)
        {
            Data = data;
            Length = Data.Length;
            Valid = valid;
        }

        public override string ToString() => $"{Data} valid is {Valid}";
    }
}
