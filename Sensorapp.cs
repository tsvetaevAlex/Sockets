using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static simicon.automation.Utils.Enums;
using Xamarin.Forms;

namespace simicon.automation;

public static class Sensorapp
{


    public static void UpdateSensorappProperty()
    {

        Logger.Write("has entered into UpdateSensorappProperty", "TraceRoute");
        Logger.Write("has entered into UpdateSensorappProperty", "sensorapp");
        

        string SensorappUdateRequest =
        "sqlite3 /tftpboot/boot/conf/kris.sql3 \"insert or replace into tblSettings (tName,tValue) values ('SENSORAPP_MSENSORATPORT','')\"";


        string SensorappSelectExpectedContent = "";
        SshSocket.Send(SensorappUdateRequest, "");
        SshSocket.Send(SensorappUdateRequest, SensorappSelectExpectedContent);
    }

    public static void VerifySensorappProperty()
    {
    Logger.Write("has entered into VerifySensorappProperty", "TraceRoute");
    Logger.Write("has entered into VerifySensorappProperty", "sensorapp");
    bool isExpcted = false;
    bool isExit0 = false;
        #region Verify value of updated Sensorapp Property
        string SensorappSelectRequest = "sqlite3 /tftpboot/boot/conf/kris.sql3 \"select * from tblSettings\" | grep SENSORAPP_MSENSORATPORT";
    string expectedContent = "SENSORAPP_MSENSORATPORT|";
    SshSocket.Send(SensorappSelectRequest, expectedContent);
    }
    #endregion

    public static void SensorappRestart()
    {

    Logger.Write("has entered into SensorappRestart", "TraceRoute");
    Logger.Write("has entered into SensorappRestert", "sensorapp");

    #region Srvice Restart
    string RestartQuery = "sv restart /service/sensorapp0";
    string expectedContent = "ok: run: /service/sensorapp0:";

    SshSocket.Send(RestartQuery, expectedContent);

    //TODO: processing sv restart sbed\ wait receive
    //_dDeviceSSHSocket.Send(ConnectionPointers.DeviceSshSocket, SelectRequest, VerifyexpectedContent, VerificationType.Contains);
    #endregion
    }


        }// end of class Sensorapp

