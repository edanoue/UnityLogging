#nullable enable

using System.Collections.Generic;
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    /// <summary>
    /// There is [under normal circumstances] just one Manager instance, which
    /// holds the hierarchy of loggers.
    /// </summary>
    internal static class Manager
    {
        private static readonly Dictionary<string, IManagedItem> _loggerDict = new();
        private static readonly object _loggerDictLock = new();

        static Manager()
        {
            // Create new root logger this static constructor
            // root logger default level is "Warning"
            // See. https://github.com/python/cpython/blob/main/Lib/logging/__init__.py#L1939-L1941
            Root = new RootLogger((int)LogLevel.Warning);
        }

        #region Public API

        public static ILogger Root { get; private set; }

        /// <summary>
        /// Get a logger with the specified name (channel name), creating it
        /// if it doesn't yet exist. This name is a dot-separated hierarchical
        /// name, such as "a", "a.b", "a.b.c" or similar.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILogger GetLogger(string name)
        {
            ILogger logger;
            lock (_loggerDictLock)
            {
                if (_loggerDict.TryGetValue(name, out var node))
                {
                    if (node is ILogger cachedLogger)
                    {
                        logger = cachedLogger;
                    }
                    // Contains but placeholder
                    else if (node is PlaceHolder placeHolder)
                    {
                        // Replace PlaceHolder to Logger
                        var oldPlaceHolder = placeHolder;
                        // Remove from Dict
                        _loggerDict.Remove(name);
                        // Create new logger
                        logger = new Logger(name); // FIXME
                        _loggerDict.Add(name, logger);
                        _FixupChildren(oldPlaceHolder, logger);
                        _FixupParents(logger);
                    }
                    else
                    {
                        throw new System.InvalidProgramException();
                    }
                }
                else
                {
                    logger = new Logger(name); // FIXME
                    _loggerDict.Add(name, logger);
                    _FixupParents(logger);
                }
            }

            return logger;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Ensure that there are either loggers or placeholders all the way
        /// from the specified logger to the root of the logger hierarchy.
        /// </summary>
        /// <param name="logger"></param>
        private static void _FixupParents(ILogger logger)
        {
            var fullName = logger.Name; // e.g. foo.bar.baz
            var nameSplit = fullName.Split(CONST.NAME_SEPARATOR); // e.g. [foo, bar, baz]
            int substrCount = nameSplit.Length; // e.g. 3
            ILogger? parent = null;

            // Skip self
            for (int i = substrCount; i > 1 && parent is null; i--)
            {
                // when input logger name: foo.bar.baz
                // iter-0: foo.bar
                // iter-1: foo
                string substr = "";
                for (int j = 0; j < i - 1; j++)
                {
                    if (j > 0)
                        substr += CONST.NAME_SEPARATOR;
                    substr += nameSplit[j];
                }

                // Already contains in map
                if (_loggerDict.TryGetValue(substr, out var node))
                {
                    if (node is ILogger parentLogger)
                    {
                        parent = parentLogger;
                    }
                    else if (node is PlaceHolder placeholder)
                    {
                        placeholder.AppendChild(logger);
                    }
                    else
                    {
                        throw new System.InvalidProgramException("Invalid type founded");
                    }
                }
                // New entry
                else
                {
                    _loggerDict.Add(substr, new PlaceHolder(logger));
                }
            }

            if (parent is null)
            {
                parent = Root;
            }

            logger.Parent = parent;
        }


        /// <summary>
        /// Ensure that children of the placeholder ph are connected to the
        /// specified logger.
        /// </summary>
        /// <param name="placeHolder"></param>
        /// <param name="logger"></param>
        private static void _FixupChildren(PlaceHolder placeHolder, ILogger logger)
        {
            var newLoggerName = logger.Name; // foo.bar

            foreach (var childLogger in placeHolder.Children)
            {
                var parentCandidate = childLogger.Parent;
                if (parentCandidate is not null)
                {
                    if (!parentCandidate.Name.StartsWith(newLoggerName))
                    {
                        logger.Parent = parentCandidate;
                        childLogger.Parent = logger;
                    }
                }
            }
        }

        #endregion
    }
}