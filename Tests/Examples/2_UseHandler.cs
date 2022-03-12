using NUnit.Framework; // For this script (Not Required)
using UnityEngine.TestTools; // For this script (Not Required)

namespace Examples
{
    using Edanoue.Logging; // Required Here

    public class UseHandler
    {
        [Test]
        public void SetupHandlerManually()
        {
            // Disable test-failing feature when logged above severity "Error".
            LogAssert.ignoreFailingMessages = true;

            // Create logger
            var logger = Logging.GetLogger<UseHandler>(nameof(SetupHandlerManually));
            {
                // Set Logger Level
                logger.SetLevel(LogLevel.Info);
            }

            // Emit log severity info
            logger.Info("1 Info message");

            // Create Unity Console Handler
            var handler = new Edanoue.Logging.Internal.UnityConsoleHandler();
            {
                // Set Handler level to Error
                handler.SetLevel(LogLevel.Error);
            }

            // then Add handler to logger
            logger.AddHandler(handler);

            // Emit info log but ignored;
            logger.Info("2 Info message"); // ignored;

            // "Error" log is emitted beacause above logger and handler level.
            logger.Error("2 error message"); // emitted
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