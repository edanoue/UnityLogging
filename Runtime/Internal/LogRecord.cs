#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    using Extra = KeyValuePair<string, object>;

    /// <summary>
    /// </summary>
    internal sealed class LogRecord : ILogRecord
    {
        private readonly Extra[] _extras;

        internal LogRecord(string name, int level, string message, Extra[] extra)
        {
            Name = name;
            Level = level;
            Message = message;
            _extras = extra;
        }

        public string Name { get; }
        public int Level { get; }
        public string Message { get; }
        public string LevelName => $"{(LogLevel) Level}";

        public bool TryGetExtra<T>(string key, out T? value)
        {
            try
            {
                var valuePair = _extras.First(kv => kv.Key == key);
                if (valuePair.Value is T castedValue)
                {
                    value = castedValue;
                    return true;
                }

                value = default;
                return false;
            }
            catch (InvalidOperationException)
            {
                value = default;
                return false;
            }
        }
    }
}