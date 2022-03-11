using NUnit.Framework; // For this script (Not Required)
using UnityEngine.TestTools; // For this script (Not Required)

using Edanoue.Logging; // Required

public class Example
{
    [Test]
    public void BasicLogging()
    {
        LogAssert.ignoreFailingMessages = true; // For testing
        // Set root logger level (for this test)
        Logging.SetLevel(LogLevel.Debug);

        Logging.Debug("Debug message");
        Logging.Info("Info message");
        Logging.Warning("Warning message");
        Logging.Error("Error message");
        Logging.Critical("Fatal message");
        // Custom Log Level
        // Pre-defined value see: LogLevel.cs
        Logging.Log(123, "Custom Level Message");

        // Set to root logger level default
        Logging.SetLevel(LogLevel.Warning);
    }

    [Test]
    public void SetLogLevel()
    {
        LogAssert.ignoreFailingMessages = true; // For testing

        // Set root logger level (for this test)
        Logging.SetLevel(LogLevel.Warning);

        Logging.Debug("Verbose message"); // Ignored
        Logging.Info("Info message"); // Ignored
        Logging.Warning("Warning message");
        Logging.Error("Error message");
        Logging.Critical("Fatal message");

        // Reset
        Logging.SetLevel(LogLevel.Warning);
    }

    [Test]
    public void CustomLoggerLogging()
    {
        // Set root logger level (for this test)
        Logging.SetLevel(LogLevel.Debug);

        // Get logger "A"
        var loggerA = Logging.GetLogger("A");
        loggerA.Info("Info message A");
        loggerA.Info("Info message A");

        // Get logger "B" child of "A"
        var loggerB = Logging.GetLogger("A.B");
        loggerB.Info("Info message B");

        // Reset root logger level
        Logging.SetLevel(LogLevel.Warning);
    }

    [Test]
    public void LevelInheritanceBehaviour()
    {
        var loggerA = Logging.GetLogger("A");
        var loggerB = Logging.GetLogger("A.B");

        // Set logger A level
        loggerA.SetLevel(LogLevel.Info);

        loggerA.Debug("ignored"); // Ignored
        loggerA.Info("info A-1");
        loggerB.Debug("ignored"); // Ignored (use parent config)
        loggerB.Info("info B-1");

        // Set logger B level
        loggerB.SetLevel(LogLevel.Warning);

        loggerA.Debug("ignored"); // Ignored
        loggerA.Info("info A-2");
        loggerB.Debug("ignored"); // Ignored (use self config)
        loggerB.Info("ignore"); // Ignored (use self config)

        // define new logger "Foo.Bar"
        // never exist "Foo" logger, then uses root level
        var loggerFooBar = Logging.GetLogger("Foo.Bar");

        loggerFooBar.Info("ignored");
        loggerFooBar.Warning("warning Foo.Bar (use root level)");

        // define new logger "Foo" and set level
        // pre-defined children "Foo.Bar" logger uses "Foo" level
        var loggerFoo = Logging.GetLogger("Foo");
        loggerFoo.SetLevel(LogLevel.Info);

        loggerFooBar.Info("info Foo.Bar (use Foo level)");
        loggerFooBar.Warning("warning Foo.Bar (use Foo level)");

    }
}
