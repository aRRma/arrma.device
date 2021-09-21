namespace Arrma.Device.Interfaces.Enum
{
    public enum LogSource
    {
        OTHER,
        SERIAL_PORT,
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
