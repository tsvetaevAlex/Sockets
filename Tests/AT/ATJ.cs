using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simicon.automation;

namespace simicon.automation.Tests.AT;

internal class ATJ
{

    [Test]
    public static void verifyATJ()
    {
        Logger.Write("ATJ: start verification of AT command ATJ", "Camera.AT: ");
        TestCase.Run("ATJ", "Offset: ");
    }

}