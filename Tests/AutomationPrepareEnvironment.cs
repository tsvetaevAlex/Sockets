
namespace simicon.automation.Tests
{
    public static class AutomationPrepareEnvironment
    {
        [Test, Description("Attempt to Prepare Environment")]
        public static void VerifyTestEnvironment()
        {
            if (!Globals.isEnvironmentPrepared)
            {
                PrepareEnvironment.Run();
            }
        }

    }
}
