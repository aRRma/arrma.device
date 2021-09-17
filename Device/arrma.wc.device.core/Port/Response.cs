﻿using System;
using arrma.wc.interfaces.SerialPort;

namespace arrma.wc.device.core.Port
{
    /// <summary>
    /// Структура пакета ответа
    /// </summary>
    public struct Response : IResponse
    {
        public byte[] Data { get; set; }
        public int Length { get; set; }
        public bool Valid { get; set; }

        public Response(int length)
        {
            Data = new byte[length];
            Length = length;
            Valid = false;
        }

        public override string ToString() => $"{BitConverter.ToString(Data).Replace("-", " ")} valid is {Valid}";
    }
}
