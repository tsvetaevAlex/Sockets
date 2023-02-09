
namespace simicon.automation.Tests.NonParameterized;

public static class SimpleTests
{
    [OneTimeSetUp, Description("ATG, Attempt to Prepare Environment")]
    public static void PrepareTestEnvironment()
    {
        Logger.Write("Wea are in SimpleTests.OneTimeSetUp.PrepareTestEnvironment", "TraceRoute");
    }


    [SetUp]
    public static void isEnvironmrntPrepared()
    {
        Logger.Write($"we are in [SetUp]; isEnvironmentPrepared: {Globals.isEnvironmentPrepared}.", "TraceRoute");
    }

    [Test]
    public static void TestCase0001_NonParameterized_ATC()
    {
        Logger.Write("has entered into TestCase00001NonParameterized_ATC()", "TraceRoute");
        Logger.Write("Command:ATC; expexted Text COntent:ApCorr; ", "ATC");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATC",
            expectedTextContent: "ApCorr: ",
            TAG: "ATC"
        ));
    }

    [Test]
    public static void TestCase0002_NonParameterized_ATF()

    {
        Logger.Write("has entered into TestCase00002_NonParameterized_ATF()", "TraceRoute");
        Logger.Write("Command: ATF; Expected Text Content: Cam flip; ", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF",
            expectedTextContent: "Cam flip: ",
            TAG: "ATF"
        ));
    }
    [Test]
    public static void TestCase0003_NonParameterized_ATG()
    {
        Logger.Write("has entered into TestCase00003_NonParameterized_ATG()", "TraceRoute");
        Logger.Write("Command: ATG; Expected Text Content: GAIN: 480", "ATG");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATG",
            expectedTextContent: "GAIN: ",
            TAG: "ATG")
        );
    }

    [Test]
    public static void TestCase0004_NonParameterized_ATJ()

    {
        Logger.Write("has entered into TestCase00004_NonParameterized_ATJ()", "TraceRoute");
        Logger.Write("Commans: ATJ; Expected Text Content: Offset: ", "ATJ");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATJ",
            expectedTextContent: "Offset: ",
            TAG: "ATJ"
            )
        );
    }

    [Test]
    public static void TestCase0005_NonParameterized_ATK()

    {
        Logger.Write("has entered into TestCase00005_NonParameterized_ATK()", "TraceRoute");
        Logger.Write("Commnd: ATK; Expected Text Content: Hpoint; ", "ATK");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATk",
            expectedTextContent: "Hpoint: ",
            TAG: "ATk"
        ));
    }
    
    [Test]
    public static void TestCase0006_NonParameterized_ATL()
    {
        Logger.Write("has entered into TestCase0006_NonParameterized_ATL()", "TraceRoute");
        Logger.Write("Commnd: ATL; Expected Text Content: Max exp: ", "ATL");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATL",
            expectedTextContent: "Max exp: ",
            TAG: "ATL"
        ));
    }

    [Test]
    public static void TestCase0007_NonParameterized_ATP()
    {
        Logger.Write("has entered into TestCase00007_NonParameterized_ATP()", "TraceRoute");
        Logger.Write("Commnd: ATP; Expected Text Content: Cur exp: ", "ATP");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATL",
            expectedTextContent: "Cur exp: ",
            TAG: "ATP"
        ));
    }

    [Test]
    public static void TestCase0008_NonParameterized_ATS()
    {
        Logger.Write("has entered into TestCase00008_NonParameterized_ATS()", "TraceRoute");
        Logger.Write("Command: ATS |expectedTextContent SHUT: | ATS", "");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATS",
            expectedTextContent: "SHUT: ",
            TAG: "ATS"
        ));
    }
}
