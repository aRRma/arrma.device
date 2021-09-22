using System;
using System.Net.Http.Headers;
using Arrma.Device.Interfaces.Protocol;

namespace Arrma.Device.Core.Transport
{
    /// <summary>
    /// Структура пакета ответа AT протокола
    /// </summary>
    public struct AtResponse : IResponse<string>
    {
        public string Data { get; init; }
        public int Length { get; init; }
        public bool Valid { get; init; }

        public AtResponse(string data, bool valid)
        {
            Data = data;
            Length = Data.Length;
            Valid = valid;
        }

        public override string ToString() => $"{Data} valid is {Valid}";
    }
}
