#nullable enable

using System.Collections.Generic;
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    using Extra = KeyValuePair<string, object>;

    public class Logger : ILogger
    {
        private readonly string _name;
        private int _level;
        private ILogger? _parent;
        private HashSet<IHandler>? _handlers = null;
        private static readonly IHandler _defaultHandler = new UnityConsoleHandler(); // FIXME

        // object locks
        private readonly object _handlersLock = new();

        // System.Object overrides
        public override string ToString()
        {
            return $"{typeof(Logger).FullName}: {_name}";
        }

        #region Constructors

        internal Logger(string name)
        {
            _name = name;
            _level = (int)LogLevel.NotSet;
        }

        internal Logger(string name, int level)
        {
            _name = name;
            _level = level;
        }

        internal Logger(string name, LogLevel level)
        {
            _name = name;
            _level = (int)level;
        }

        #endregion

        #region ILogger impls

        int ILogger.Level => _level;
        ILogger? ILogger.Parent
        {
            get => _parent;
            set
            {
                _parent = value;
            }
        }

        public string Name => _name;

        public int EffectiveLevel
        {
            get
            {
                ILogger? logger = this;
                while (logger is not null)
                {
                    if (logger.Level != (int)LogLevel.NotSet)
                    {
                        return logger.Level;
                    }
                    logger = logger.Parent;
                }
                return (int)LogLevel.NotSet;
            }
        }

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void Log(int level, string message, params Extra[] extra) => _Log(level, message, extra);
        public void Debug(string message, params Extra[] extra) => _Log((int)LogLevel.Debug, message, extra);
        public void Info(string message, params Extra[] extra) => _Log((int)LogLevel.Info, message, extra);
        public void Warning(string message, params Extra[] extra) => _Log((int)LogLevel.Warning, message, extra);
        public void Error(string message, params Extra[] extra) => _Log((int)LogLevel.Error, message, extra);
        public void Critical(string message, params Extra[] extra) => _Log((int)LogLevel.Critical, message, extra);

        public bool IsEnabledFor(int level)
        {
            return level >= EffectiveLevel;
        }

        public void AddHandler(IHandler handler)
        {
            lock (_handlersLock)
            {
                if (_handlers is null)
                {
                    _handlers = new();
                }
                _handlers.Add(handler);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        protected void _Log(int level, in string message, Extra[] extra)
        {
            if (!IsEnabledFor(level))
            {
                return;
            }

            var record = new LogRecord(_name, level, message, extra);
            if (_handlers is not null)
            {
                foreach (var handler in _handlers)
                {
                    if (level >= handler.Level)
                    {
                        handler.Emit(record);
                    }
                }
            }
            else
            {
                if (level >= _defaultHandler.Level)
                {
                    _defaultHandler.Emit(record);
                }
            }
        }

        #endregion

    }
}