
using NUnit.Framework;

namespace simicon.automation;

public class TestContext
{ 
    public string Command = "";
    public string ExpectedTextContent = "";
    public string tag = "";

    public TestContext(string cCommand, string cExpectedContent, string cTag)
    {
        Command = cCommand;
        ExpectedTextContent = cExpectedContent;
        tag = cTag;
    }

    private void ReviewExpectedCOntent()
    {

    }
    private void takingDecision()
    {

    }





}
