using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation.Utils;

public static class Enums
{

    public enum VerificationType
    {
        Equal,
        Contains,
        None
    }

    public enum requestProcessingLogic
    {
        Device,
        Camera
    }
    public enum SensorType
    {
        Color,
        BW,
        Undefined
    }

}
