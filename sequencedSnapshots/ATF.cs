namespace simicon.automation.Tests.Tests.subsequencedSnapshots;

public class ATF : TestRun
{
    [Test]
    public void TestCase00014_ATF0()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase00014";
        log.Route(reportRow.ID);
        reportRow.Command = "ATF=0";
        snapshots.Get(reportRow.Command);
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Cam flip: Off"
            ));
        snapshots.Get(reportRow.Command);
    }

    [Test]
    public void TestCase00015_ATF1()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase00015";
        log.Route(reportRow.ID);
        reportRow.Command = "ATF=1";
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Cam flip: On"
        ));
        snapshots.Get(reportRow.Command);
    }

    [Test]
    public void TestCase00016_ATF2()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase00016";
        log.Route(reportRow.ID);
        reportRow.Command = "ATF=2";
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Cam flip: Vert"
            ));
        snapshots.Get(reportRow.Command);
    }

    [Test]
    public void TestCase00017_ATF3()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase00017";
        log.Route(reportRow.ID);
        reportRow.Command = "ATF=3";
        reportRow.ID = "TestCase00020";
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Cam flip: Mirror"
            ));
        snapshots.Get(reportRow.Command);
    }

    [Test]
    public void TestCase00018_ATF4()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase00018";
        log.Route(reportRow.ID);
        reportRow.Command = "ATF=4";
        reportRow.ID = "TestCase00021";
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Cam flip: Off"
        ));
        snapshots.Get(reportRow.Command);
    }
}