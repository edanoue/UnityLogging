#nullable enable

namespace Edanoue.Logging.Interfaces
{
    /// <summary>
    /// </summary>
    internal interface ILogRecord
    {
        /// <summary>
        /// Logger name
        /// </summary>
        /// <value></value>
        public string Name { get; }

        /// <summary>
        /// Logged level
        /// </summary>
        /// <value></value>
        public int Level { get; }

        public string LevelName { get; }
        public string Message { get; }
        public bool TryGetExtra<T>(string key, out T? value);
    }
}