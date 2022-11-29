using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

public  static class ATkeyWords
{

    public static Dictionary<string, string> KeyWords = new Dictionary<string, string>()
        {
        //{}ATcommand, keyWord}
        {"AT?" ,"this help"},
        {"ATA" ,"AGC"},
        {"ATB" ,"Night mode"},
        {"ATC" ,"ApCorr"},
        {"ATD" ,"ApCorr"},
        {"ATF" ,"Cam Flip:"},
        {"ATG" ,"Gain"},
        {"ATI" ,"INFO: AGC"},
        {"ATI1" ,"INFO: AGC"},
        {"ATJ" ,"Offset"},
        {"ATK" ,"Hpoint"},
        {"ATK1" ,"ATK1"},
        {"ATL" ,"Max exp"},
        {"ATM" ,"Gamma:"},
        {"ATN" ,"ATn="},
        {"ATO" ,"Ext sync: "},
        {"ATP" ,"Cur exp:"},
        {"ATM" ,"Gamma:"},
        {"ATM" ,"Gamma:"},
        {"ATR" ,"Gamma:"},
        {"ATR9" ,"Gamma:"},
        {"ATS" ,"Gamma:"},
        {"ATS" ,"SHUT: "},
        {"ATT" ,"TEST: "},
        {"ATU" ,"Hmask: "},
        {"ATV" ,"imx"},
        {"ATV0" ,"FPGA:"},
        {"ATV1" ,"Build at:"},
        {"ATW" ,"Serial:"},
        {"ATX" ,"imx:"},
        {"ATY" ,"FPGA: imx"},
        {"ATZ" ,"Hist average:"},
        {"AT1" ,"Night auto mode:"},
        {"AT2" ,"Color"},
        {"AT3" ,"WB"},
        {"AT9" ,"Serial:"}
      };
}

