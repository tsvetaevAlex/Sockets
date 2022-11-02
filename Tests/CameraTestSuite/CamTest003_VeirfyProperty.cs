using automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

/// <summary>
/// Verify CAmera Property after update
/// </summary>
public class CamTest003
{
    public void Run()
    {
        string SelectQuery = $"sqlite3 /tftpboot/boot/conf/kris.sql3 \"select * from tblSettings\" | grep SENSORAPP_MSENSORATPORT";

        Envelope testData = new Envelope(
            testname: "CamTest002",
            request: "sqlite3 /tftpboot/boot/conf/kris.sql3 \"select * from tblSettings\" | grep SENSORAPP_MSENSORATPORT",
            expectedContent: "SENSORAPP_MSENSORATPORT|",
            vt: VerificationType.Equal
            );
        Helper.Execute(testData, _result);
    }
}

