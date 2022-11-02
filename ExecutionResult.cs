using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

    public class ExecutionResult
    {

        public string output;
        public int ExitStatus;
        public ExecutionResult()
        {
            output = "";
            ExitStatus = 0;
        }

    }

