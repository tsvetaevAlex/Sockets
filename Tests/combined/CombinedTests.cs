using simicon.automation.Tests;

namespace simicon.automation.Tests;

public class CombinedTests : TestRun
{
    [Test, Description("Verify that aTG changeable When_ATA0.")]
    public void TestCase00019_Combined_VerifyThat_ATG_channable_When_ATA0()
    {
        reportRow.Wipe();
        string testcaseName = "TestCase00019";
        reportRow.ID = testcaseName;
        reportRow.Command = "ATG = 500";
        log.Info($"@DEBUG->  simicon.automation.Tests.combined. {testcaseName};" + NewLine);
        sharedTests.TestCase_SetATA0();
        log.TestCase("\\nTest Case: TestCase00023_Combined_VerifyThat_ATG_channable_When_ATA0");
        helper.Verify(new RequestDetails
        (
            inputCommand: reportRow.Command,
            expectedTextContent: "GAIN: 480"
        ));
    }

    [Test, Description("Verify that ATS nonchangeable When_ATA1.")]
    public void TestCase00020Combined_VerifyThat_ATG_NonChannableWhen_ATA1()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase00020";
        sharedTests.TestCase_SetATA1 ();
        reportRow.Command = "ATS=7";
        log.TestCase("\\nTest Case: TestCase00024_Combined_VerifyThat_ATG_NonChannableWhen_ATA1");
        helper.Verify(new RequestDetails
        (
            inputCommand: reportRow.Command,
            expectedTextContent: "ERR"
        ));
    }

    [Test, Description("Verify that aTG changeable When_ATA1.")]
    public void TestCase00021_Combined_VerifyThat_ATG_NotChannableWhen_ATA1()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase00021";
        reportRow.Command = "ATG = 500";
        sharedTests.TestCase_SetATA1();
        log.TestCase("\\nTest Case: TestCase00025_Combined_VerifyThat_ATG_NotChannableWhen_ATA1");
        log.Route("Test Case: TestCase00025_Combined_VerifyThat_ATG_NotChannableWhen_ATA1");
        helper.Verify(new RequestDetails
        (
            inputCommand: reportRow.Command,
            expectedTextContent: "ERR"
        ));
    }

    [Test, Description("Verify that ATS changeable When_ATA0.")]
    public void TestCase00022_Combined_VerifyThat_ATS_channableWhen_ATA0()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase00022";
        reportRow.Command = "ATS=7";
        sharedTests.TestCase_SetATA0();
        log.TestCase("\\nTest Case: TestCase00026_Combined_VerifyThat_ATS_channableWhen_ATA0");
        helper.Verify(new RequestDetails
        (
            inputCommand: reportRow.Command,
            expectedTextContent: "SHUT: 7"
        ));
    }
    [Test, Description("Verify that ATS changeable When_ATA1.")]
    public void TestCase00023_Combined_VerifyThat_ATS_NotChannableWhen_ATA1()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase00023";
        reportRow.Command = "ATS=7";
        sharedTests.TestCase_SetATA1();
        log.TestCase("\\nTest Case: TestCase00027_Combined_VerifyThat_ATS_NotChannableWhen_ATA1");
        helper.Verify(new RequestDetails
        (
            inputCommand: reportRow.Command,
            expectedTextContent: "SHUT: 7"
        ));
    }


    [Test, Description("Verify that ATL changeable When_ATA0.")]
    public void TestCase00024_Combined_VerifyThat_ATL_channableWhen_ATA0()
    {//Max exp
        reportRow.Wipe();
        reportRow.ID= "TestCase00024";
        sharedTests.TestCase_SetATA1();
        log.TestCase("\\nTest Case: TestCase00028_Combined_VerifyThat_ATL_channableWhen_ATA0");
        helper.Verify(new RequestDetails
        (
            inputCommand: "ATL=5",
            expectedTextContent: "Max exp: "
        ));
    }
    [Test, Description("Verify that ATL changeable When_ATA1.")]
    public void TestCase00025_Combined_VerifyThat_ATL_NotChannableWhen_ATA1()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase00025";
        sharedTests.TestCase_SetATA1();
        log.TestCase("\\nTest Case: TestCase00029_Combined_VerifyThat_ATL_NotChannableWhen_ATA1");
        helper.Verify(new RequestDetails
        (
            inputCommand: "ATL=7",
            expectedTextContent: "ERR"
        ));
    }

}// end of class
