
namespace simicon.automation.Tests;

public class SimpleTests : TestRun
{ 
    private DataHeap dataHeap = new DataHeap();
    private readonly string logPrefix = "simicon.automation.Tests.Parameterized, COde line: ";


    [Test, Description("get GetFirmwareVersion for report teble title.")]
    public void TestCase0001_ATV_GetFirmwareVersion()
    {
        // ATV == "Imx265-R v0.0.3 T29, hw 1.0 (c)23
        //StartsWith("Imx") 

        reportRow.Wipe();
        reportRow.ID = "TestCase0001";
        reportRow.Command = "ATV";
        reportRow.TestCase = "TestCase0001_ATV_GetFirmwareVersion";
        log.Route($"{reportRow.TestCase}");
        reportRow.Description = "Verify that ATV command return firmware version and harware configuration";
        log.TestCase($"{logPrefix}23 {reportRow.TestCase}");
        log.TestCase($"Command:ATC; expexted Content should StartsWith 'imx' prefix;");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Imx:"
        ));
    }
    public void TestCase0002_AT2_GetSensorType()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase0002";
        reportRow.Command = "AT2";
        reportRow.Description = "Verify that AT2 command return sensor type expected:b/w or color.";
        reportRow.TestCase = "TestCase0002_AT2_GetSensorType";
        string cameraType = helper.ClarifySensorType();
        if ((cameraType == "BW") || (cameraType == "Color"))
        {
            dataHeap.CameraType = cameraType;
        }
        Assert.That(((cameraType == "BW") || (cameraType == "Color")), Is.True);
    }
    [Test]
    public void TestCase0003_ATC()
    {
        log.WhereLogs();
        reportRow.Command = "ATC";
        reportRow.ID= "TestCase0003";
        log.TestCase($"{logPrefix}> TestCase0001_ATC()");
        log.TestCase($"Command:TestSuite3_ATC; expexted Content(Key words):'ApCorr';");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "ApCorr:"
        ));
    }

    [Test]
    public void TestCase0004_ATF()
    {
        reportRow.Wipe();
        reportRow.ID = "TestCase0004";
        reportRow.Command = "ATF";
        reportRow.Description = "Verify that ATF command current state of Flip property";
        log.TestCase($"{logPrefix}: TestCase0003_ATF");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Cam flip: "
        ));
    }

    [Test]
    public void TestCase0005_ATG()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase0005";
        reportRow.Command = "ATG";
        reportRow.Description = "Verify that ATG command current state of Gain property";
        log.TestCase($"{logPrefix}: TestCase0004_ATG");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "GAIN: "
            ));
    }

    [Test]
    public void TestCase0006_ATJ()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase0006";
        reportRow.Command = "ATJ";

        log.TestCase($"{logPrefix}: TestCase0005_ATJ" +
            $"{logPrefix}.55 Command: ATJ; Expected text Keyword: Offset: ");
        reportRow.Description = "Verify that ATJ command current state of brightness property";
        reportRow.ID= "TestCase000z4";
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Offset: "
            )
        );
    }

    [Test]
    public void TestCase0007_ATK()

    {
        reportRow.Wipe();
        reportRow.ID= "TestCase0007";
        reportRow.Command = "ATK";
        reportRow.Description = "Verify that ATJ command current state of Average histogram level property";
        log.TestCase($"{logPrefix}: TestCase0006_ATK");
        reportRow.ID= "TestCase0005";
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Hpoint: "
        ));
    }

    [Test]
    public void TestCase0008_ATL()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase0008";
        reportRow.Command = "ATL";
        log.TestCase($"{logPrefix}: TestCase0007_ATL");
        reportRow.Description = "Verify that ATJ command current state of max exposure for AGC property";
        log.Route("Test Case: TestCase0006_ATL");  
        log.TestCase("Commnd: ATL; Expected text Keyword: Max exp: ");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Max exp: "
        ));
    }

    [Test]
    public void TestCase0009_ATP()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase0009";
        reportRow.Description = "Verify that ATP command current state of exposure in uSe property";
        reportRow.Command = "ATP";
        log.TestCase($"{logPrefix}: TestCase0008_ATP");
        log.Route("Test Case: TestCase0007_ATP");
        log.TestCase("Commnd: ATP; Expected text Keyword: Cur exp: ");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "Cur exp: "
        ));
    }

    [Test]
    public void TestCase0010_ATS()
    {
        reportRow.Wipe();
        reportRow.ID= "TestCase0010";
        reportRow.Command = "ATS";
        reportRow.Description = "Verify that ATS command current state of exposure property";
        log.TestCase($"\\n{logPrefix}.134 Test Case: TestCase0008_ATS");
        log.Route("Test Case: TestCase0009_ATS");
        log.TestCase($"\\n{logPrefix}.Command: ATS Expected text Keyword'SHUT: '");
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: "SHUT: "
        ));
    }

}