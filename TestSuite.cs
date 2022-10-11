using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit;

namespace automation;

public class TestSuite
{
    //[TestCase(TestName = "Test case name")]
    //[Test, Order(1)]

    [TestCase(TestName = "devece connection test"),Order(1)]
    public void ConnectionTest()
    {
        Assert.That(Device.GetConnection(), Is.True);
    }

    [TestCase(TestName = "Update Camera Propety Test"), Order(2)]
    public void UpdateCameraPropetyTest()
    {
        //bool result = UpdateCameraPropetyTest("SENSORAPP_MSENSORATPORT", string.Empty)
        //Assert.IsTrue(result);
    }
    [TestCase(TestName = "Verify Camera Propety Test"), Order(3)]
    {
    //public void VerifyCameraPropetyTest()
    ///
    }

[TestCase(TestName = "SERvice Restart Test"), Order(4)]
    {
    //public void ServiseRestartTest();
    }
}