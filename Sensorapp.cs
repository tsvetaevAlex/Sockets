using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

public class Sensorapp
{
    private TestSuite suite;
    private Helper _helper;
    public Sensorapp(ConnectionPointers cp)
    {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint Senoslapp  ConnectionPointers constructor");
        _helper = new Helper(cp);
    }

    public void Prepare()
    {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint SEnsorapp Prepare");

        #region update Property
        //TODO: move to here
        Console.WriteLine("-------------------------------[Update SEnsorApp Property in DB]-------------------------------");
        Envelope UpdateQuery = new Envelope(
        testname: "SQL insert or replace",
        request: "sqlite3 /tftpboot/boot/conf/kris.sql3 \"insert or replace into tblSettings (tName,tValue) values ('SENSORAPP_MSENSORATPORT','')\"",
        expectedContent: "",
        vt: VerificationType.None
        );
        _helper.Execute(UpdateQuery);
        Console.WriteLine("-------------------------------[Update SEnsorApp Property in DB Completed]---------------------");

        #endregion

        #region Verify Property
        Console.WriteLine("-------------------------------[Verify SEnsorApp Property in DB]-------------------------------");

        Envelope SelectQuery = new Envelope(
            testname: "CamTest002",
            request: "sqlite3 /tftpboot/boot/conf/kris.sql3 \"select * from tblSettings\" | grep SENSORAPP_MSENSORATPORT",
        expectedContent: "SENSORAPP_MSENSORATPORT|",
            vt: VerificationType.Equal
            );
        _helper.Execute(SelectQuery);
        Console.WriteLine("-------------------------------[Verify SEnsorApp Property in DB Completed]---------------------");
        #endregion

        #region Srvice Restart
        Console.WriteLine("-------------------------------[Restart SEnsorApp Service]-------------------------------");

        Envelope RestartQuery = new Envelope(
        testname: "CamTest004(sv restart /service/sensorapp0)",
        request: "sv restart /service/sensorapp0",
        expectedContent: "ok: run: /service/sensorapp0:",
        vt: VerificationType.Contains
        );
        _helper.Execute(RestartQuery);
        Console.WriteLine("-------------------------------[Restart SEnsorApp Serviceompleted]-----------------------");
        #endregion
    }// end of prepare
}// end of class Sensorapp

