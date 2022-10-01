using NUnit.Framework;
using NUnit.Framework.Internal;
using CameraTests;

namespace CameraTests;

public class ct001ZoomTest : CameraTestSuite
    {

    public ct001ZoomTest() { }


        public void Run(int zoomvalue)
        {
        string request = $" echo - e \'PROTOCOL 1 \\nSETZOOMPOS {zoomvalue}' | nc 0 {CameraPort}";
        string response = targetDevice.SendMessage(request);
            ReportRecord reportRow = new ReportRecord();
            reportRow.testName = this.GetType().Name;
            reportRow.testResult = response.Contains($" CURRENT={zoomvalue} ");
            Assert.IsTrue(response.Contains($" CURRENT={zoomvalue} "));
        }
    }
