#nullable enable

using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    internal class Formatter : IFormatter
    {
        string IFormatter.Format(ILogRecord record)
        {
            // FIXME
            return $"[{record.LevelName}] [{record.Name}] {record.Message}";
        }
    }
}