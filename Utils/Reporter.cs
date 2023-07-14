
using simicon.automation.Tests;

namespace simicon.automation.Utils;

public class Reporter : TestRun
{



    private readonly Snapshot snapshots = new Snapshot();
    //private readonly string reportPath = snapshots.LocalSnapshotPath;
    private string reportFilename = "_Report.html";
    private string ReportFile = "";
    private string ReportFolder = "";
    private string StartTimeStapm = string.Empty;
    public void Init()
    {
        log.Route("Reporter.Init()");
        DataHeap dataHeap = new DataHeap();
        StartTimeStapm = dataHeap.launchDateTime;
        ReportFolder = dataHeap.ReportFolder;

        ReportFile = Path.Combine(ReportFolder, reportFilename);
        //FileStream(string filename, FileMode mode)
        if (File.Exists(ReportFile))
        {
            File.Delete(ReportFile);
        }
        using (StreamWriter reportWriter = new StreamWriter(ReportFile))
        {
            /*
	border-collapse: collapse;
	}
</style>
</head>
<body>
<table width="100%" border="1">
<tr>
<th border: solid;width="10%"; style="text-align: center; vertical-align: middle;">Test Case name</th>
<th border: solid; width="10%"; style="text-align: center; vertical-align: middle;">AT sent</th>
<th border: solid;width="15%"; style="text-align: center; vertical-align: middle;">AT output</th>
<th border: solid;width="25%"; style="text-align: center; vertical-align: middle;">Snapshot Before AT</th>
<th border: solid;width="25%"; style="text-align: center; vertical-align: middle;">Snapshot AFter AT</th>
<th border: solid;width="15%"; style="text-align: center; vertical-align: middle;">conclusion</th
</tr>
             */
    reportWriter.WriteLine("<html>");
    reportWriter.WriteLine("<head>");
    reportWriter.WriteLine($"<title>{dataHeap.firmwareVersion}</title>");
    reportWriter.WriteLine("<style>");
    reportWriter.WriteLine("table, th, td {");
    reportWriter.WriteLine("border: 1px solid black;");
    reportWriter.WriteLine("border-collapse: collapse;");
    reportWriter.WriteLine("}</style>");
    reportWriter.WriteLine("<tr>");
            reportWriter.WriteLine("< th border: solid; width = \"15 %\"; style = \"text-align: center; vertical-align: middle;\" > Test Case name</ th >");
            reportWriter.WriteLine(Environment.NewLine);
            reportWriter.WriteLine("< th border: solid; width = \"15 %\"; style = \"text-align: center; vertical-align: middle;\" > Test Case  Description</ th >");
            reportWriter.WriteLine(Environment.NewLine);
            reportWriter.WriteLine("< th border: solid; width = \"3 %\"; style = \"text-align: center; vertical-align: middle;\" > AT sent </ th >");
    reportWriter.WriteLine("< th border: solid; width = \"17 %\"; style = \"text-align: center; vertical-align: middle;\" > AT output </ th >");
            reportWriter.WriteLine(Environment.NewLine);
    reportWriter.WriteLine("< th border: solid; width = \"30 %\"; style = \"text-align: center; vertical-align: middle;\" >Snapshot</ th >");
            reportWriter.WriteLine(Environment.NewLine);
            reportWriter.WriteLine(Environment.NewLine);
            reportWriter.WriteLine("< th border: solid; width = \"10 %\"; style = \"text-align: center; vertical-align: middle;\" > conclusion </ th>");
    reportWriter.WriteLine("</ tr>");
            reportWriter.WriteLine(Environment.NewLine);
        }//End of yding StreamWriter
        //Create the file.
    }// end of initReport

    public void addrReportRow()
    {
        using StreamWriter sw = File.AppendText(ReportFile);
        {
            sw.WriteLine("<tr>");
            sw.WriteLine($"<td>{reportRow.ID}</td>");// from test case
            sw.WriteLine(Environment.NewLine);
            sw.WriteLine($"<td>{reportRow.Command}</td>");// from test case
            sw.WriteLine(Environment.NewLine);
            sw.WriteLine($"<td>{reportRow.output}</td>");//from helper.Verify.ouput
            sw.WriteLine(Environment.NewLine);

            if (reportRow.snapshot == "")
            {
                sw.WriteLine($"<td style=\"text-align: center; vertical-align: middle;\">N/A</td>");//from helper.Verify filr name param for<img src="">
            }
            else
            {
                sw.WriteLine($"<td><img src=\"{reportRow.Command}.jpg\" alt = {reportRow.Command}, snapshot.></img></td>");
                /*
                 *         <td colspan = 4><img src="C:\Pics\H.gif" alt="" border=3 height=100 width=100></img></td>
    </tr>
                 */
            }
            sw.WriteLine(Environment.NewLine);
            sw.WriteLine($"<td>{reportRow.Conclusion}</td>");//from helper.Verify filr name param for<img src="">
            sw.WriteLine("</tr>");
            sw.WriteLine(Environment.NewLine);
            reportRow.Wipe();
        }
    }
    public void FinalizeReport()
    {
        log.Route("simicon.automation.Utils.Reporter.FinalizeReport()");
    }
}