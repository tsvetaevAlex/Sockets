using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

public class VerificationRecord
{


    public string AT = "";
    public int ControlLine = 0;
    public string ExpecteContent = "";

    public VerificationRecord()
    {
        AT = "";
        ControlLine = 0;
        ExpecteContent = "";
    }

    public VerificationRecord(string at, int ln, string ec)
    {
        AT = at;
        ControlLine = ln;
        ExpecteContent = ec;
    }
}
