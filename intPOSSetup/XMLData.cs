using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intPOSSetup
{
    public static class XMLData
    {
        //
        public static string POSType = "";
        public static DateTime InstallationDate;
        public static string Info = "";
        public static string Version = "";
        //
        public static int Type = 0;
        public static bool POSStatus = false;
        public static bool SyncStatus = false;
        //

        public static string Scanner = "";
        public static string Scale = "";
        public static bool ScannerInUsed = true;

        //
        public static string Key = "";
        //
        public static string PrinterName = "";
        public static string Disclaimer = "";
        //
        public static string ServerName = "";
        public static string DbName = "";
        public static string UserName = "";
        public static string Password = "";
        //
        public static string DbConnectionString = "";
        public static string PriorityCode = "";

        public static string Key_BE = "";
        public static string PriorityCode_BE = "";

        public static string ORDNo = "";
        public static DateTime UpdatedDate;

        //
        public static int LiveToLocalTime = 5;
        public static int LocalToLiveTime = 1;
        public static int OrderSuccessScreen = 20;

        //EPX Payment Gateway Setting
        public static bool EPXPaymenrServiceON = false;
        public static string EPXTerminalIP = "";
        public static string EPXTerminalPort = "";
        public static int EPXShow = 0;
        public static int IsDemoVersion = 0;
    }

    public static class XMLDataLabel
    {
        //
        public static string POSType = "";
        public static DateTime InstallationDate;
        public static string Info = "";
        public static string Version = "";
        //
        public static string Scanner = "";
        //
        public static string PrinterName = "";
        public static string SignPrinterName = "";
        public static string Disclaimer = "";
        //
        public static string ServerName = "";
        public static string DbName = "";
        public static string UserName = "";
        public static string Password = "";
        //
        public static string DbConnectionString = "";
        public static string PriorityCode = "";

        public static string BarCode = "";
        public static string HalfPage = "";
        public static string FullPage = "";
        public static string PotraitFullPage = "";
        public static string SevenSign = "";
    }

}
