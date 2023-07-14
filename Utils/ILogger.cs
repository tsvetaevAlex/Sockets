namespace simicon.automation.Tests.Utils
{
    #region Logger Interface
    public interface ILogger
    {
        void Route(string message);
        void Info(string message);
        void Failure(string message);
        void Success(string message);
        void Sensorapp(string message);
        void SnapShot(string message);
        void Warning(string message);
        void TestCase(string message);
        void Networtk(string message);
    }
    #endregion
}
