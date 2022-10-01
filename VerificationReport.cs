using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraTests;

    public class VerificationReport
    {
        List<ReportRecord> reportRow = new List<ReportRecord>();


        public void addRow(ReportRecord record)
        {
            reportRow.Add(record);
        }

    }
