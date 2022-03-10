#nullable enable

namespace Edanoue.Logging
{
    public class LoggerConfig
    {
        public string Format = "[%(levelname)s] (%(logger)s) %(message)s";
        public int Level = LogLevel.Verbose;
    }
}