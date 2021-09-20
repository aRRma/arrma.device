namespace arrma.device.interfaces
{
    /// <summary>
    /// Перечисление типов регистрации модема в сети
    /// </summary>
    public enum NetRegType
    {
        /// <summary>
        /// Неизвестно
        /// </summary>
        NONE,
        /// <summary>
        /// Не зарегистрирован, поиска сети нет
        /// </summary>
        NOT,
        /// <summary>
        /// Зарегистрирован, домашняя сеть
        /// </summary>
        REGISTERED,
        /// <summary>
        /// Не зарегистрирован, идёт поиск новой сети
        /// </summary>
        SEARCH,
        /// <summary>
        /// Регистрация отклонена
        /// </summary>
        DENIED,
        /// <summary>
        /// Неизвестно
        /// </summary>
        UNKNOWN,
        /// <summary>
        /// Роуминг
        /// </summary>
        ROAMING
    }
    /// <summary>
    /// Перечисление типов операторов сим карт
    /// </summary>
    public enum SimOperatorType
    {
        NONE,
        MTS,
        BEELINE,
        MEGAFON,
        YOTA,
        TELE2,
        WIFIRE,
        ROSTELEKOM
    }
}
