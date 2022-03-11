#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine; // UnityEngine.Object
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    using ILogger = Interfaces.ILogger;

    /// <summary>
    /// A root logger is not that different to any other logger, except that
    /// it must have a logging level and there is only one instance of it in
    /// the hierarchy.
    /// </summary>
    internal class RootLogger : Logger
    {
        /// <summary>
        /// Initialize the logger with the name "root".
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        internal RootLogger(int level) : base("root", level) { }
    }
}