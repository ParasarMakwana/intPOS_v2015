using SFPOS.Entities.FrontEnd;
using System;
using System.Collections.Generic;
using System.Data;

namespace SFPOS.Common
{
    public static class LoginInfo
    {
        public static long UserId = 0;
        public static long StoreID = 0;
        public static string UserName = "";
        public static string StoreName = "";
        public static decimal StoreDefaultTax = 0;
        public static string StoreDisclaimer = "";
        public static bool IsStoreTax = false;
        public static int CashierAge = 0;
        public static bool CashierAgeVerified = false;
        public static string RoleType = "";
        public static string CounterIP = "";
        public static string MacAddress = "";

        public static string frmName = "Master";
        public static string Command = "";
        public static string CashierID = "";
        /*public static int ScreenID = 0;*/  //1 Backend,2 FrontEnd
        public static bool Connections = true;
        public static int SyncType = 0;  //1 FullSync,2 LiveToLocalSync,3 LocalToLiveSync
        public static bool ManualSync = false;
        public static bool POSStatus = true;
        public static string ComPort = "COM1";
        public static string ORDNO = "";
        public static string SettingScreen = "tabezPOSProInfo";
        public static bool TaxCarry = false;

        public static bool OpenScale = true;

        //public static decimal FSEligibleVoidAmt = 0;
        //public static decimal FSTotal = 0;
        //public static decimal CashAmt = 0;
        //public static decimal Change = 0;
        //public static int PaymentType = 0;
        //public static decimal remainingFSAmt = 0;
        //public static bool IsOrder = false;
        //public static bool IsFSClicked = false;
        //public static bool IsFSVoidtax = false;
        //public static decimal CheckAmt = 0;
        //public static decimal CreditAmt = 0;
        public static string transcationNo = "";
        public static decimal TareWeight = 0;

        public static DateTime LoginTime = DateTime.Now;

        public static long LastOrderID = 0;
        public static int LastPaymentType = 0;
        public static decimal LastCashAmt = 0;
        public static decimal LastChange = 0;
        public static decimal LastCheckAmt = 0;
        public static decimal LastCreditAmt = 0;
        public static decimal LastBalance = 0;

        public static bool tnfn = false;
        public static bool CasePrice = false;

        public static decimal MaxVoidAmount = 0;
        public static decimal? TotalVoidAmount = 0;
        public static bool IsManagerReq = false;
        public static bool LocalSync = false;

        public static List<string> lstMacAddress = new List<string>();
        public static bool IsVerifiedTillStatus { get; set; }
    }

    public static class OrderInfo
    {
        public static decimal RecivedAmt = 0;
        public static decimal FSEligibleVoidAmt = 0;
        public static decimal FSTotal = 0;
        public static decimal CashAmt = 0;
        public static decimal Change = 0;
        public static int PaymentType = 0;
        public static decimal remainingFSAmt = 0;
        public static bool IsOrder = false;
        public static bool IsFSClicked = false;
        public static bool IsFSVoidtax = false;
        public static decimal CheckAmt = 0;
        public static decimal CreditAmt = 0;
        public static decimal TaxCarryAmount = 0;
        public static bool IsRefundOrder = false;
        //public static decimal TotalReceivedAmount = 0;
        public static int MenuIdforPermission = 0;
        public static bool IsPaymentResume = false;
        public static bool RemoveCoupon = false;
        public static decimal inapplicableDepartmentsAmt = 0;
        public static decimal TaxAmount = 0;
        public static bool CancelFS = false;
    }

    public static class RetryOrder
    {
        public static bool isRetry = false;
        public static bool isCancel = false;
        public static bool UpdateNow = false;
    }

    public static class AgeVerifidInfo
    {
        public static long DepartmentAge = 0;
        public static long SectionAge = 0;
        public static long StoreAge = 0;
        public static int AgeVerificationAge = 0;
        public static bool AgeChecked = false;
        public static bool AgeVerified = false;
    }

    public static class PriceOverride
    {
        public static decimal MaxOverridePrice = 0;
        public static decimal? OverridePrice = 0;
        public static decimal? OverrideRange = 0;
        public static decimal? TotalOverrideRange = 0;
        public static bool AllowOverridePrice = false;
        public static bool IsOverridePrice = false;
        public static decimal Qty = 1;
    }

    public static class ManagerAction
    {
        public static bool suspend = false;
        public static DataTable dtresume;
        public static bool TillStatus = false;
        public static bool resume = false;
        public static bool Exit = false;
        public static string ManagerName = "";

    }

    public static class CouponInfo
    {
        public static string CouponCode = "";
        public static string CouponName = "";
        public static decimal Discount = 0;
        public static decimal DiscAmt = 0;
        public static decimal MinPurAmt = 0;
        public static long availableCoupon = 0;
        public static long usedCoupon = 0;
        public static bool isCoupon = false;
        public static long CouponId = 0;
        public static bool IsRestricted = false;
        public static decimal FSCouponDisc = 0;
        public static decimal TaxAmount = 0;
    }

    public static class CustomerInfo
    {
        public static bool IsCustomerVerfied = false;
        public static bool IsCustomerVerfiedForTC = false;
        public static long CustomerId = 0;
    }

    public static class MultiPaymentInfo
    {
        public static List<PaymentTransMasterModel> lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
    }
    public static class CardRefundType {
        public static string CardType = "";
    }
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
        public static bool IsSync = false;
        public static int SyncTime = 0;
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
        public static int POSScreen = 0;
        public static int CustomerScreen = 1;
    }

}
