namespace simicon.automation.Tests;

/// <summary>
/// these TestCases can be used as part of ccombined test.
/// </summary>
public class SharedTests : TestRun
{


    [Test, Description("Set ATA property to 1")]
    public void TestCase_SetATA1()
    {
        reportRow.Wipe();
        Response.Wipe();
        reportRow.ID = "TestCase0010";
        reportRow.Command = "ATA=1";
        string expectedCOntent = "AGC: (1), ON";

        reportRow.TestCase = "TestCase_SetATA1";
        log.Route($"{reportRow.TestCase}");
        reportRow.Description = "Set ATA value to 1 will be used as a part of combined TestCases an trigger for ATA poperty";

        //AGC: (1), ON
        log.Route(reportRow.TestCase);
        helper.Verify(new RequestDetails(
            inputCommand: reportRow.Command,
            expectedTextContent: expectedCOntent
        ));
    }

    [Test, Description("Set ATA property to 0")]
    public void TestCase_SetATA0()
    {
        connections.AuthorizePicocom();
        reportRow.Wipe();
        Response.Wipe();
        reportRow.ID = "TestCase0011";
        reportRow.Command = "ATA=0";
        string expectedCOntent = "AGC: (0), OFF";
        reportRow.TestCase = "TestCase0010_SetATA0";
        log.Route($"{reportRow.TestCase}");
        reportRow.Description = "Set ATA value to 0 will be used as a part of combined TestCases an trigger for ATA poperty";

        //AGC: (1), ON
        log.Route(reportRow.TestCase);
        var camera = CameraConsole;
        if (camera is not null)
        {
            camera.WriteLine(reportRow.Command);
        }
        while (camera.DataAvailable)
        {
            string respLine = camera.ReadLine();
            if (respLine.Contains(expectedCOntent))
            {
                log.TestCase("shared dunction set ATA=0 completed successfully.");
                break;
            }
        }

    }

    [Test, Description("Send ATR to camera console")]
    public void TestCase_ATR()
    {
        log.Route($"{reportRow.TestCase}");
        reportRow.Wipe();
        Response.Wipe();
        reportRow.ID = "TestCase0012";
        reportRow.Command = "ATR";
        reportRow.TestCase = "TestCase_ATR";
        helper.Send(reportRow.Command);
    }


}
