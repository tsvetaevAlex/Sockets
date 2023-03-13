using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simicon.automation.Utils;

namespace simicon.automation.Tests.parameterized;

public static class parameterizedTests
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

    [Test, Description("Set ATA property to 0")]
    public static void TestCase0022_Parameterized_SetATA0()
    {
        //AGC: (0), OFF
        Logger.Write("has entered into TestCase00001NonParameterized_ATC()", "TraceRoute");
        Logger.Write("Command:ATC; expexted Text COntent:ApCorr; ", "ATC");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATA=0",
            expectedTextContent: "AGC: (0), OFF",
            TAG: "ATC"
        ));
    }

    public static void TestCase00022_Parameterized_minATK()
    {
        Logger.Write("has entered into TestCase00022sParameterized_minATK()", "TraceRoute");
        Logger.Write("Command:ATC; expexted Text COntent:ApCorr; ", "ATC");
        Snapshot.Get("00022_Before_atk20", "ATK");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATK=20",
            expectedTextContent: "Hpoint: 20",
            TAG: "ATK",
            TargetImageFilename: "00022_After_atk20",
            imageTag: "ATK"
        ));
    }
    [Test, Description("Veify image when ATK properyt has maximum value (image should be bright)")]
    public static void TestCase00024_Parameterized_maxATK()
    {
        Logger.Write("has entered into TestCase00022_Parameterized_maxATK()", "TraceRoute");
        Logger.Write("Command:ATC; expexted Text COntent:ApCorr; ", "ATC");
        Snapshot.Get("00022_Before_atk150", "ATK");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATK=150",
            expectedTextContent: "Hpoint: 20",
            TAG: "ATK",
            TargetImageFilename: "00022_After_atk150",
            imageTag: "ATK"
        ));
    }
    [Test, Description("Set ATA property to 1")]
    public static void TestCase0023_Parameterized_SetATA1()
    {
        //AGC: (1), ON
        Logger.Write("has entered into TestCase00001NonParameterized_ATC()", "TraceRoute");
        Logger.Write("Command:ATC; expexted Text COntent:ApCorr; ", "ATC");
        Helper.Verify(new RequestDetails(
            inputCommand: "ATA=1",
            expectedTextContent: "AGC: (1), ON",
            TAG: "ATC"
        ));

    }
}

