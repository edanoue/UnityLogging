using NUnit.Framework;
using Edanoue.Logging;

public class ManagerUnitTest
{
    [Test]
    public void LoggerIsSameInstance()
    {
        // Get logger "A.B.C"
        var loggerABC_0 = Logging.GetLogger("A.B.C");
        var loggerABC_1 = Logging.GetLogger("A.B.C");

        Assert.That(loggerABC_0, Is.EqualTo(loggerABC_1));
    }

    [Test]
    public void PlaceHolderReplacement()
    {
        // Get logger "ManagerUnitTest.A.B.C"
        Logging.GetLogger("ManagerUnitTest.A.B.C");

        // Get logger "ManagerUnitTest.A
        Logging.GetLogger("ManagerUnitTest.A");

        // Get logger "ManagerUnitTest.A.B
        Logging.GetLogger("ManagerUnitTest.A.B");

    }
}
