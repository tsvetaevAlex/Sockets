
namespace simicon.automation.Utils;

public class RequestDetails
{
    public string Command = "";
    public string ExpectedContent = "";
    public string tag = "";
    public bool returnFullTextResponse = false;
    public bool returnFullTextResponseResponse = false;
    public string ImageFilename = "";
    public string _ImageTag = "";

    public RequestDetails
        (string inputCommand, string expectedTextContent, string TAG,
        bool returnFullTextOfContent = false,
         string TargetImageFilename = "",
         string imageTag = "")
    {
        Command = inputCommand;
        ExpectedContent = expectedTextContent;
        tag = TAG;
        returnFullTextResponse = returnFullTextOfContent;
        ImageFilename = TargetImageFilename;
        _ImageTag = imageTag;
    }

}
