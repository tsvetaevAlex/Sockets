using simicon.automation.Tests;

namespace simicon.automation;

public class Sensorapp : TestRun
{

    public void GetReady()
    {
        Response.Wipe();
        remoteConsole.ExecuteBashCommand("cd /Bash &&./prepareSensorapp.sh");
        log.Sensorapp($"Prepare Sensoeapp to work with automated testcaes;");
        log.Sensorapp($"Prepare Sensoeapp sh script completed with exit code: {Response.ExitCode};");
        if (Response.ExitCode == 0)
        {
            if (Response.output.Contains("ok: run: /service/sensorapp0:"))
            {
                log.Sensorapp("successfully completed.");
                log.Sensorapp("SEnsorapp ready");
            }
        }
        else
        {
            log.Failure("here will be implementer try count logic to complete sensorapp preparation");
            Assert.Fail("try vount login not implrnterd yet here");
        }
    }
    //public void UpdateSensorappProperty()
    //{

    //    log.Route("=====>Sensorapp<===== UpdateSensorappProperty");

    //    //script line://cd /tftpboot/boot &&./prepareSensorapp.sh

    //    string SensorappUdateRequest =
    //    "sqlite3 /tftpboot/boot/conf/kris.sql3 \"insert or replace into tblSettings (tName,tValue) values ('SENSORAPP_MSENSORATPORT','')\"";

    //    try
    //    {
    //        log.Sensorapp("attemptto update semsorapp property SENSORAPP_MSENSORATPORT to empty value.");
    //        RemoteConsole.SendBashCommand("cd /tftpboot/boot/");
    //        Response resp = RemoteConsole.GetResponseSSH(SensorappUdateRequest);
    //        log.Debug($"SensorappUdateRequest exit code: {resp.ExitCode}");
    //        log.Debug($"SensorappUdateRequest output: {resp.output}");
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Failure($"Sensorapp.UpdateSensorappPropertycodeline 26 exception:{ex.ToString}");
    //    }
    //}

    //public void SensorappRestart()
    //{

    //log.Route("has entered into SensorappRestert");

    //#region Srvice Restart
    //string RestartQuery = "sv restart /service/sensorapp0";
    //string expectedContent = "ok: run: /service/sensorapp0:";

    //RemoteConsole.Send("Sensorapp.SensorappRestart: ", RestartQuery, expectedContent);

    ////TODO: processing sv restart sbed\ wait receive
    ////_dDeviceSSHSocket.Send(ConnectionPointers.DeviceSshSocket, SelectRequest, VerifyexpectedContent, VerificationType.Contains);
    //#endregion
    //}


}// end of class Sensorapp

