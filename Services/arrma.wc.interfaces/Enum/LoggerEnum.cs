namespace arrma.wc.interfaces.Enum
{
    public enum LogSource
    {
        OTHER,
        APPLICATION,
        NETWORK,
        DB_LOCAL,
        DB_REMOTE,
        LOGGER,
        SCHEDULER,
        VM,
        PERFORMANCE
    }

    public enum LogMode
    {
        DEBUG,
        RELEASE
    }

    public enum LogEventLevel
    {
        DEBUG,
        INFORMATION,
        WARNING,
        ERROR,
        FATAL,
        ALL
    }
}
