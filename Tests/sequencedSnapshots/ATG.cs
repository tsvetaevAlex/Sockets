namespace simicon.automation.Tests.Tests.subsequencedsnapshotss;

public class ATG : TestRun
{
    [Test]
    public void TestCase00017_ATG100()
    {
        reportRow.Wipe();
        reportRow.Description = $"get snapsjot with {reportRow.Command}";
        reportRow.ID = "TestCase000";
        log.Route(reportRow.ID);
        sharedTests.TestCase_ATR();
        sharedTests.TestCase_SetATA0();
        reportRow.Command = "ATG=100";
        snapshots.Get(reportRow.Command);
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "GAIN: "
            ));
        snapshots.Get(reportRow.Command);
    }

    [Test]
    public void TestCase00018_ATG550()
    {
        reportRow.Wipe();
        reportRow.Description = $"get snapsjot with {reportRow.Command}";
        reportRow.ID = "TestCase00018";
        log.Route(reportRow.ID);
        reportRow.Command = "ATG=550";
        snapshots.Get(reportRow.Command);
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "GAIN: "
            ));
        snapshots.Get(reportRow.Command);
    }
    [Test]
    public void TestCase00028_ATG950()
    {
        reportRow.Wipe();
        reportRow.Description = $"get snapsjot with {reportRow.Command}";
        reportRow.ID = "TestCase00028";
        log.Route(reportRow.ID);
        reportRow.Command = "ATG=950";
        snapshots.Get(reportRow.Command);
        helper.Verify(new RequestDetails(
        inputCommand: reportRow.Command,
        expectedTextContent: "GAIN: "
            ));
        snapshots.Get(reportRow.Command);
    }
    [Test]
    public void TestCase00019_ATG850()
    {
        reportRow.Wipe();
        reportRow.Description = $"get snapsjot with {reportRow.Command}";
        reportRow.ID = "TestCase00019";
        log.Route(reportRow.ID);
        reportRow.Command = "ATG=850";
        snapshots.Get(reportRow.Command);
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "GAIN: "
            ));
        snapshots.Get(reportRow.Command);
    }
    [Test]
    public void TestCase00030_ATG1250()
    {
        reportRow.Wipe();
        reportRow.Description = $"get snapsjot with {reportRow.Command}";
        reportRow.ID = "TestCase00030";
        log.Route(reportRow.ID);
        reportRow.Command = "ATG=1250";
        snapshots.Get(reportRow.Command);
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "GAIN: "
            ));
        snapshots.Get(reportRow.Command);
    }
    [Test]
    public void TestCase00031_ATG2000()
    {
        reportRow.Wipe();
        reportRow.Description = $"get snapsjot with {reportRow.Command}";
        reportRow.ID = "TestCase00031";
        log.Route(reportRow.ID);
        reportRow.Command = "ATG=2000";
        snapshots.Get(reportRow.Command);
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "GAIN: "
            ));
        snapshots.Get(reportRow.Command);
    }
}
