namespace arrma.device.core
{
    /// <summary>
    /// Перечисление команд AT протокола для модема Teleofis RX108-R2
    /// </summary>
    public enum AtCommand
    {
        //настройка модема
        /// <summary>
        /// команда проверки связи с модемом  
        /// </summary>
        AT_,
        /// <summary>
        /// команда отключения эхо  (if parameter is omitted, the command has the same behaviour of ATE0)
        /// </summary>
        AT_E0,
        /// <summary>
        /// команда отключения автоматического ответа на звонок после первого гудка (0 - auto answer disabled (factory default))
        /// </summary>
        AT_S0,
        /// <summary>
        /// команда включения определение номера входящего звонка
        /// </summary>
        AT_CLIP,
        /// <summary>
        /// команда перехода из PDU режима в текстовый режим СМС
        /// </summary>
        AT_CMGF,
        /// <summary>
        /// команда запроса статуса регистрации модема в сети
        /// </summary>
        AT_CREG,
        /// <summary>
        /// команда запроса статуса качества связи
        /// </summary>
        AT_CSQ,

        //системная информация 
        /// <summary>
        /// команда запроса идент. производителя
        /// </summary>
        AT_GMI,
        /// <summary>
        /// команда запроса идент. модели
        /// </summary>
        AT_GMM,
        /// <summary>
        /// команда запроса версии по
        /// </summary>
        AT_GMR,
        /// <summary>
        /// команда запроса серийного номера
        /// </summary>
        AT_CGSN,
        /// <summary>
        /// команда запроса серийного номера сим-карты
        /// </summary>
        AT_CCID,
        /// <summary>
        /// команда запроса информации об операторе
        /// </summary>
        AT_COPS,
        /// <summary>
        /// команда запроса кода IMSI (Mobile Subscriber Identity) тип оператора
        /// </summary>
        AT_CIMI,

        //баланс
        /// <summary>
        /// команда запроса USSD команды для запроса баланса
        /// </summary>
        AT_CUSD,

        //перезагрузка
        /// <summary>
        /// команда перезагрузки модема
        /// </summary>
        AT_REBOOT,

        //смс
        /// <summary>
        /// команда запроса статуса хранилища смс сообщений
        /// </summary>
        AT_CPMS,
        /// <summary>
        /// команда запроса все смс сообщения
        /// </summary>
        AT_CMGL,
        /// <summary>
        /// команда чтения смс под номером 1
        /// </summary>
        AT_CMGR,
        /// <summary>
        /// команда удаления всех смс сообщений
        /// </summary>
        AT_CMGD,
        /// <summary>
        /// команда отправить смс на номер
        /// </summary>
        AT_CMGS,

        //звонки
        /// <summary>
        /// отклонить звонок
        /// </summary>
        AT_H0
    }

    public enum AtCommandEnd
    {
        /// <summary>
        /// Не известно
        /// </summary>
        NONE,
        /// <summary>
        /// CR (0x0D HEX) (\r escape) [Enter]
        /// </summary>
        CR,
        /// <summary>
        /// SUB (0x1A HEX) (\x1A escape) [Ctrl Z]
        /// </summary>
        SUB,
        /// <summary>
        /// _ (0x20 HEX) (\x20 escape) [Space]
        /// </summary>
        SPACE,
        /// <summary>
        /// LF (0x0A HEX) (\n escape) [Line Feed]
        /// </summary>
        LF,
        /// <summary>
        /// CRLF
        /// </summary>
        CRLF
    }

    public enum AtModemAnswer
    {
        /// <summary>
        /// Не известно
        /// </summary>
        NONE,
        /// <summary>
        /// Ок
        /// </summary>
        OK,
        /// <summary>
        /// Ошибка
        /// </summary>
        ERROR,
        /// <summary>
        /// Ввод смс
        /// </summary>
        ENTER_SMS
    }


