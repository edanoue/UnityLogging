#nullable enable

using UnityEngine; // UnityEngine.Object

namespace Edanoue.Logging
{
    public static class Logging
    {
        static readonly Logger _rootLogger;

        static Logging()
        {
            // Create new root logger
            _rootLogger = Logger.GetRootLogger();

            // Enable Unity Redirecting
            // UnityConsoleRedirector.Enable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Logger GetLogger(string name)
        {
            return _rootLogger.GetLogger(name);
        }

        /// <summary>
        /// Set global (root) logger config 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="level"></param>
        public static void SetGlobalConfig(LoggerConfig config)
        {
            _rootLogger.SetConfig(config);
        }

        /// <summary>
        /// Log message with severity "Verbose" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Verbose(string message)
        {
            _rootLogger.Verbose(message);
        }

        public static void Verbose(string message, UnityEngine.Object context)
        {
            _rootLogger.Verbose(message, context);
        }

        /// <summary>
        /// Log message with severity "Info" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            _rootLogger.Info(message);
        }

        public static void Info(string message, UnityEngine.Object context)
        {
            _rootLogger.Info(message, context);
        }

        /// <summary>
        /// Log message with severity "Warning" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            _rootLogger.Warning(message);
        }

        public static void Warning(string message, UnityEngine.Object context)
        {
            _rootLogger.Warning(message, context);
        }

        /// <summary>
        /// Log message with severity "Error" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            _rootLogger.Error(message);
        }

        public static void Error(string message, UnityEngine.Object context)
        {
            _rootLogger.Error(message, context);
        }

        /// <summary>
        /// Log message with severity "Fatal" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            _rootLogger.Fatal(message);
        }

        public static void Fatal(string message, UnityEngine.Object context)
        {
            _rootLogger.Fatal(message, context);
        }

    }
}