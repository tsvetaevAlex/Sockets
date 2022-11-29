using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation.Tests.AT;

    public static class ATB
    {//night mode

        [Test]
        public static void verifyATB()
        {
            Logger.Write("ATB: start verification of AT command ATB", "ATB");
            TestCase.Run("ATB", "night mode","ATB");
        }


}

