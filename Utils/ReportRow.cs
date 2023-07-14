namespace simicon.automation.Utils;

public class ReportRow
{
    public string ID = string.Empty;
    public string TestCase = string.Empty;
    public string Description = string.Empty;
    public string Command = string.Empty;
    public string output = string.Empty;
    public string snapshot = string.Empty;
    public string Conclusion = string.Empty;
    private string reportfile = string.Empty;



    ///return ReportRow to initial state for using in next test case
    ///use ir before start next test Case.
    public void Wipe()
    {
        ID = string.Empty;
        TestCase = string.Empty;
        Description = string.Empty;
        Command = string.Empty;
        output = string.Empty;
        snapshot = string.Empty;
        Conclusion = string.Empty;
        reportfile = string.Empty;
    }
}