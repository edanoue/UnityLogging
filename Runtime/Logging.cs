#nullable enable

using System.Collections.Generic;
using Edanoue.Logging.Internal;
namespace Edanoue.Logging
{
    using Object = UnityEngine.Object;
    using Extra = KeyValuePair<string, object>;

    /// <summary>
    /// Utility functions at module level.
    /// Basically delegate everything to the root logger.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// Return a logger with the specified name or, if name is None, return a logger which is the root logger of the hierarchy.
        /// <param name="name">
        /// If specified, the name is typically a dot-separated hierarchical
        /// name like "a", "a.b" or "a.b.c.d". Choice of these names is entirely up to the developer who is using logging.
        /// </param>
        /// <returns>logger</returns>
        public static Logger GetLogger(string name)
        {
            if (string.IsNullOrEmpty(name) || name == "root")
                return Manager.Root;
            // FIXME
            // Cast For Unity Gameobject arguments API
            return (Logger)Manager.GetLogger(name);
        }

        /// <summary>
        /// Return a logger with the specified class, creating it if necessary.
        /// </summary>
        /// <note>
        /// GetLogger<MyClass>() is same to  GetLogger("MyNamespace.MyClass")
        /// </note>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Logger GetLogger<T>()
        {
            // Get Type fullname
            var name = typeof(T).FullName;
            // Replace nested type separator "+" to "."
            name = name.Replace("+", ".");

            // FIXME
            // Cast For Unity Gameobject arguments API
            return (Logger)Manager.GetLogger(name);
        }

        /// <summary>
        /// Set global (root) logger config 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="level"></param>
        public static void SetLevel(LogLevel level)
        {
            Manager.Root.SetLevel(level);
        }

        /// <summary>
        /// Log message with severity "Debug" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            Manager.Root.Debug(message);
        }

        /// <summary>
        /// Log message with severity "Debug" from root logger
        /// with UnityEngine.Object context
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message, Object context)
        {
            Manager.Root.Debug(message, new Extra("UnityContext", context));
        }

        /// <summary>
        /// Log message with severity "Info" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            Manager.Root.Info(message);
        }

        public static void Info(string message, Object context)
        {
            Manager.Root.Info(message, new Extra("UnityContext", context));
        }

        /// <summary>
        /// Log message with severity "Warning" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            Manager.Root.Warning(message);
        }

        public static void Warning(string message, Object context)
        {
            Manager.Root.Warning(message, new Extra("UnityContext", context));
        }

        /// <summary>
        /// Log message with severity "Error" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            Manager.Root.Error(message);
        }

        public static void Error(string message, Object context)
        {
            Manager.Root.Error(message, new Extra("UnityContext", context));
        }

        /// <summary>
        /// Log message with severity "Critical" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Critical(string message)
        {
            Manager.Root.Critical(message);
        }

        public static void Critical(string message, Object context)
        {
            Manager.Root.Critical(message, new Extra("UnityContext", context));
        }

        public static void Log(int level, string message)
        {
            Manager.Root.Log(level, message);
        }

        public static void Log(int level, string message, Object context)
        {
            Manager.Root.Log(level, message, new Extra("UnityContext", context));
        }
    }
}