    /// <summary>
    /// Перечисление состояний модема
    /// </summary>
    public enum AtModemState
    {
        /// <summary>
        /// ничего не делаем
        /// </summary>
        NONE,
        /// <summary>
        /// настройка модема завершена
        /// </summary>
        INIT_DONE,
        /// <summary>
        /// отправить любую команду
        /// </summary>
        SEND_SOME_COMM,
        /// <summary>
        /// прочитать ответ на команду
        /// </summary>
        READ_ANSWER,
        /// <summary>
        /// ожидание звонка
        /// </summary>
        WAIT_CALL,
        /// <summary>
        /// выполнение команды по звонку
        /// </summary>
        EXECUTE_COMMAND,
        /// <summary>
        /// отправить смс
        /// </summary>
        SEND_SMS,
        /// <summary>
        /// прочитать смс
        /// </summary>
        READ_SMS,

        //настройка модема
        /// <summary>
        /// отключить эхо
        /// </summary>
        ATE,
        /// <summary>
        /// отключение автоответа на звонок
        /// </summary>
        ATS,
        /// <summary>
        /// включить АОН
        /// </summary>
        ATCLIP,
        /// <summary>
        /// перейти в текстовый режим
        /// </summary>
        ATCMGF,
        /// <summary>
        /// выключить авто сброс звонка (на время пока мы настраиваемся)
        /// </summary>
        ATCESTHLCK,

        //статус связи
        /// <summary>
        /// проверить тип регистрации в сети
        /// </summary>
        ATCREG,
        /// <summary>
        /// проверить качество связи
        /// </summary>
        ATCSQ,

        //системная информация
        /// <summary>
        /// запросить идент. произодителя модема
        /// </summary>
        ATGMI,
        /// <summary>
        /// запросить идент. модели модуля модема
        /// </summary>
        ATGMM,
        /// <summary>
        /// запросить идент. версии по модуля модема
        /// </summary>
        ATGMR,
        /// <summary>
        /// запросить серийный номер IMEI модема
        /// </summary>
        ATCGSN,
        /// <summary>
        /// запросить серийный номер SIM-карты
        /// </summary>
        ATCCID,
        /// <summary>
        /// запросить код IMSI (Mobile Subscriber Identity) типа оператора
        /// </summary>
        ATCIMI,

        //баланс
        /// <summary>
        /// запросить баланс
        /// </summary>
        ATCUSD,

        //СМС  
        /// <summary>
        /// статус хранилища смс
        /// </summary>
        ATCPMS,
        /// <summary>
        /// запросить все смс
        /// </summary>
        ATCMGL,
        /// <summary>
        /// запросить смс под номером n
        /// </summary>
        ATCMGR,
        /// <summary>
        /// удалить все смс
        /// </summary>
        ATCMGD,
        /// <summary>
        /// отправить смс
        /// </summary>
        ATCMGS,

        //перезагрузка
        /// <summary>
        /// перезагрузка модема
        /// </summary>
        ATREBOOT,
        /// <summary>
        /// пинг модема
        /// </summary>
        PING,
        /// <summary>
        /// сброс в заводские настройки
        /// </summary>
        ATCMAR
    }

    /// <summary>
    /// Перечисление статуса регистрации модема в сети
    /// </summary>
    public enum NetworkRegType
    {
        /// <summary>
        /// не зарегистрирован, поиска сети нет
        /// </summary>
        NOT_REG,
        /// <summary>
        /// зарегистрирован, домашняя сеть
        /// </summary>
        REGISTERED,
        /// <summary>
        /// не зарегистрирован, идёт поиск новой сети
        /// </summary>
        SEARCH,
        /// <summary>
        /// регистрация отклонена
        /// </summary>
        DENIED,
        /// <summary>
        /// неизвестно
        /// </summary>
        UNKNOWN,
        /// <summary>
        /// роуминг
        /// </summary>
        ROAMING

        // 0 - NOT registered, ME is NOT currently searching a new operator TO register TO
        // 1 - registered, home network
        // 2 - not registered, but ME is currently searching a new operator to register to
        // 3 - registration denied
        // 4 -unknown
        // 5 - registered, roaming
    }
}
