#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    using Extra = KeyValuePair<string, object>;

    /// <summary>
    /// 
    /// </summary>
    internal sealed class LogRecord : ILogRecord
    {
        private readonly Extra[] _extras;

        public string Name { get; private set; }
        public int Level { get; private set; }
        public string Message { get; private set; }
        public string LevelName => $"{(LogLevel)Level}";
        public bool TryGetExtra<T>(string key, out T? value)
        {
            try
            {
                var valuePair = _extras.Where(kv => kv.Key == key).First();
                if (valuePair.Value is T castedValue)
                {
                    value = castedValue;
                    return true;
                }
                else
                {
                    value = default;
                    return false;
                }
            }
            catch (InvalidOperationException)
            {
                value = default;
                return false;
            }
        }

        internal LogRecord(string name, int level, string message, Extra[] extra)
        {
            Name = name;
            Level = level;
            Message = message;
            _extras = extra;
        }
    }
}