#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine; // UnityEngine.Object

namespace Edanoue.Logging
{
    public class Logger
    {
        static readonly string _rootLoggerName = "__root__";
        readonly HashSet<Logger> _children = new();

        LoggerConfig? _config = null;

        string Name { get; set; }
        Logger? Parent { get; set; }
        LoggerConfig Config
        {
            get
            {
                if (this.IsRoot)
                {
                    return _config!;
                }

                if (this._config is null)
                {
                    return Parent!.Config;
                }
                else
                {
                    return _config;
                }
            }
        }

        internal string Path
        {
            get
            {
                if (this.IsRoot)
                    return "";
                else
                    return $"{Parent!.Name}/{this.Name}";
            }
        }

        public void SetConfig(LoggerConfig config)
        {
            _config = config;
        }

        internal static Logger GetRootLogger()
        {
            var rootLogger = new Logger(_rootLoggerName);
            rootLogger.SetConfig(new LoggerConfig());
            return rootLogger;
        }

        public bool IsRoot => Parent is null;

        private bool AddChild(Logger child)
        {
            return this._children.Add(child);
        }

        private bool RemoveChild(Logger child)
        {
            return this._children.Remove(child);
        }

        internal void SetParent(Logger parent)
        {
            if (this.Parent is not null)
            {
                this.Parent.RemoveChild(this);
                this.Parent = null;
            }
            this.Parent = parent;
            this.Parent.AddChild(this);
        }

        internal Logger(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Log message with severity "Verbose".
        /// </summary>
        /// <param name="message"></param>
        public void Verbose(string message)
        {
            this.Log(LogLevel.Verbose, in message);
        }

        public void Verbose(string message, UnityEngine.Object context)
        {
            this.Log(LogLevel.Verbose, in message, context);
        }

        /// <summary>
        /// Log message with severity "Info".
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            this.Log(LogLevel.Info, in message);
        }

        public void Info(string message, UnityEngine.Object context)
        {
            this.Log(LogLevel.Verbose, in message, context);
        }

        /// <summary>
        /// Log message with severity "Warning".
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            this.Log(LogLevel.Warning, in message);
        }

        public void Warning(string message, UnityEngine.Object context)
        {
            this.Log(LogLevel.Verbose, in message, context);
        }

        /// <summary>
        /// Log message with severity "Error".
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            this.Log(LogLevel.Error, in message);
        }

        public void Error(string message, UnityEngine.Object context)
        {
            this.Log(LogLevel.Error, in message, context);
        }

        /// <summary>
        /// Log message with severity "Fatal".
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string message)
        {
            this.Log(LogLevel.Fatal, in message);
        }

        public void Fatal(string message, UnityEngine.Object context)
        {
            this.Log(LogLevel.Fatal, in message, context);
        }

        #region Helper Methods

        void Log(int level, in string message, UnityEngine.Object? context = null)
        {
            var levelStr = LogLevel.ToString(level);
            Console.WriteLine($"[{levelStr}] {message}");
        }

        #endregion
    }
}