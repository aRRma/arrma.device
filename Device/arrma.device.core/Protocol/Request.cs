﻿using System;
using Arrma.Device.Interfaces.Protocol;

namespace Arrma.Device.Core.Protocol
{
    /// <summary>
    /// Структура пакета запроса
    /// </summary>
    public struct Request : IRequest
    {
        public byte Command { get; set; }
        public byte[] Data { get; set; }
        public int Length { get; set; }

        public Request(byte command, byte[] data)
        {
            Command = command;
            Data = data;
            Length = data.Length;
        }

        public override string ToString() => $"{Command.ToString():X2} {BitConverter.ToString(Data).Replace("-", " ")}";
    }
}
