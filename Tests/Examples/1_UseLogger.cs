using NUnit.Framework; // For this script (Not Required)
using UnityEngine.TestTools; // For this script (Not Required)

namespace Examples
{
    using Edanoue.Logging; // Required Here

    public class UseLogger
    {
        [Test]
        public void CustomLoggerLogging()
        {
            // Get logger "A"
            var loggerA = Logging.GetLogger("A");
            loggerA.Info("Info message A");

            // Get logger "B" child of "A"
            var loggerB = Logging.GetLogger("A.B");
            loggerB.Info("Info message B");
        }

        [Test]
        public void LevelInheritanceBehaviour01()
        {
            // define two familly loggers
            var loggerA = Logging.GetLogger("A");
            var loggerB = Logging.GetLogger("A.B");

            // Set logger A level to Debug
            loggerA.SetLevel(LogLevel.Debug);

            // Both logs emitted
            loggerA.Debug("A debug 1");
            loggerB.Debug("B debug 1");

            // Set logger A level to Info
            loggerA.SetLevel(LogLevel.Info);

            // Both logs ignored
            loggerA.Debug("A debug 2"); // Ignored
            loggerB.Debug("B debug 2"); // Ignored

            // Set logger B level to warning
            loggerB.SetLevel(LogLevel.Warning);

            loggerA.Info("A info 3");
            loggerB.Info("B info 3"); // Ignored
        }

        [Test]
        public void LevelInheritanceBehaviour02()
        {
            // Set root logger level
            Logging.SetLevel(LogLevel.Warning);

            // define new logger "Foo.Bar"
            // never exist "Foo" logger, then uses root level
            var loggerFooBar = Logging.GetLogger("Foo.Bar");

            loggerFooBar.Info("ignored");
            loggerFooBar.Warning("warning Foo.Bar (use root level)");

            // define new logger "Foo" and set level
            // pre-defined children "Foo.Bar" logger uses "Foo" level
            var loggerFoo = Logging.GetLogger("Foo");
            {
                loggerFoo.SetLevel(LogLevel.Info);
            }

            loggerFooBar.Info("info Foo.Bar (use Foo level)");
            loggerFooBar.Warning("warning Foo.Bar (use Foo level)");
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