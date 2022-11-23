using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using automation.Tests.ATSZ;
using NUnit.Framework.Internal;

namespace automation.Tests;

public class CameraTest
{
    public CameraTest() { }

    public void TestSuite()
    {
        ATszSingleRun.Run(150);
        ATszSingleRun.Run(150);
        ATszSingleRun.Run(-200);
        ATszSingleRun.Run(300);
        ATszSingleRun.Run(-100);
        ATszSingleRun.Run(150);
    }

}
