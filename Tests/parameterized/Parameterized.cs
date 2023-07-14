
namespace simicon.automation.Tests;

public class Parameterized : TestRun
{
    [Test]
    public void TestCase00009minATK()
    {
        reportRow.ID= "TestCase00022";
        reportRow.TestCase= "TestCase00022sParameterized_minATK()";
        helper.Verify(new RequestDetails(
            inputCommand: "ATK=20",
            expectedTextContent: "Hpoint: 20"
        ));
        snapshots.Get(reportRow.Command);
    }

    [Test, Description("Veify image when ATK properyt has maximum value (image should be bright)")]
    public void TestCase00010maxATK()
    {
        reportRow.ID= "TestCase00024";
        log.TestCase("TestCase00022maxATK()");
        helper.Verify(new RequestDetails(
            inputCommand: "ATK=150",
            expectedTextContent: "Hpoint: 20"
        ));
        snapshots.Get(reportRow.Command);
    }
    [Test]
    public void TestCase0005_1__ATK1_BelowLowBorder()
    {
        reportRow.ID= "TestCase0005_1";
        log.TestCase($"TestCase0005_1__ATK1_BelowLowBorder");
        log.Info("Recommented Range [70..250]");
        log.Route("Test Case: TestCase0005_1__ATK1_BelowLowBorder");
        log.TestCase("Commnd: ATK; Expected text Keyword: Too low level,");
        helper.Verify(new RequestDetails(
            inputCommand: "ATk1=50",
            expectedTextContent: "Too low level,"
        ));
        snapshots.Get(reportRow.Command);
    }
}

