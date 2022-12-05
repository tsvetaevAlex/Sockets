using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation
{
    public static class OutputVerification
    {

        public static void Verify(string command, string output, string expectedContent)
        {
            Logger.Write($"OutputVerification.Verify", "OutputVerification");

            Logger.Write($"OUTPUT: {output}", "OutputVerification");

            bool isKeyWordReceived = false;
            bool IsOkReceived = false;
            bool isErrReceived = false;
            string _output = "";
                if (_output.Contains(expectedContent))
                {
                    isKeyWordReceived = true;
                }
                else
                {
                    Assert.Fail("Expected content has not been received.");
                }

                if (_output.Contains("OK("))
                {
                    IsOkReceived = true;
                }

                if (_output.Contains("ERR"))
                {
                    isErrReceived = true;
                    Assert.Fail("OutputVerification FaiLed due to ERR token received.");
                }



                if (isKeyWordReceived && IsOkReceived)
                {
                    string summaryMessage = $"AT command: {command}, Verification successfully passed. Expected content and OK #tag received";
                    Logger.Write(summaryMessage, "Verify");
                    Assert.Pass(summaryMessage);
                }
                //Assert.Fail("OutputVerification FaiLed" );


        }
    }
}
