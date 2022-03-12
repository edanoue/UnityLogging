#nullable enable

namespace Edanoue.Logging.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Format the specified record as text.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        internal string Format(ILogRecord record);
    }
}