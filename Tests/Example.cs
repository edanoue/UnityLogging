using NUnit.Framework; // For this script (Not Required)
using UnityEngine.TestTools; // For this script (Not Required)

using Edanoue.Logging; // Required

public class Example
{
    [Test]
    public void BasicLogging()
    {
        LogAssert.ignoreFailingMessages = true; // For testing

        Logging.Debug("Verbose message");
        Logging.Info("Info message");
        Logging.Warning("Warning message");
        Logging.Error("Error message");
        Logging.Critical("Fatal message");
    }

    [Test]
    public void SetLogLevel()
    {
        LogAssert.ignoreFailingMessages = true; // For testing

        // Set root logger level
        Logging.SetLevel(LogLevel.Warning);

        Logging.Debug("Verbose message"); // Ignored
        Logging.Info("Info message"); // Ignored
        Logging.Warning("Warning message");
        Logging.Error("Error message");
        Logging.Critical("Fatal message");

        // Reset
        Logging.SetLevel(LogLevel.NotSet);
    }

    [Test]
    public void CustomLoggerLogging()
    {
        var loggerA = Logging.GetLogger("My Module A");
        loggerA.Info("Info message A");
        loggerA.Info("Info message A");

        var loggerB = loggerA.GetLogger("MyModule B");
        loggerB.Info("Info message B");
    }

    [Test]
    public void ConfigOverriding()
    {
        var loggerA = Logging.GetLogger("A");
        var loggerB = loggerA.GetLogger("B");

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
    }
}
