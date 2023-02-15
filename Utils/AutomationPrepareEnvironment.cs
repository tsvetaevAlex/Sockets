namespace simicon.automation.Utils;

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
            Snapshot.Get("Default");
        }
    }
    [OneTimeTearDown]
    public static void TearDown()
    {
        Logger.Write("we are in [OneTimeTearDown]", "TraceRoute");
        ConnectionPointers.Dispose();
    }
}