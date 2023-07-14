
namespace simicon.automation.Utils;

public class RequestDetails
{
    public string Command = "";
    public string ExpectedContent = "";
    

    public RequestDetails
        (string inputCommand, string expectedTextContent)
    {
        Command = inputCommand;
        ExpectedContent = expectedTextContent;
        
    }

}
