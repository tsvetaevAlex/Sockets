namespace simicon.automation.Tests.Utils;

public class HeapReturn
{

    public bool BoolResult;
    public int IntResult;
    public string StringResult;
    public bool VoidResult;
    public bool NullResult;


    HeapReturn()
    {
    BoolResult = false;
    IntResult = 0;
    StringResult = string.Empty;
    VoidResult = false;
    NullResult = false;
    }

    public void Wipe()
    {
        BoolResult = false;
        IntResult = 0;
        StringResult = string.Empty;
        VoidResult = false;
        NullResult = false;
    }


}
