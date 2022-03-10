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
        private Logger? _parent = null;
        private readonly HashSet<Logger> _children = new();
        private int _level;
        private readonly Dictionary<int, bool> _isEnabledForCache = new();

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

        public int EffectiveLevel
        {
            get
            {
                Logger? logger = this;
                while (logger is not null)
                {
                    if (logger._level != (int)LogLevel.NotSet)
                    {
                        return logger._level;
                    }
                    logger = logger._parent;
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
        public void Debug(string message) => Log((int)LogLevel.Debug, message);
        public void Info(string message) => Log((int)LogLevel.Info, message);
        public void Warning(string message) => Log((int)LogLevel.Warning, message);
        public void Error(string message) => Log((int)LogLevel.Error, message);
        public void Critical(string message) => Log((int)LogLevel.Critical, message);
        public void Log(int level, string message)
        {
            if (IsEnabledFor(level))
                this._Log(level, in message);
        }

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

        #endregion

        #region Internal API

        #endregion

        #region Helper Methods

        void _Log(int level, in string message, UnityEngine.Object? context = null)
        {
            var levelStr = $"{level}";
            string formatMessage;
            var loggerPath = Path;
            if (loggerPath == "")
            {
                formatMessage = $"[{levelStr}] {message}";
            }
            else
            {
                formatMessage = $"[{levelStr}] [{Path}] {message}";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Logger GetLogger(string name)
        {
            var newLogger = new Logger(name);
            newLogger.SetParent(this);
            return newLogger;
        }

        internal string Path
        {
            get
            {
                if (this.IsRoot)
                    return "";
                else
                {
                    var parentPath = _parent?.Path;
                    if (parentPath is null || parentPath == "")
                        return $"{this._name}";
                    else
                        return $"{parentPath}::{this._name}";
                }
            }
        }

        private bool IsRoot => _parent is null;

        private bool AddChild(Logger child)
        {
            return this._children.Add(child);
        }

        private bool RemoveChild(Logger child)
        {
            return this._children.Remove(child);
        }

        private void SetParent(Logger parent)
        {
            if (this._parent is not null)
            {
                this._parent.RemoveChild(this);
                this._parent = null;
            }
            this._parent = parent;
            this._parent.AddChild(this);
        }

        #region Helper Methods


        #endregion
    }

    /// <summary>
    /// A root logger is not that different to any other logger, except that
    /// it must have a logging level and there is only one instance of it in
    /// the hierarchy.
    /// </summary>
    internal class RootLogger : Logger
    {
        /// <summary>
        /// Initialize the logger with the name "root".
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        internal RootLogger(int level) : base("root", level) { }
    }
}