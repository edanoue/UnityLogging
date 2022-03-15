#nullable enable

using System;
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
        private static readonly Dictionary<string, IManagedItem> LoggerDict = new();
        private static readonly object LoggerDictLock = new();

        static Manager()
        {
            // Create new root logger this static constructor
            // root logger default level is "Warning"
            // See. https://github.com/python/cpython/blob/main/Lib/logging/__init__.py#L1939-L1941
            Root = new RootLogger((int) LogLevel.Warning);
        }

        #region Public API

        public static ILogger Root { get; }

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
            lock (LoggerDictLock)
            {
                if (LoggerDict.TryGetValue(name, out var node))
                {
                    switch (node)
                    {
                        case ILogger cachedLogger:
                            logger = cachedLogger;
                            break;
                        // Contains but placeholder
                        case PlaceHolder placeHolder:
                            // Remove from Dict
                            LoggerDict.Remove(name);
                            // Create new logger
                            logger = new Logger(name); // FIXME
                            LoggerDict.Add(name, logger);
                            _FixupChildren(placeHolder, logger);
                            _FixupParents(logger);
                            break;
                        default:
                            throw new InvalidProgramException();
                    }
                }
                else
                {
                    logger = new Logger(name); // FIXME
                    LoggerDict.Add(name, logger);
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
            var substrCount = nameSplit.Length; // e.g. 3
            ILogger? parent = null;

            // Skip self
            for (var i = substrCount; i > 1 && parent is null; i--)
            {
                // when input logger name: foo.bar.baz
                // iter-0: foo.bar
                // iter-1: foo
                var substr = "";
                for (var j = 0; j < i - 1; j++)
                {
                    if (j > 0)
                        substr += CONST.NAME_SEPARATOR;
                    substr += nameSplit[j];
                }

                // Already contains in map
                if (LoggerDict.TryGetValue(substr, out var node))
                    switch (node)
                    {
                        case ILogger parentLogger:
                            parent = parentLogger;
                            break;
                        case PlaceHolder placeholder:
                            placeholder.AppendChild(logger);
                            break;
                        default:
                            throw new InvalidProgramException("Invalid type founded");
                    }
                // New entry
                else
                    LoggerDict.Add(substr, new PlaceHolder(logger));
            }

            parent ??= Root;
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
                if (parentCandidate is null) continue;
                if (parentCandidate.Name.StartsWith(newLoggerName)) continue;
                logger.Parent = parentCandidate;
                childLogger.Parent = logger;
            }
        }

        #endregion
    }
}