using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using simicon.automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

    public class CamTest007
    {

        public void Run()
        {
            Envelope message = new Envelope(
                testname: "CamTest_007_picocom",
                request: "picocom -b 115200 /dev/tts/sdi0_camera",
                expectedContent: "/dev/tts/camera",
                vt: VerificationType.Contains
                );

        Helper.Execute(message, _result);
        }// end of Run
    }// end of class

