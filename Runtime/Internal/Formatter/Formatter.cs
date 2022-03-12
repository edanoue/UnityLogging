#nullable enable

using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    internal class Formatter : IFormatter
    {
        // FIXME
        string IFormatter.Format(ILogRecord record)
        {
            if (record.Name == CONST.ROOT_LOGGER_NAME)
            {
                return $"[{record.LevelName}] {record.Message}";
            }
            else
            {
                return $"[{record.LevelName}] [{record.Name}] {record.Message}";
            }
        }
    }
}