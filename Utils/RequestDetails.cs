
namespace simicon.automation.Utils;

public class RequestDetails
{
    public string Command = "";
    public string ExpectedContent = "";
    public string tag = "";
    public bool returnFullTextResponse = false;

    public RequestDetails(string inputCommand, string expectedTextContent, string TAG, bool returnFullTextOfContent = false)
    {
        Command= inputCommand;
        ExpectedContent = expectedTextContent;
        tag = TAG;
        returnFullTextResponse= returnFullTextOfContent;
    }

}
