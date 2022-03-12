#nullable enable
using System.Collections.Generic;

namespace Edanoue.Logging.Interfaces
{
    using Extra = KeyValuePair<string, object>;

    public interface ILogger : IManagedItem
    {
        #region Public API

        /// <summary>
        /// Get the logger name
        /// </summary>
        /// <value></value>
        public string Name { get; }

        /// <summary>
        /// Get the effective level for this logger.
        /// </summary>
        /// <note> 
        /// Loop through this logger and its parent in the logger hierarchy,
        /// looking for a non-zero logging level. Return the first one found.
        /// </note>
        /// <value></value>
        public int EffectiveLevel { get; }

        /// <summary>
        /// Set the logging level of this logger.
        /// </summary>
        /// <param name="level"></param>
        public void SetLevel(int level);

        /// <summary>
        /// Set the logging level of this logger.
        /// </summary>
        /// <param name="level"></param>
        public void SetLevel(LogLevel level) => SetLevel((int)level);

        /// <summary>
        /// Log message with seveirty Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        public void Debug(string message, params Extra[] extra);

        /// <summary>
        /// Log message with seveirty Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        public void Info(string message, params Extra[] extra);

        /// <summary>
        /// Log message with seveirty Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        public void Warning(string message, params Extra[] extra);

        /// <summary>
        /// Log message with seveirty Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        public void Error(string message, params Extra[] extra);

        /// <summary>
        /// Log message with seveirty Critical
        /// </summary>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        public void Critical(string message, params Extra[] extra);

        /// <summary>
        /// Log message with the integer severity level.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        public void Log(int level, string message, params Extra[] extra);

        /// <summary>
        /// Is this logger enabled for level?
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabledFor(int level);

        /// <summary>
        /// Is this logger enabled for level?
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabledFor(LogLevel level) => IsEnabledFor((int)level);

        /// <summary>
        /// Add the specified handler to this logger.
        /// </summary>
        /// <param name="handler"></param>
        public void AddHandler(IHandler handler);

        #endregion

        #region Internal API

        internal int Level { get; }
        internal ILogger? Parent { get; set; }

        #endregion

    }
}