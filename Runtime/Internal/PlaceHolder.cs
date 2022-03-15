#nullable enable

using System.Collections.Generic;
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    internal sealed class PlaceHolder : IManagedItem
    {
        private readonly HashSet<ILogger> _loggerMap = new();

        internal PlaceHolder(ILogger childLogger)
        {
            AppendChild(childLogger);
        }

        internal IEnumerable<ILogger> Children => _loggerMap;

        internal void AppendChild(ILogger logger)
        {
            _loggerMap.Add(logger);
        }
    }
}