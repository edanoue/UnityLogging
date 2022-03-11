#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine; // UnityEngine.Object
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging
{
    using ILogger = Interfaces.ILogger;

    public class Logger : ILogger
    {
        private readonly string _name;
        private int _level;
        private readonly Dictionary<int, bool> _isEnabledForCache = new();
        private ILogger? _parent;

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

        string ILogger.Name => _name;
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

        public void Debug(string message) => Log((int)LogLevel.Debug, message);
        public void Info(string message) => Log((int)LogLevel.Info, message);
        public void Warning(string message) => Log((int)LogLevel.Warning, message);
        public void Error(string message) => Log((int)LogLevel.Error, message);
        public void Critical(string message) => Log((int)LogLevel.Critical, message);

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

        #region Public API

        public void Debug(string message, UnityEngine.Object context) => Log((int)LogLevel.Debug, message, context);
        public void Info(string message, UnityEngine.Object context) => Log((int)LogLevel.Info, message, context);
        public void Warning(string message, UnityEngine.Object context) => Log((int)LogLevel.Warning, message, context);
        public void Error(string message, UnityEngine.Object context) => Log((int)LogLevel.Error, message, context);
        public void Critical(string message, UnityEngine.Object context) => Log((int)LogLevel.Critical, message, context);
        public void Log(int level, string message, UnityEngine.Object context)
        {
            if (IsEnabledFor(level))
                this._Log(level, in message, context);
        }

        public override string ToString()
        {
            return $"{typeof(Logger).FullName}: {_name}";
        }

        #endregion

        #region Helper Methods

        void _Log(int level, in string message, UnityEngine.Object? context = null)
        {
            // FIXME: Formatting Feature
            // FIXME: Handler Feature
            var levelStr = $"{(LogLevel)level}";
            string formatMessage;
            var loggerPath = _name;
            if (loggerPath == "root")
            {
                formatMessage = $"[{levelStr}] {message}";
            }
            else
            {
                formatMessage = $"[{levelStr}] [{_name}] {message}";
            }

            if (level < (int)LogLevel.Warning)
            {
                UnityEngine.Debug.Log(formatMessage, context);
            }
            else if (level < (int)LogLevel.Error)
            {
                UnityEngine.Debug.LogWarning(formatMessage, context);
            }
            // Above error
            else
            {
                UnityEngine.Debug.LogError(formatMessage, context);
            }
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