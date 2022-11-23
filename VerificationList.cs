
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using Xamarin.Forms.Shapes;

namespace simicon.automation;

public static class VerificationList
{

    public static ArrayList VerificationCriteriaList = new ArrayList();

    public static VerificationRecord GetVerificationForAT(string At)
    {
        foreach (VerificationRecord record in VerificationCriteriaList)
        {
            if (record.AT.Equals(At))
                return record;
        }
        return (new VerificationRecord("Failure",-1,  "Required AT does not fond in VerificarionList please verify your input."));
    }


    public static ArrayList InitVerificationCriteria()
    {
        Console.WriteLine(Base.GOP() + "------------------------------------------->we are in Camera.COnnect=>VerificationList.InitVerificationCriteria.");

        VerificationCriteriaList = new ArrayList();
        VerificationCriteriaList.Add(new VerificationRecord("AT?", 2, "this help"));
        VerificationCriteriaList.Add(new VerificationRecord("ATB", 1, "Night mode"));
        VerificationCriteriaList.Add(new VerificationRecord("ATC", 1, "ApCorr"));
        VerificationCriteriaList.Add(new VerificationRecord("ATF", 1, "atf"));
        VerificationCriteriaList.Add(new VerificationRecord("ATG", 1, "atg"));
        VerificationCriteriaList.Add(new VerificationRecord("ATI", 1, "ati"));
        VerificationCriteriaList.Add(new VerificationRecord("ATI1", 1, "ati1"));
        VerificationCriteriaList.Add(new VerificationRecord("ATJ", 1, "atj"));
        VerificationCriteriaList.Add(new VerificationRecord("ATK", 1, "atk"));
        VerificationCriteriaList.Add(new VerificationRecord("ATK1", 1, "ATK1"));
        VerificationCriteriaList.Add(new VerificationRecord("ATL", 1, "atl"));
        VerificationCriteriaList.Add(new VerificationRecord("ATM", 1, "Gamma:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATN", 1, "atn="));//2nd exposure in uSec (33…39708), 0 - disable    
        VerificationCriteriaList.Add(new VerificationRecord("ATP", 2, "Cur exp:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATM", 1, "Gamma:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATM", 1, "Gamma:"));// Gamma correction
                                                                                //ATM=x - gamma, 0 - off, 1 - 0.7, 2 - 0.45, 3 - 0.35, 4 - 0.22, 5 - Custom1, 6 - Custom2ATM=7 x.xx - gamma, x.xx = 0.1 … 1.00
        VerificationCriteriaList.Add(new VerificationRecord("ATR", 1, "Gamma:"));//drop all settng to default
                                                                                //("ATR8", 1,"Gamma:"),//EXCLUDED AS DANGEROUS for FIRMWARE, awareness to owrtite or crash deployed firmware
        VerificationCriteriaList.Add(new VerificationRecord("ATR9", 1, "Gamma:"));//CPU restart/ Camera Restart
        VerificationCriteriaList.Add(new VerificationRecord("ATS", 1, "Gamma:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATS", 1, "SHUT:"));//exposure
        VerificationCriteriaList.Add(new VerificationRecord("ATT", 1, "TEST:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATU", 1, "Hmask:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATV", 1, "imx:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATV0", 1, "FPGA:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATV1", 1, "Build at:"));//25
        VerificationCriteriaList.Add(new VerificationRecord("ATW", 1, "Serial:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATX", 1, "imx:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATY", 1, "FPGA:"));
        VerificationCriteriaList.Add(new VerificationRecord("ATZ", 1, "Hist average:"));
        VerificationCriteriaList.Add(new VerificationRecord("AT1", 1, "Night auto mode:"));
        VerificationCriteriaList.Add(new VerificationRecord("AT2", 1, "Color"));
        VerificationCriteriaList.Add(new VerificationRecord("AT3", 1, "WB"));
        VerificationCriteriaList.Add(new VerificationRecord("AT9", 1, "Serial:"));

        Console.WriteLine(Base.GOP() + "------------------------------------------->VerificationList.InitVerificationCriteria. DONE.");
            Console.WriteLine(Base.GOP() + $"CriteriaList: {VerificationCriteriaList}");
        return VerificationCriteriaList;
        //Console.WriteLine($"QTY of InitVerificationCriteria: {VerificationCriteriaList}");
    } // end of initVerificationCriteria
}// end of class

    //VerificationRecord VefrificationRecord = from records in VerificationCriteria.Any where == ATComand;
    //var _record = VerificationCriteria.Where(t => t.Item1.Equals(ATComand));
    //VerificationCriteria = (VerificationRecord[])_record;
    //Console.WriteLine($"GetVerificarionRecord; rexordreceived: {_record}");
    //return (VerificationRecord)_record;