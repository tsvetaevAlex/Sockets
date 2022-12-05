using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simicon.automation;

namespace simicon.automation.Tests.AT;

internal class ATG
{
    [Test]
    public static void verifyATG()
    {
        Logger.Write("ATG: start verification of AT command ATG", "ATG");
        TestCase.Run("ATG", "GAIN");
    }
}

