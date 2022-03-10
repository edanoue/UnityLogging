using NUnit.Framework; // For this script (Not Required)
using UnityEngine.TestTools; // For this script (Not Required)

using Edanoue.Logging; // Required

public class Example
{
    [Test]
    public void RootLoggerLogging()
    {
        LogAssert.ignoreFailingMessages = true; // For testing

        Logging.Verbose("Verbose message");
        Logging.Info("Info message");
        Logging.Warning("Warning message");
        Logging.Error("Error message");
        Logging.Fatal("Fatal message");
    }

    [Test]
    public void SetLogLevel()
    {
        LogAssert.ignoreFailingMessages = true; // For testing

        Logging.SetGlobalConfig(new()
        {
            Level = LogLevel.Warning
        });

        Logging.Verbose("Verbose message");
        Logging.Info("Info message");
        Logging.Warning("Warning message");
        Logging.Error("Error message");
        Logging.Fatal("Fatal message");

        Logging.SetGlobalConfig(new()
        {
            Level = LogLevel.Verbose
        });
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

        loggerA.SetConfig(new()
        {
            Level = LogLevel.Info
        });

        loggerA.Verbose("ignored"); // Ignored
        loggerA.Info("info A-1");
        loggerB.Verbose("ignored"); // Ignored (use parent config)
        loggerB.Info("info B-1");

        loggerB.SetConfig(new()
        {
            Level = LogLevel.Warning
        });

        loggerA.Verbose("ignored"); // Ignored
        loggerA.Info("info A-2");
        loggerB.Verbose("ignored"); // Ignored (use self config)
        loggerB.Info("ignore"); // Ignored (use self config)
    }
}
