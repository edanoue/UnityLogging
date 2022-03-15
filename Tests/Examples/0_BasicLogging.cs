using Edanoue.Logging;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
// For this script (Not Required)

// For this script (Not Required)

namespace Examples
{
    // Required Here

    public class BasicLogging
    {
        [Test]
        public void HelloWorld()
        {
            // Disable test-failing feature when logged above severity "Error".
            LogAssert.ignoreFailingMessages = true;

            Logging.Debug("Debug message");
            Logging.Info("Info message");
            Logging.Warning("Warning message");
            Logging.Error("Error message");
            Logging.Critical("Fatal message");
            Logging.Log(123, "Custom Level Message");
        }

        [Test]
        public void WithUnityObject()
        {
            // Same to use Debug.Log(message, context)
            var anyObject = new GameObject();
            Logging.Info("Info message with context", anyObject);
        }

        [Test]
        public void SetLogLevel()
        {
            // Disable test-failing feature when logged above severity "Error".
            LogAssert.ignoreFailingMessages = true;

            // Set root logger level 
            Logging.SetLevel(LogLevel.Warning);

            Logging.Debug("Verbose message"); // Ignored
            Logging.Info("Info message"); // Ignored
            Logging.Warning("Warning message");
            Logging.Error("Error message");
            Logging.Critical("Fatal message");

            // Reset
            Logging.SetLevel(LogLevel.Debug);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Set root logger level (for this cases)
            Logging.SetLevel(LogLevel.Debug);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Set root logger level to Default
            Logging.SetLevel(LogLevel.Warning);
        }
    }
}