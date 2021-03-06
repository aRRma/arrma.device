using System;

namespace Arrma.Device.Util.Crc
{
    public static class CrcUtil
    {
        /// <summary>
        /// CRC16 Modbus
        /// </summary>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static ushort GetCRC16Modbus(byte[] data, int size)
        {
            ushort crc = 0xFFFF;
            int i;
            int index = 0;
            while (size-- != 0)
            {
                crc ^= data[index++];
                for (i = 0; i < 8; i++)
                    crc = (ushort)((crc & 0x01) != 0 ? (crc >> 1) ^ 0xA001 : crc >> 1);
            }
            return crc;
        }
        /// <summary>
        /// CRC16 CCNet
        /// </summary>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static ushort GetCRC16CCNet(byte[] data, int size)
        {
            ushort CRC, i;
            byte j;
            CRC = 0;
            for (i = 0; i < size; i++)
            {
                CRC ^= data[i];
                for (j = 0; j < 8; j++)
                {
                    if ((CRC & 0x0001) != 0)
                    {
                        CRC >>= 1;
                        CRC ^= 0x08408;
                    }
                    else CRC >>= 1;
                }
            }
            return CRC;
        }
        /// <summary>
        /// CRC8 Impuls
        /// </summary>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte GetCRC8Impuls(byte[] data, int size)
        {
            byte CRC = data[0];
            for (int i = 1; i < size; i++)
                CRC ^= data[i];
            return CRC;
        }
    }
}
