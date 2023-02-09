using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation.Tests.parameterized
{
    public static  class ATC
    {

        [Test]
        public static void TestCase0009Parameterized_ATC0()
        {
            Logger.Write("has entered into TestCase0009Parameterized_ATC0()", "TraceRoute");
            Helper.Verify(new RequestDetails(
                inputCommand: "ATC=0",
                expectedTextContent: "ApCorr:  0",
                TAG: "ATC"
                ));
        }
        [Test]
        public static void TestCase0010Parameterized_ATC1()
        {
            Logger.Write("has entered into TestCase0010Parameterized_ATC1()", "TraceRoute");
            Helper.Verify(new RequestDetails(
                inputCommand: "ATC=1",
                expectedTextContent: "ApCorr:  1",
                TAG: "ATC"
                ));
        }
        [Test]
        public static void TestCase00011Parameterized_ATC15()
        {
            Logger.Write("has entered into TestCase0011Parameterized_ATC15()", "TraceRoute");
            Helper.Verify(new RequestDetails(
                inputCommand: "ATC=15",
                expectedTextContent: "ApCorr:  15",
                TAG: "ATC"
                ));
        }
        [Test]
        public static void TestCase0012Parameterized_TC7()
        {
            Logger.Write("has entered into TestCase0012Parameterized_TC7()", "TraceRoute");
            Helper.Verify(new RequestDetails(
                inputCommand: "ATC=7",
                expectedTextContent: "ApCorr:  7",
                TAG: "ATC"
                ));
        }
        [Test]
        public static void TestCase0013Parameterized_ATC_belowZeroby1()
        {
            Logger.Write("has entered into TestCase00013Parameterized_ATC_belowZeroby1()", "TraceRoute");
            Helper.Verify(new RequestDetails(
                inputCommand: "ATC=-1",
                expectedTextContent: "ApCorr:  -1",
                TAG: "ATC"
                ));
        }
        [Test]
        public static void TestCase0014Parameterized_ATC_belowZeroby10()
        {
            Logger.Write("has entered into VTestCase0014Parameterized_ATC_belowZeroby10()", "TraceRoute");
            Helper.Verify(new RequestDetails(
                inputCommand: "ATC=-10",
                expectedTextContent: "ApCorr:  -10",
                TAG: "ATC"
                ));
        }
        [Test]
        public static void TestCase0015Parameterized_ATC__belowZeroby15()
        {
            Logger.Write("has entered intoTestCase0015Parameterized_ATC__belowZeroby15()", "TraceRoute");
            Helper.Verify(new RequestDetails(
                inputCommand: "ATC=-15",
                expectedTextContent: "ApCorr:  -15",
                TAG: "ATC"
                ));
        }
        [Test]
        public static void TestCase0016Parameterized_ATC__belowZeroby16()
        {
            Logger.Write("has entered into VTestCase0016Parameterized_ATC__belowZeroby16()", "TraceRoute");
            Helper.Verify(new RequestDetails(
                inputCommand: "ATC=-16",
                expectedTextContent: "ApCorr:  -15",
                TAG: "ATC"
                ));
        }
    }
}
