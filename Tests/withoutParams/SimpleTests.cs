using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation.Tests.withoutParams;

public static class SimpleTests
{
    [OneTimeSetUp, Description("parameterizedTests, Attempt to Prepare Environment")]
    public static void PrepareTestEnvironment()
    {
        Logger.Write("Wea are in SimpleTests.OneTimeSetUp.PrepareTestEnvironment", "TraceRoute");
        AutomationPrepareEnvironment.VerifyTestEnvironment();
    }


    [SetUp]
    public static void isEnvironmrntPrepared()
    {
        Logger.Write($"we are in [SetUp]; isEnvironmentPrepared: {Globals.isEnvironmentPrepared}.", "TraceRoute");
    }

    [Test]
    public static void ats01VerifyATG()
    {
        Logger.Write("has entered into VerifyATG()", "TraceRoute");
        Helper.Verify("ATG", "GAIN: ", "ATG");
    }
    public static void ats02VerifyATC()
    {
        Logger.Write("has entered into VerifyATC()", "TraceRoute");
        Helper.Verify("ATC", "ApCorr: ", "ATC");
    }

    [Test]
    public static void ats03VerifyATF()

    {
        Logger.Write("has entered into VerifyATF()", "TraceRoute");
        Helper.Verify("ATF", "Cam flip: ", "ATF");
    }
    [Test]
    public static void ats04VerifyATJ()

    {
        Logger.Write("has entered into VerifyATJ()", "TraceRoute");
        Helper.Verify("ATJ", "Offset: ", "ATJ");
    }

    [Test]
    public static void ats05VerifyATK()

    {
        Logger.Write("has entered into VerifyATK)", "TraceRoute");
        Helper.Verify("ATK", "Hpoint: ", "ATK");
    }


    [Test]
    public static void ats06VerifyATL()

    {
        Logger.Write("has entered into VerifyATL()", "TraceRoute");
        Helper.Verify("ATL", "Max exp: ", "ATL");
    }

    [Test]
    public static void ats07VerifyATP()

    {
        Logger.Write("has entered into VerifyATP()", "TraceRoute");
        Helper.Verify("ATP", "Cur exp: ", "ATP");
    }

    [Test]
    public static void ats08VerifyATS()

    {
        Logger.Write("has entered into VerifyATS()", "TraceRoute");
        Helper.Verify("ATS", "SHUT: ", "ATS");
    }

}
