#nullable enable

using System.Collections.Generic;
using System.Linq;
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    using Extra = KeyValuePair<string, object>;

    public class Logger : ILogger
    {
        private static readonly IHandler DefaultHandler = new UnityConsoleHandler(); // FIXME

        // object locks
        private readonly object _handlersLock = new();
        private HashSet<IHandler>? _handlers;
        private int _level;

        // System.Object overrides
        public override string ToString()
        {
            return $"{typeof(Logger).FullName}: {Name}";
        }

        #region Helper Methods

        /// <summary>
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        private void _Log(int level, in string message, Extra[] extra)
        {
            if (!IsEnabledFor(level)) return;

            var record = new LogRecord(Name, level, message, extra);
            if (_handlers is not null)
            {
                foreach (var handler in _handlers.Where(handler => level >= handler.Level))
                    handler.Emit(record);
            }
            else
            {
                if (level >= DefaultHandler.Level) DefaultHandler.Emit(record);
            }
        }

        #endregion

        #region Constructors

        internal Logger(string name)
        {
            Name = name;
            _level = (int) LogLevel.NotSet;
        }

        internal Logger(string name, int level)
        {
            Name = name;
            _level = level;
        }

        internal Logger(string name, LogLevel level)
        {
            Name = name;
            _level = (int) level;
        }

        #endregion

        #region ILogger impls

        int ILogger.Level => _level;

        ILogger? ILogger.Parent { get; set; }

        public string Name { get; }

        public int EffectiveLevel
        {
            get
            {
                ILogger? logger = this;
                while (logger is not null)
                {
                    if (logger.Level != (int) LogLevel.NotSet) return logger.Level;
                    logger = logger.Parent;
                }

                return (int) LogLevel.NotSet;
            }
        }

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void Log(int level, string message, params Extra[] extra)
        {
            _Log(level, message, extra);
        }

        public void Debug(string message, params Extra[] extra)
        {
            _Log((int) LogLevel.Debug, message, extra);
        }

        public void Info(string message, params Extra[] extra)
        {
            _Log((int) LogLevel.Info, message, extra);
        }

        public void Warning(string message, params Extra[] extra)
        {
            _Log((int) LogLevel.Warning, message, extra);
        }

        public void Error(string message, params Extra[] extra)
        {
            _Log((int) LogLevel.Error, message, extra);
        }

        public void Critical(string message, params Extra[] extra)
        {
            _Log((int) LogLevel.Critical, message, extra);
        }

        public bool IsEnabledFor(int level)
        {
            return level >= EffectiveLevel;
        }

        public void AddHandler(IHandler handler)
        {
            lock (_handlersLock)
            {
                _handlers ??= new HashSet<IHandler>();
                _handlers.Add(handler);
            }
        }

        #endregion
    }
}