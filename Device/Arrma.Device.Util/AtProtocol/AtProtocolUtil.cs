using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arrma.Device.Enum;
using Arrma.Device.Interfaces.Device.Base;

namespace Arrma.Device.Util.AtProtocol
{
    public static class AtProtocolUtil
    {
        /// <summary>
        /// Парсинг кода IMSI (Mobile Subscriber Identity - типа оператора)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SimOperator CimiParse(int mcc, int imsi)
        {
            SimOperator sim = new SimOperator();

            sim.Country = mcc switch
            {
                289 => "GE-AB Абхазия",
                505 => "AU Австралия",
                232 => "AT Австрия",
                400 => "AZ Азербайджан",
                276 => "AL Албания",
                603 => "DZ Алжир",
                332 => "VI Американские Виргинские острова",
                544 => "AS Американские Самоа",
                631 => "AO Ангола",
                213 => "AD Андорра",
                365 => "AI Антигуа",
                344 => "AG Антигуа и Барбуда",
                722 => "AR Аргентина",
                283 => "AM Армения",
                363 => "AW Аруба",
                412 => "AF Афганистан",
                364 => "BS Багамские Острова",
                470 => "BD Бангладеш",
                342 => "BB Барбадос",
                426 => "BH Бахрейн",
                257 => "BY Беларусь",
                702 => "BZ Белиз",
                206 => "BE Бельгия",
                616 => "BJ Бенин",
                350 => "BM Бермудские острова",
                284 => "BG Болгария",
                736 => "BO Боливия",
                218 => "BA Босния и Герцеговина",
                652 => "BW Ботсвана",
                724 => "BR Бразилия",
                348 => "VG Британские Виргинские острова",
                528 => "BN Бруней",
                613 => "BF Буркина-Фасо",
                642 => "BI Бурунди",
                402 => "BT Бутан",
                541 => "VU Вануату",
                225 => "VA Ватикан",
                235 => "GB Великобритания",
                234 => "GB Великобритания",
                734 => "VE Венесуэла",
                216 => "HU Венгрия",
                514 => "TL Восточный Тимор",
                452 => "VN  Вьетнам",
                628 => "GA Габон",
                372 => "HT Гаити",
                738 => "GY Гайана",
                607 => "GM Гамбия",
                620 => "GH Гана",
                340 => "GP Гваделупа",
                704 => "GT Гватемала",
                611 => "GN Гвинея",
                632 => "GW Гвинея-Бисау",
                262 => "DE  Германия",
                266 => "GI Гибралтар",
                708 => "HN Гондурас",
                454 => "HK Гонконг",
                352 => "GD Гренада",
                290 => "GL Гренландия",
                202 => "GR Греция",
                282 => "GE Грузия",
                535 => "GU Гуам",
                238 => "DK Дания",
                630 => "CD Демократическая Республика Конго",
                638 => "DJ Джибути",
                366 => "DM Доминика",
                370 => "DO Доминиканская Республика",
                602 => "EG Египет",
                645 => "ZM Замбия",
                648 => "ZW Зимбабве",
                425 => "IL Израиль",
                404 => "IN Индия",
                405 => "IN Индия",
                510 => "ID Индонезия",
                416 => "JO Иордания",
                418 => "IQ Ирак",
                432 => "IR Иран",
                272 => "IE Ирландия",
                274 => "IS Исландия",
                214 => "ES Испания",
                222 => "IT Италия",
                421 => "YE Йемен",
                625 => "CV Кабо-Верде",
                401 => "KZ Казахстан",
                346 => "KY Каймановы Острова",
                456 => "KH Камбоджа",
                624 => "CM Камерун",
                302 => "CA Канада",
                427 => "QA Катар",
                639 => "KE Кения",
                280 => "CY Кипр",
                437 => "KG Киргизия",
                545 => "KI Кирибати",
                460 => "CN Китай",
                467 => "KP КНДР",
                732 => "CO Колумбия",
                654 => "KM Коморские Острова",
                629 => "CG Республика Конго",
                450 => "KR Республика Корея",
                712 => "CR Коста-Рика",
                612 => "CI Кот-д’Ивуар",
                368 => "CU Куба",
                419 => "KW Кувейт",
                457 => "LA Лаос",
                247 => "LV Латвия",
                651 => "LS Лесото",
                618 => "LR Либерия",
                415 => "LB Ливан",
                606 => "LY Ливия",
                246 => "LT Литва",
                295 => "LI Лихтенштейн",
                270 => "LU Люксембург",
                617 => "MU Маврикий",
                609 => "MR Мавритания",
                646 => "MG Мадагаскар",
                455 => "MO Макао",
                294 => "MK Северная Македония",
                650 => "MW Малави",
                502 => "MY Малайзия",
                610 => "ML Мали",
                472 => "MV Мальдивы",
                278 => "MT Мальта",
                604 => "MA Марокко",
                551 => "MH Маршалловы Острова",
                334 => "MX Мексика",
                643 => "MZ Мозамбик",
                259 => "MD Молдавия",
                212 => "MC Монако",
                428 => "MN Монголия",
                354 => "MS Монтсеррат",
                414 => "MM Мьянма",
                649 => "NA Намибия",
                536 => "NR Науру",
                429 => "NP Непал",
                614 => "NE Нигер",
                621 => "NG Нигерия",
                204 => "NL Нидерланды",
                362 => "AN Нидерландские Антильские острова",
                710 => "NI Никарагуа",
                546 => "NC Новая Каледония",
                530 => "NZ Новая Зеландия",
                242 => "NO Норвегия",
                424 => "AE Объединённые Арабские Эмираты",
                430 => "AE Объединённые Арабские Эмираты(Абу-Даби)",
                431 => "AE Объединённые Арабские Эмираты(Дубай)",
                422 => "OM Оман",
                548 => "CK Острова Кука",
                410 => "PK Пакистан",
                552 => "PW Палау",
                423 => "PS Палестинские территории",
                714 => "PA Панама",
                537 => "PG Папуа — Новая Гвинея",
                744 => "PY Парагвай",
                716 => "PE Перу",
                260 => "PL Польша",
                268 => "PT Португалия",
                330 => "PR Пуэрто-Рико",
                647 => "RE Реюньон",
                250 => "RU Россия",
                635 => "RW Руанда",
                226 => "RO Румыния",
                706 => "SV Сальвадор",
                549 => "WS Самоа",
                292 => "SM Сан-Марино",
                626 => "ST Сан-Томе и Принсипи",
                420 => "SA Саудовская Аравия",
                653 => "SZ Свазиленд",
                534 => "MP Северные Марианские острова",
                633 => "SC Сейшельские Острова",
                608 => "SN Сенегал",
                308 => "PM Сен-Пьер и Микелон",
                356 => "KN Сент-Китс и Невис",
                358 => "LC Сент-Люсия",
                360 => "VC Сент-Винсент и Гренадины",
                220 => "RS Сербия",
                525 => "SG Сингапур",
                417 => "SY Сирия",
                310 => "US Соединённые Штаты Америки",
                311 => "US Соединённые Штаты Америки",
                312 => "US Соединённые Штаты Америки",
                313 => "US Соединённые Штаты Америки",
                314 => "US Соединённые Штаты Америки",
                315 => "US Соединённые Штаты Америки",
                316 => "US Соединённые Штаты Америки",
                231 => "SK Словакия",
                293 => "SI Словения",
                540 => "SB Соломоновы Острова",
                637 => "SO Сомали",
                634 => "SD Судан",
                746 => "SR Суринам",
                619 => "SL Сьерра-Леоне",
                436 => "TJ Таджикистан",
                466 => "TW Тайвань",
                520 => "TH Таиланд",
                640 => "TZ Танзания",
                376 => "TC Теркс и Кайкос",
                374 => "TT Тринидад и Тобаго",
                615 => "TG Того",
                539 => "TO Тонга",
                605 => "TN Тунис",
                438 => "TM Туркмения",
                286 => "TR Турция",
                641 => "UG Уганда",
                434 => "UZ Узбекистан",
                543 => "WF Уоллис и Футуна",
                255 => "UA Украина",
                748 => "UY Уругвай",
                288 => "FO Фарерские острова",
                550 => "FM Федеративные Штаты Микронезии",
                542 => "FJ Фиджи",
                515 => "PH Филиппины",
                244 => "FI Финляндия",
                750 => "FK Фолклендские Острова",
                208 => "FR Франция",
                742 => "GF Французская Гвиана",
                547 => "PF Французская Полинезия",
                219 => "HR Хорватия",
                623 => "CF Центральноафриканская Республика",
                622 => "TD Чад",
                297 => "ME Черногория",
                230 => "CZ Чехия",
                730 => "CL Чили",
                413 => "LK Шри-Ланка",
                228 => "CH Швейцария",
                240 => "SE Швеция",
                740 => "EC Эквадор",
                627 => "GQ Экваториальная Гвинея",
                657 => "ER Эритрея",
                248 => "EE Эстония",
                636 => "ET Эфиопия",
                655 => "ZA Южно-Африканская Республика",
                338 => "JM Ямайка",
                441 => "JP Япония",
                440 => "JP Япония",
                _ => "None"
            };

            sim.Type = imsi switch
            {
                1 or 4 or 10 or 13 or 92 => SimOperatorType.MTS,
                6 or 99 => SimOperatorType.BEELINE,
                2 or 25 => SimOperatorType.MEGAFON,
                11 => SimOperatorType.YOTA,
                5 or 9 or 12 or 17 or 18 or 20 or 26 or 40 or 45 or 62 => SimOperatorType.TELE2,
                59 => SimOperatorType.WIFIRE,
                3 or 39 => SimOperatorType.ROSTELEKOM,
                _ => SimOperatorType.NONE
            };

            return sim;
        }
        /// <summary>
        /// Парсинг типа регистрации в сети
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static NetworkRegType CregParse(string data) => data switch
        {
            "0" => NetworkRegType.NOT_REG,
            "1" => NetworkRegType.REGISTERED,
            "2" => NetworkRegType.SEARCH,
            "3" => NetworkRegType.DENIED,
            "4" => NetworkRegType.UNKNOWN,
            "5" => NetworkRegType.ROAMING,
            _ => NetworkRegType.UNKNOWN
        };
        /// <summary>
        /// Парсинг показаний качества сигнала QoS
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static QosInfo CsqParse(int rssi, int ber)
        {
            QosInfo qos = new QosInfo();

            if (rssi == 99) qos.Asu = 0;
            else
            {
                int dBm = Math.Abs(-113 + rssi * 2);
                if (dBm >= 101) { qos.Asu = 0; }
                else if (dBm >= 96 && dBm <= 100) { qos.Asu = 1; }
                else if (dBm >= 92 && dBm <= 95) { qos.Asu = 2; }
                else if (dBm >= 87 && dBm <= 91) { qos.Asu = 3; }
                else if (dBm >= 83 && dBm <= 86) { qos.Asu = 4; }
                else if (dBm >= 50 && dBm <= 82) { qos.Asu = 5; }
            }

            if (ber == 99) qos.Error = 0;
            else if (ber == 0) { qos.Error = 0.2; }
            else if (ber == 1) { qos.Error = 0.2; }
            else if (ber == 2) { qos.Error = 0.4; }
            else if (ber == 3) { qos.Error = 0.8; }
            else if (ber == 4) { qos.Error = 1.6; }
            else if (ber == 5) { qos.Error = 3.2; }
            else if (ber == 6) { qos.Error = 6.4; }
            else if (ber == 7) { qos.Error = 12.8; }

            return qos;
        }
        /// <summary>
        /// Парсинг ответа на USSD запрос баланса 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CusdParse(string data)
        {
            string balance = string.Empty;
            var temp = data.Replace("\r\n", "").Replace("OK", "").Split("\"",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var item in temp)
            {
                if (item.Contains("balance", StringComparison.OrdinalIgnoreCase)
                    || item.Contains("balans", StringComparison.OrdinalIgnoreCase)
                    || item.Contains("баланс", StringComparison.OrdinalIgnoreCase))
                {
                    balance = item.Contains("Balans", StringComparison.OrdinalIgnoreCase) ? item.Replace("Balans", "")
                        : item.Contains("Balance", StringComparison.OrdinalIgnoreCase) ? item.Replace("Balance", "")
                        : item.Contains("Баланс", StringComparison.OrdinalIgnoreCase) ? item.Replace("Баланс", "")
                        : item;
                    balance = balance.Replace(":", "");
                    balance = balance.Contains("rub", StringComparison.OrdinalIgnoreCase) ? balance.Replace("rub", "")
                        : balance.Contains("руб", StringComparison.OrdinalIgnoreCase) ? balance.Replace("руб", "")
                        : balance;
                    balance = balance.Contains("r", StringComparison.OrdinalIgnoreCase) ? balance.Replace("r", "")
                        : balance.Contains("р", StringComparison.OrdinalIgnoreCase) ? balance.Replace("р", "")
                        : balance;
                    if (balance.EndsWith('.'))
                        balance = balance.Remove(balance.Length - 1);
                }
            }
            return balance.Replace(" ", "");
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
