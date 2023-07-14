namespace simicon.automation.Tests.Tests.subsequencedSnapshots;

public class ATC : TestRun
{

    [Test]
    public void TestCase00010_ATC0()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase00009";
        reportRow.Command = "ATC=0";

        log.Route(reportRow.ID);
        log.TestCase("Test Case: TestCase0009_ATC0");
        log.TestCase("Test Case: TestCase0009_ATC0");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "ApCorr:  0"
            ));
    }
    [Test]
    public void TestCase00011_ATC1()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase0010";
        reportRow.Command = "ATC=1";
        log.TestCase("Test Case: TestCase00010_ATC1");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "ApCorr:  1"
            ));
    }
    [Test]
    public void TestCase00012_ATC15()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase00011";
        reportRow.Command = "ATC=15";
        log.TestCase("Test Case: TestCase00011_ATC15");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "ApCorr:  15"
            ));
    }
    [Test]
    public void TestCase00013_TC7()
    {
        reportRow.Wipe();

        reportRow.ID = "TestCase0012";
        reportRow.Command = "ATC=7";
        log.TestCase("Test Case: TestCase00012_TC7");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "ApCorr:  7"
            ));
    }
    [Test]
    public void TestCase00013_ATC_belowZero()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase0013";
        log.TestCase("\\nTest Case: TestCase0013_ATC_belowZeroby1");
        helper.Verify(new RequestDetails(
            inputCommand: "TestSuite3_ATC=-1",
            expectedTextContent: "ApCorr:  -1"
            ));
    }
}
