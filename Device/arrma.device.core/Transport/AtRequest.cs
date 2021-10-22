using System;
using System.Linq;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Protocol;

namespace Arrma.Device.Core.Transport
{
    /// <summary>
    /// Структура пакета запроса AT протокола
    /// </summary>
    public struct AtRequest : IRequest<string>
    {
        public string Command { get; set; }
        public string Data { get; set; }
        public int Length { get; set; }

        public AtRequest(string command, string data)
        {
            Command = command;
            Data = data;
            Length = data.Length;
        }

        public override string ToString() => $"{Command}{Data}";
    }
}
