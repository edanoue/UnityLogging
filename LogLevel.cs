#nullable enable

namespace Edanoue.Logging
{
    public static class LogLevel
    {
        public const int Fatal = 300;
        public const int Error = 200;
        public const int Warning = 100;
        public const int Info = 0;
        public const int Verbose = -100;

        public static string ToString(int level)
        {
            return level switch
            {
                Fatal => "Fatal",
                Error => "Error",
                Warning => "Warning",
                Info => "Info",
                Verbose => "Verbose",
                _ => $"{level}",
            };
        }
    }

}