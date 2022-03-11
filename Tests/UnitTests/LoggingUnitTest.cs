#nullable enable

using NUnit.Framework;
using Edanoue.Logging;

using ForTesting;

public class LoggingUnitTest
{
    [Test]
    public void RootLoggerAccessFromGetLogger()
    {
        var root01 = Logging.GetLogger("");
        var root02 = Logging.GetLogger("root");
        var root03 = Logging.GetLogger(null!);

        // three instances is same root logger
        Assert.That(root01, Is.EqualTo(root02));
        Assert.That(root02, Is.EqualTo(root03));
    }

    [Test]
    public void GetLoggerTypeAccessEquability01()
    {
        var logger01 = Logging.GetLogger("LoggingUnitTest");
        var logger02 = Logging.GetLogger<LoggingUnitTest>();

        // two instances is same
        Assert.That(logger01, Is.EqualTo(logger02));
    }

    [Test]
    public void GetLoggerTypeAccessEquability02()
    {
        // Nested type uses FullName
        var logger01 = Logging.GetLogger("ForTesting.NamespaceNestedClass.NestedClass");
        var logger02 = Logging.GetLogger<NamespaceNestedClass.NestedClass>();

        // two instances is same
        Assert.That(logger01, Is.EqualTo(logger02));
    }
}


namespace ForTesting
{
    internal class NamespaceNestedClass
    {
        internal class NestedClass
        {

        }
    }
}
