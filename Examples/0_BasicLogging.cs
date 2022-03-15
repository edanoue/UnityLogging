using Edanoue.Logging;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Examples
{
    public class BasicLogging
    {
        [Test]
        public void HelloWorld()
        {
            Logging.Warning("Watch out!"); // will print a message to the console
            Logging.Info("I told you so"); // will not print anything
        }

        [Test]
        public void WithUnityObject()
        {
            // Same to use Debug.Log(message, context)
            var anyObject = new GameObject();
            Logging.Warning("Info message with context", anyObject);
        }

        [Test]
        public void SetLogLevel()
        {
            // Anything with a severity lower than "Error" is ignored by root logger default.
            Logging.Info("Ignored");

            // Sets the minimum level at which the route logger outputs.
            Logging.SetLevel(LogLevel.Info);
            Logging.Info("Info message");

            // Reset log level for other examples
            Logging.SetLevel(LogLevel.Warning);
        }

        [Test]
        public void AvailableSeverity()
        {
            // Disable test-failing feature when logged above severity "Error".
            LogAssert.ignoreFailingMessages = true;
            // Set root-logger level 
            Logging.SetLevel(LogLevel.Debug);

            Logging.Debug("Debug Message");
            Logging.Info("Info Message");
            Logging.Warning("Warning Message");
            Logging.Error("Error Message");
            Logging.Critical("Critical Message");
            Logging.Log(100, "Use custom level message");

            // Reset log level for other examples
            Logging.SetLevel(LogLevel.Warning);
        }
    }
}