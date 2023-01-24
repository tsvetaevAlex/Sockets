using simicon.automation.Utils;

namespace simicon.automation;

public static class PrepareEnvironment
{

    private static readonly string Host = "192.168.10.102";
    private static readonly string Login = "root";
    private static readonly string Password = "test";
    public static void Run()
    {
        Logger.Write("we are in PrepareEnvironment.Run()", "TraceRoute");

        if (!Globals.isEnvironmentPrepared)
        {
            Logger.InitLogger();
            Logger.Write($"we are in:'if (!Globals.isEnvironmentPrepared) is: '{Globals.isEnvironmentPrepared}'", "TraceRoute");
            ConnectionPointers.InitConnectionPointers(Host, Login, Password);
            try
            {
                Sensorapp.UpdateSensorappProperty();
            }
            catch(Exception ex)
            {
                Logger.Write($"Exception throen at UpdateSensorappProperty; {ex.Message}", "sensorapp");
            }
            try
            {
                Sensorapp.VerifySensorappProperty();
            }
            catch (Exception ex)
            {
                Logger.Write($"Exception throen at VerifySensorappProperty; {ex.Message}", "sensorapp");
            }
            try
            {
                Sensorapp.SensorappRestart();
            }
            catch (Exception ex)
            {
                Logger.Write($"Exception throen at SensorappRestart; {ex.Message}", "sensorapp");
            }
            ConnectionPointers.InitConnectionPointers("192.168.10.102","root","test");
            Logger.Write("Update Environment ready status", "sensorapp");
            Logger.Write("Update Environment ready status", "TraceRoute");
            Globals.isEnvironmentPrepared = true;
        }
        else
        {
            Logger.Write("Environment is in valid state/prepared", "PrepareEnvironment");
            ConnectionPointers.summarize();
        }
    }
}
