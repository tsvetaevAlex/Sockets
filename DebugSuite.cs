using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using automation;

namespace simicon.automation;

public class DebugSuite
{

    [Test, Order(1)]
    public static void PrepareEvironment()
    {
        Console.WriteLine("\n<============================[ PrepareEvironment Srarted ]============================");
        ConnectionPointers.InitConnectionPointers("192.168.10.102", "root", "test");
        Sensorapp.Prepare();
    }
}