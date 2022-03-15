using Edanoue.Logging;
using NUnit.Framework;

public class ManagerUnitTest
{
    [Test]
    public void LoggerIsSameInstance()
    {
        // Get logger "A.B.C"
        var loggerAbc0 = Logging.GetLogger("A.B.C");
        var loggerAbc1 = Logging.GetLogger("A.B.C");

        Assert.That(loggerAbc0, Is.EqualTo(loggerAbc1));
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