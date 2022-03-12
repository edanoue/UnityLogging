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
        private readonly Dictionary<int, bool> _isEnabledForCache = new();
        private ILogger? _parent;
        private readonly HashSet<IHandler> _handlers = new();

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
                _ClearCache();
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
            _ClearCache();
        }

        public void SetLevel(LogLevel level) => SetLevel((int)level);

        public void Log(int level, string message)
        {
            if (IsEnabledFor(level))
            {
                this._Log(level, in message);
            }
        }
        public void Log(int level, string message, params Extra[] extra)
        {
            if (IsEnabledFor(level))
            {
                this._Log(level, in message, extra);
            }
        }
        public void Debug(string message) => Log((int)LogLevel.Debug, message);
        public void Debug(string message, params Extra[] extra) => Log((int)LogLevel.Debug, message, extra);

        public void Info(string message) => Log((int)LogLevel.Info, message);
        public void Info(string message, params Extra[] extra) => Log((int)LogLevel.Info, message, extra);

        public void Warning(string message) => Log((int)LogLevel.Warning, message);
        public void Warning(string message, params Extra[] extra) => Log((int)LogLevel.Warning, message);

        public void Error(string message) => Log((int)LogLevel.Error, message);
        public void Error(string message, params Extra[] extra) => Log((int)LogLevel.Error, message, extra);

        public void Critical(string message) => Log((int)LogLevel.Critical, message);
        public void Critical(string message, params Extra[] extra) => Log((int)LogLevel.Critical, message, extra);

        public bool IsEnabledFor(int level)
        {
            if (_isEnabledForCache.TryGetValue(level, out var isEnabled))
            {
                return isEnabled;
            }
            return _isEnabledForCache[level] = level >= EffectiveLevel;
        }

        public bool IsEnabledFor(LogLevel level) => IsEnabledFor((int)level);

        #endregion

        public override string ToString()
        {
            return $"{typeof(Logger).FullName}: {_name}";
        }

        #region Helper Methods

        protected void _Log(int level, in string message, params Extra[] extra)
        {
            // FIXME
            // context みたいなやつをわたしたい
            // 事前に暮らす内部に貼る配列にある Handler を回すとかをする
            IHandler handler = new UnityConsoleHandler();
            var record = new LogRecord(_name, level, message, extra);
            // 配列を対象に取るべき
            handler.Emit(record);
        }

        /// <summary>
        ///  Clear the cache for all loggers in loggerDict
        /// Called when level changes are made
        /// </summary>
        void _ClearCache()
        {
            this._isEnabledForCache.Clear();
        }

        #endregion

    }
}