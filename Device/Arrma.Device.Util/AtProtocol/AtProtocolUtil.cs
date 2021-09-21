using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrma.Device.Util.AtProtocol
{
    public static class AtProtocolUtil
    {
        /// <summary>
        /// Парсинг серийного номера SIM-карты
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CcidParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Парсинг серийного номера IMEI модема
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CgsnParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Парсинг кода IMSI (Mobile Subscriber Identity - типа оператора)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CimiParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Парсинг типа регистрации в сети
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CregParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Парсинг показаний качества сигнала QoS
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CsqParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Парсинг ответа на USSD запрос баланса 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CusdParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Парсинг идент. производителя модема
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GmiParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Парсинг идент. модели модуля модема
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GmmParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Парсинг идент. версии по модуля модема
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GmrParse(string data)
        {
            return "";
        }
        /// <summary>
        /// Преобразование строки из hex символов (2 байта) utf16 в строку
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UcsHexToString(string data)
        {
            return "";
        }
    }
}
