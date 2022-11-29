using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

public static class Sensorapp
{

    
    public static void Prepare()
    {
        #region update Property
        //TODO: move to here
        Envelope UpdateQuery = new Envelope(
        testname: "SQL insert or replace",
        request: "sqlite3 /tftpboot/boot/conf/kris.sql3 \"insert or replace into tblSettings (tName,tValue) values ('SENSORAPP_MSENSORATPORT','')\"",
        expectedContent: "",
        vt: VerificationType.None
        );
        Helper.StringExecute(UpdateQuery.messageToSend);

        #endregion

        #region Verify Property

        Envelope SelectQuery = new Envelope(
            testname: "CamTest002",
            request: "sqlite3 /tftpboot/boot/conf/kris.sql3 \"select * from tblSettings\" | grep SENSORAPP_MSENSORATPORT",
        expectedContent: "SENSORAPP_MSENSORATPORT|",
            vt: VerificationType.Equal
            );
        Helper.StringExecute(SelectQuery.messageToSend);
        #endregion

        #region Srvice Restart
        Envelope RestartQuery = new Envelope(
        testname: "CamTest004(sv restart /service)",
        request: "sv restart /service/sensorapp0",
        expectedContent: "ok: run: /service/sensorapp0:",
        vt: VerificationType.Contains
        );
        Helper.StringExecute(RestartQuery.messageToSend);
        Thread.Sleep(10000);
        #endregion
    }// end of prepare
}// end of class Sensorapp

