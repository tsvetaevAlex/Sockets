
namespace simicon.automation.Tests.withoutParams;

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
    public static void ats01VerifyATC()
    {
        Logger.Write("has entered into VerifyATC()", "TraceRoute");
        Logger.Write("Command:ATC; expexted Text COntent:ApCorr; ", "ATC");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATC",
            expectedTextContent: "ApCorr: ",
            TAG: "ATC"
        ));
    }

    [Test]
    public static void ats02VerifyATF()

    {
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        Logger.Write("Command: ATF; Expected Text Content: Cam flip; ", "ATF");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATF",
            expectedTextContent: "Cam flip: ",
            TAG: "ATF"
        ));
    }
    [Test]
    public static void Ats03VerifyATG()
    {
        Logger.Write("has entered into VerifyATG()", "TraceRoute");
        Logger.Write("Command: ATG; Expected Text Content: GAIN: 480", "ATG");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATG",
            expectedTextContent: "GAIN: ",
            TAG: "ATG")
        );
    }

    [Test]
    public static void ats04VerifyATJ()

    {
        Logger.Write("has entered into VerifyATJ()", "TraceRoute");
        Logger.Write("Commans: ATJ; Expected Text Content: Offset: ", "ATJ");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATJ",
            expectedTextContent: "Offset: ",
            TAG: "ATJ"
            )
        );
    }

    [Test]
    public static void ats05VerifyATK()

    {
        Logger.Write("has entered into VerifyATK)", "TraceRoute");
        Logger.Write("Commnd: ATK; Expected Text Content: Hpoint; ", "ATK");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATk",
            expectedTextContent: "Hpoint: ",
            TAG: "ATk"
        ));
    }
    
    [Test]
    public static void ats06VerifyATL()
    {
        Logger.Write("has entered into VerifyATL()", "TraceRoute");
        Logger.Write("Commnd: ATL; Expected Text Content: Max exp: ", "ATL");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATL",
            expectedTextContent: "Max exp: ",
            TAG: "ATL"
        ));
    }

    [Test]
    public static void ats07VerifyATP()
    {
        Logger.Write("has entered into VerifyATP()", "TraceRoute");
        Logger.Write("Commnd: ATP; Expected Text Content: Cur exp: ", "ATP");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATL",
            expectedTextContent: "Cur exp: ",
            TAG: "ATP"
        ));
    }

    [Test]
    public static void ats08VerifyATS()
    {
        Logger.Write("has entered into VerifyATS()", "TraceRoute");
        Logger.Write("Command: ATS |expectedTextContent SHUT: | ATS", "");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATS",
            expectedTextContent: "SHUT: ",
            TAG: "ATS"
        ));
    }
}
