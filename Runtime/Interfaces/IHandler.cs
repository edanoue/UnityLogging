#nullable enable

namespace Edanoue.Logging.Interfaces
{
    /// <summary>
    /// Handler instances dispatch logging events to specific
    /// destinations. 
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Get the logging level of this handler. 
        /// </summary>
        /// <value></value>
        public int Level { get; }

        /// <summary>
        /// Set the logging level of this handler.
        /// </summary>
        /// <param name="level"></param>
        public void SetLevel(int level);
        public void SetLevel(LogLevel level);

        public void SetFormatter(IFormatter formatter);

        /// <summary>
        /// Emit the log message
        /// </summary>
        /// <param name="message"></param>
        internal void Emit(ILogRecord record);
    }
}