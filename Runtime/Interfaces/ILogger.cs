#nullable enable
using System.Collections.Generic;

namespace Edanoue.Logging.Interfaces
{
    public interface ILogger : IManagedItem
    {
        #region Public API

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
        public void SetLevel(LogLevel level);

        /// <summary>
        /// Log message with seveirty Debug
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message);

        /// <summary>
        /// Log message with seveirty Info
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message);

        /// <summary>
        /// Log message with seveirty Warning
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message);

        /// <summary>
        /// Log message with seveirty Error
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message);

        /// <summary>
        /// Log message with seveirty Critical
        /// </summary>
        /// <param name="message"></param>
        public void Critical(string message);

        /// <summary>
        /// Log message with the integer severity level.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public void Log(int level, string message);

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
        public bool IsEnabledFor(LogLevel level);

        #endregion

        #region Internal API

        int Level { get; }
        string Name { get; }
        ILogger? Parent { get; set; }

        #endregion

    }
}