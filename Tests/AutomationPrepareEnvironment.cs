
using simicon.automation.Utils;

namespace simicon.automation.Tests;

public static class AutomationPrepareEnvironment
{
    [Test, Description("Attempt to Prepare Test Environment (Device)")]
    [OneTimeSetUp]
    public static void VerifyTestEnvironment()
    {
        Logger.Write($"we are in '[OneTimeSetUp]', isEnvironmentPrepared: {Globals.isEnvironmentPrepared}", "TraceRoute");
        if (!Globals.isEnvironmentPrepared)
        {
            PrepareEnvironment.Run();
        }
    }
    [OneTimeTearDown]
    public static void TearDown()
    {
        Logger.Write("we are in [OneTimeTearDown]", "TraceRoute");
        Globals.isEnvironmentPrepared = false;
    }
}