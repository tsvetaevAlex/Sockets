using automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;
public class CamTest004
    {


    public void Run()
    {
        Envelope testData = new Envelope(
            testname: "CamTest004(sv restart /service)",
            request: "sv restart /service/sensorapp0",
            expectedContent: "ok: run: /service/sensorapp0:",
            vt: VerificationType.Contains
            );
        Helper.Execute(testData, _result);
    }

}

