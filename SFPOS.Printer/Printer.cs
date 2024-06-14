using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Printer.Enums;
using SFPOS.Printer.EscPosCommands;
using SFPOS.Printer.Helper;
using SFPOS.Printer.Interfaces.Command;
using SFPOS.Printer.Interfaces.Printer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SFPOS.Printer
{
    public class Printer : IPrinter
    {

        //private string logfilepath = "C:\\@vishnu\\log.txt";

        #region Properties

        private byte[] _buffer;
        private readonly string _printerName;
        private readonly IPrintCommand _command;
        private readonly PrinterType _printerType;
        public DataTable dt = new DataTable();
        //public decimal Balance = 0;
        //public decimal FSEligibleVoidAmt = 0;
        public decimal TaxableAmount = 0;
        public bool RePrint = false;
        public bool Refund = false;
        public bool IsCancel = false;
        private decimal totalAmtforFS = 0;
        private decimal ItemCount = 0;
        private decimal Special_Qty = 0;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        #endregion

        #region Constructors

        public Printer(string printerName, PrinterType type)
        {
            try
            {
                _printerName = string.IsNullOrEmpty(printerName) ? "temp.prn" : printerName.Trim();
                _printerType = type;

                switch (type)
                {
                    case PrinterType.Epson:
                        _command = new EscPos();
                        break;
                }
            }
            catch(Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
            
        }

        #endregion

        #region Methods

        public int ColsNomal => _command.ColsNomal;
        public int ColsCondensed => _command.ColsCondensed;
        public int ColsExpanded => _command.ColsExpanded;

        public decimal giftcardbalance = 0;
        private BitmapData data;

        public void PrintDocument()
        {
            if (_buffer == null)
                return;

            if (!RawPrinterHelper.SendBytesToPrinter(_printerName, _buffer))
                throw new ArgumentException("Something went wrong : " + _printerName);
        }

        public string GetStatus()
        {
            string data = "";
            try
            {
                data = RawPrinterHelper.GetBytesFromPrinter(_printerName);
                
            }
            catch(Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
            return data;
        }

        public void Append(string value)
        {
            AppendString(value, true);
        }

        public void Append(byte[] value)
        {
            try
            {
                if (value == null)
                    return;

                var list = new List<byte>();
                if (_buffer != null)
                    list.AddRange(_buffer);
                list.AddRange(value);
                _buffer = list.ToArray();
            }
            catch(Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
            
        }

        public void AppendWithoutLf(string value)
        {
            AppendString(value, false);
        }

        //private void LogMessage(string message)
        //{
        //    try
        //    {
        //        using (StreamWriter writer = new StreamWriter(logfilepath, true))
        //        {
        //            writer.WriteLine($"{DateTime.Now}: {message}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error writing to log file: " + ex.Message);
        //    }
        //}

        private void AppendString(string value, bool useLf)
        {
           // LogMessage(value);

            if (string.IsNullOrEmpty(value))
                return;

            if (useLf) value += "\n";

            var list = new List<byte>();
            if (_buffer != null) list.AddRange(_buffer);

            var bytes = _printerType == PrinterType.Bematech
                ? Encoding.GetEncoding(850).GetBytes(value)
                : Encoding.GetEncoding("IBM860").GetBytes(value);

            list.AddRange(bytes);
            _buffer = list.ToArray();
        }

        public void NewLine()
        {
            Append("\r");
        }

        public void NewLines(int lines)
        {
            for (var i = 1; i < lines; i++)
                NewLine();
        }

        public void Clear()
        {
            _buffer = null;
        }

        #endregion

        #region Commands

        public void ReceiptPrint()
        {
            try
             {
                if (dt != null && dt.Rows.Count > 0)
                {
                    #region REPRINT
                    if (RePrint == true)
                    {
                        AlignCenter();
                        Append("=== DUPLICATE CUSTOMER COPY ===");
                    }
                    #endregion

                    #region REFUND
                    if (Refund == true)
                    {
                        AlignCenter();
                        Append("=== REFUND ===");
                    }
                    #endregion

                    #region LOGO

                    AlignCenter();
                    //if(XMLData.IsDemoVersion == 1)
                    //{
                    //    Append(GetDocument());
                    //}
                    //else
                    //{
                        Append(GetDocument());
                    //}
                    #endregion

                    #region HEADER

                    AlignCenter();
                    ExpandedMode(PrinterModeState.On);
                    BoldMode(dt.Rows[0]["StoreName"].ToString());
                    ExpandedMode(PrinterModeState.Off);
                    NewLines(2);
                    if (dt.Rows[0]["SMAddress"].ToString() != "")
                    {
                        Append(dt.Rows[0]["SMAddress"].ToString());
                    }
                    if (dt.Rows[0]["SAddress2"].ToString() != "")
                    {
                        Append(dt.Rows[0]["SAddress2"].ToString());
                    }
                    if (dt.Rows[0]["SPhone"].ToString() != "")
                    {
                        Append("TEL: " + dt.Rows[0]["SPhone"].ToString());
                    }
                    Append(dt.Rows[0]["CreatedDate"].ToString());
                    Append("TRANS #: " + dt.Rows[0]["OrdNo"].ToString().Replace("TS", ""));
                    Append("CASHIER: " + dt.Rows[0]["FirstName"].ToString());
                    NewLines(2);

                    #endregion

                    #region PRODUCT
                    AlignLeft();
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        if (Refund == true)
                        {
                            dt.Rows[row]["TotalAmount"] = dt.Rows[row]["TotalAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["TaxAmount"] = dt.Rows[row]["TaxAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["GrossAmount"] = dt.Rows[row]["GrossAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["CashAmount"] = dt.Rows[row]["CashAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["CheckAmount"] = dt.Rows[row]["CheckAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["CreditCardAmount"] = dt.Rows[row]["CreditCardAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["FoodStampAmount"] = dt.Rows[row]["FoodStampAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["RefundAmount"] = dt.Rows[row]["RefundAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["TaxableAmount"] = dt.Rows[row]["TaxableAmount"].ToString().Replace("-", "");
                            dt.Rows[row]["Quantity"] = dt.Rows[row]["Quantity"].ToString().Replace("-", "");
                            dt.Rows[row]["finalPrice"] = dt.Rows[row]["finalPrice"].ToString().Replace("-", "");
                            dt.Rows[row]["GroupQty"] = dt.Rows[row]["GroupQty"].ToString().Replace("-", "");
                            dt.Rows[row]["GroupPrice"] = dt.Rows[row]["GroupPrice"].ToString().Replace("-", "");
                            dt.Rows[row]["CaseQty"] = dt.Rows[row]["CaseQty"].ToString().Replace("-", "");
                            dt.Rows[row]["CasePrice"] = dt.Rows[row]["CasePrice"].ToString().Replace("-", "");
                            dt.Rows[row]["FSEligibleAmount"] = dt.Rows[row]["FSEligibleAmount"].ToString().Replace("-", "");
                          
                        }
                            if (Functions.GetBoolean(dt.Rows[row]["IsScale"].ToString()) == true)
                        {
                            if (Functions.GetBoolean(dt.Rows[row]["DiscountApplyed"].ToString()) == true)
                            {
                                Append("GROUP DISCOUNT INCULDED");
                                if (dt.Rows[row]["ManWT"].ToString().ToUpper() == "TRUE")
                                {
                                    Append("MAN WT " + dt.Rows[row]["Quantity"].ToString() + " lb @ $" + GetDisplayAmount(dt.Rows[row]["GroupPrice"].ToString()) + " For " + dt.Rows[row]["GroupQty"].ToString() + " lb");
                                    Append("Reg. $" + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["SellPrice"].ToString()).ToString("0.00")) + "/lb");
                                }
                                else
                                {
                                    Append(dt.Rows[row]["Quantity"].ToString() + " lb @ $" + GetDisplayAmount(dt.Rows[row]["GroupPrice"].ToString()) + " For " + dt.Rows[row]["GroupQty"].ToString() + " lb");
                                    Append("Reg. $" + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["SellPrice"].ToString()).ToString("0.00")) + "/lb");
                                }

                            }
                            else
                            {
                                if (dt.Rows[row]["ManWT"].ToString().ToUpper() == "TRUE")
                                {
                                    Append("MAN WT " + dt.Rows[row]["Quantity"].ToString() + " lb @ $" + GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString()) + "/lb");
                                }
                                else
                                {
                                    Append(dt.Rows[row]["Quantity"].ToString() + " lb @ $" + GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString()) + "/lb");

                                }
                            }
                            if (dt.Rows[row]["ProductName"].ToString().Length > 25)
                            {
                                AppendWithoutLf(dt.Rows[row]["ProductName"].ToString().Substring(0, 25));
                            }
                            else
                            {
                                string ProName = dt.Rows[row]["ProductName"].ToString();
                                int Count = ProName.Length;
                                if (Count < 25)
                                {
                                    Count = 25 - Count;
                                    for (int i = 0; i < Count; i++)
                                    {
                                        ProName = ProName + " ";
                                    }
                                }
                                AppendWithoutLf(ProName);
                            }

                            #region SALEPRICE & ABBRIVIATIONS
                            if (Functions.GetBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true && Functions.GetBoolean(dt.Rows[row]["IsTax"].ToString().ToLower()) == true)
                            {
                                Append((GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + " " + "TF").PadLeft(17));
                                if (OrderInfo.IsFSClicked == true)
                                {
                                    totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["FSEligibleAmount"].ToString()).ToString("0.00"));
                                }
                            }
                            else if (Functions.GetBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true)
                            {
                                Append((GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + " " + "F ").PadLeft(17));
                                if (OrderInfo.IsFSClicked == true)
                                {
                                    totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["FSEligibleAmount"].ToString()).ToString("0.00"));
                                }
                            }
                            else if (Functions.GetBoolean(dt.Rows[row]["IsTax"].ToString().ToLower()) == true)
                            {
                                Append((GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + " " + "T ").PadLeft(17));
                            }
                            else
                            {
                                Append((GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + " " + "  ").PadLeft(17));
                                //if (Functions.GetBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true)
                                //{
                                //    if (OrderInfo.IsFSClicked == true)
                                //    {
                                //        totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()).Replace("F", "")).ToString("0.00"));
                                //    }
                                //}
                            }
                            #endregion
                            ItemCount += 1;
                        }
                        else
                        {
                            string s = dt.Rows[row]["Quantity"].ToString();
                            string[] parts = s.Split('.');
                            int i2 = int.Parse(parts[1]);

                            if (Functions.GetBoolean(dt.Rows[row]["DiscountApplyed"].ToString()) == true)
                            {
                                Append("GROUP DISCOUNT INCLUDED");
                                if (i2 >= 0)
                                {
                                    Append(Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) + " @ $" + GetDisplayAmount(GetDisplayAmount(dt.Rows[row]["GroupPrice"].ToString())) + " For " + Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["GroupQty"].ToString()))));
                                    Append("Reg. $" + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["SellPrice"].ToString()).ToString("0.00")) + "/EA");
                                }
                                else/* if (Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) != 1)*/
                                {
                                    Append(Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) + " @ $" + GetDisplayAmount(GetDisplayAmount(dt.Rows[row]["GroupPrice"].ToString())) + " For " + Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["GroupQty"].ToString()))));
                                    Append("Reg. $" + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["SellPrice"].ToString()).ToString("0.00")) + "/EA");
                                }
                            }
                            else if (Functions.GetBoolean(dt.Rows[row]["CasePriceApplied"].ToString()) == true)
                            {
                                Append("CASE PRICE APPLIED");
                                if (i2 > 0)
                                {
                                    Append(Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) + " @ $" + GetDisplayAmount(GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString())));
                                }
                                else if (Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) != 1)
                                {
                                    Special_Qty = Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()) / Convert.ToDecimal(dt.Rows[row]["CaseQty"].ToString());
                                    Append(Convert.ToInt32(Math.Floor(Convert.ToDecimal(Special_Qty.ToString()))) + " @ $" + GetDisplayAmount(dt.Rows[row]["CasePrice"].ToString()));
                                    //Append(Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) + " @ $" + GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString()));
                                }
                            }
                            else
                            {
                                if (i2 > 0)
                                {
                                    Append(Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) + " @ $" + GetDisplayAmount(GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString())));
                                }
                                else if (Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) != 1)
                                {
                                    Append(Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) + " @ $" + GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString()));
                                }
                            }

                            if (dt.Rows[row]["ProductName"].ToString().Length > 25)
                            {
                                int strlen = 0;
                                int temp = Convert.ToInt32(dt.Rows[row]["ProductName"].ToString().Length);
                                int temp2 = temp / 25;

                                for (int i = 0; i <= temp2; i++)
                                {
                                    if (temp >= 25)
                                    {
                                        if (temp == 25)
                                        {
                                            AppendWithoutLf(dt.Rows[row]["ProductName"].ToString().Substring(strlen, 25));
                                        }
                                        else
                                        {
                                            Append(dt.Rows[row]["ProductName"].ToString().Substring(strlen, 25));
                                        }
                                        strlen = strlen + 25;
                                        temp = temp - 25;

                                    }
                                    else
                                    {
                                        if (temp > 0)
                                        {
                                            string ProName = dt.Rows[row]["ProductName"].ToString().Substring(strlen, temp);
                                            int Count = ProName.Length;
                                            if (Count < 25)
                                            {
                                                Count = 25 - Count;
                                                for (int j = 0; j < Count; j++)
                                                {
                                                    ProName = ProName + " ";
                                                }
                                            }
                                            AppendWithoutLf(ProName);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string ProName = dt.Rows[row]["ProductName"].ToString();
                                int Count = ProName.Length;
                                if (Count < 25)
                                {
                                    Count = 25 - Count;
                                    for (int i = 0; i < Count; i++)
                                    {
                                        ProName = ProName + " ";
                                    }
                                }
                                //DoubleWidth4();
                                AppendWithoutLf(ProName);
                            }

                            #region SALEPRICE & ABBRIVIATIONS
                            if (Functions.GetBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true && Functions.GetBoolean(dt.Rows[row]["IsTax"].ToString().ToLower()) == true)
                            {
                                Append((GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + " " + "TF").PadLeft(17));
                                if (OrderInfo.IsFSClicked == true)
                                {
                                    totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["FSEligibleAmount"].ToString()).ToString("0.00"));
                                }
                            }
                            else if (Functions.GetBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true)
                            {
                                Append((GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + " " + "F ").PadLeft(17));
                                if (OrderInfo.IsFSClicked == true)
                                {
                                    totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["FSEligibleAmount"].ToString()).ToString("0.00"));
                                }
                            }
                            else if (Functions.GetBoolean(dt.Rows[row]["IsTax"].ToString().ToLower()) == true)
                            {
                                Append((GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + " " + "T ").PadLeft(17));
                            }
                            else
                            {
                                Append((GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + " " + "  ").PadLeft(17));
                            }
                            #endregion

                            if (Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()) != 0)
                            {
                                if (Functions.GetBoolean(dt.Rows[row]["CasePriceApplied"].ToString()) == true)
                                    ItemCount += Special_Qty;
                                else
                                    ItemCount += Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString());
                            }
                            else
                            {
                                ItemCount += 1;
                            }
                        }
                    }
                    #endregion

                    if (!IsCancel)
                    {
                        #region PRICE LIST
                        NewLines(2);
                        AlignLeft();
                        AppendWithoutLf("SUBTOTAL".PadLeft(20));
                        Append((GetDisplayAmount(dt.Rows[0]["GrossAmount"].ToString())).PadLeft(19));

                        if (totalAmtforFS != 0)
                        {
                            AlignLeft();
                            Append(("FS ELIGIBLE " + "          " + GetDisplayAmount(totalAmtforFS.ToString())).PadLeft(20));

                            if (Functions.GetDecimal(dt.Rows[0]["TaxExempted"].ToString()) > 0)
                            {
                                AlignLeft();
                                //Append(("TAX EXEMPTED" + "          " + GetDisplayAmount(FSEligibleVoidAmt.ToString())).PadLeft(20));
                                Append(("TAX EXEMPTED" + "           " + GetDisplayAmount(dt.Rows[0]["TaxExempted"].ToString())).PadLeft(20));
                            }
                        }
                        if (OrderInfo.IsFSVoidtax == false)
                        {
                            if (TaxableAmount != 0)
                            {
                                AlignLeft();
                                Append(("TAXABLE AMOUNT" + "        " + GetDisplayAmount(TaxableAmount.ToString())).PadLeft(20));
                            }
                        }
                        //if (Functions.GetDecimal(dt.Rows[0]["CouponDiscAmt"].ToString()) > 0)
                        //{
                        if(Functions.GetDecimal(dt.Rows[0]["CouponDiscAmt"].ToString())>0)
                        { 
                            AlignLeft();
                            Append(("COUPON CODE" + "           " + (dt.Rows[0]["CouponCode"].ToString())).PadLeft(20));
                        }
                        AlignLeft();
                        if (Convert.ToDecimal(dt.Rows[0]["TaxAmount"]) != 0)
                        {
                            AlignLeft();
                            AppendWithoutLf("TAX     ".PadLeft(20));
                            Append((GetDisplayAmount(dt.Rows[0]["TaxAmount"].ToString())).PadLeft(19));
                        }
                        AlignLeft();
                        AppendWithoutLf("TOTAL   ".PadLeft(20));
                        Append((GetDisplayAmount((Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString())
                        + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())).ToString())).PadLeft(19));

                        if ((dt.Rows[0]["CouponDiscAmt"].ToString()) != null)
                        {
                            if (Functions.GetDecimal(dt.Rows[0]["CouponDiscAmt"].ToString()) > 0)
                            {
                                AppendWithoutLf("COUPON DISCOUNT".PadLeft(27));
                                Append((GetDisplayAmount(dt.Rows[0]["CouponDiscAmt"].ToString())).PadLeft(12));

                                //Komel 20201104
                                AppendWithoutLf("BALANCE ".PadLeft(20));
                                Append(GetDisplayAmount(GetDisplayAmount(((Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString())
                        + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - Convert.ToDecimal(dt.Rows[0]["CouponDiscAmt"].ToString())).ToString())).PadLeft(19));
                            }
                        }

                        #endregion

                        #region PAYMENTS
                        for (int i = 0; i < MultiPaymentInfo.lstPaymentTransMasterModel.Count; i++)
                        {
                            if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 4)
                            {
                                AppendWithoutLf("FOOD STAMPS".PadLeft(23));
                                Append((GetDisplayAmount(MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount.ToString())).PadLeft(16));
                            }
                            else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 3)
                            {
                                AppendWithoutLf("CHECK   ".PadLeft(20));
                                Append((GetDisplayAmount(MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount.ToString())).PadLeft(19));
                            }
                            else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 2)
                            {
                                AppendWithoutLf("CREDIT/DEBIT CARD".PadLeft(29));
                                Append((GetDisplayAmount(MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount.ToString())).PadLeft(10));
                            }
                            else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 1)
                            {
                                AppendWithoutLf("CASH    ".PadLeft(20));
                                Append((GetDisplayAmount(MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount.ToString())).PadLeft(19));
                            }


                            if (MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance != 0 && MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance != null && i != MultiPaymentInfo.lstPaymentTransMasterModel.Count - 1)
                            {
                                if (MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance.ToString().Contains("-"))
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = Functions.GetDecimal(MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance.ToString().Replace("-", ""));
                                }
                                AppendWithoutLf("BALANCE ".PadLeft(20));
                                Append((GetDisplayAmount(MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance.ToString())).PadLeft(19));
                            }
                            if (MultiPaymentInfo.lstPaymentTransMasterModel[i].ChangeAmount != 0 && MultiPaymentInfo.lstPaymentTransMasterModel[i].ChangeAmount != null)
                            {
                                AppendWithoutLf("CHANGE  ".PadLeft(20));
                                Append((GetDisplayAmount(MultiPaymentInfo.lstPaymentTransMasterModel[i].ChangeAmount.ToString())).PadLeft(19));
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        #region TRANSACTION SUSPEND
                        AlignLeft();

                        NewLines(2);
                        Append("Transaction Canceled by Employee: " + dt.Rows[0]["FirstName"].ToString());
                        NewLines(1);
                        ItemCount = 0;
                        AlignCenter();
                        #endregion
                    }

                    #region FOOTER OR SUSPEND
                    if (OrderInfo.PaymentType == 5)
                    {
                        #region TRANSACTION SUSPEND
                        AlignLeft();

                        Append("TRANSACTION SUSPENDED BY: " + ManagerAction.ManagerName.ToUpper());
                        NewLines(1);

                        AlignCenter();
                        #endregion

                        #region BARCODE
                        string barCode = dt.Rows[0]["TransSuspendCode"].ToString().ToUpper();
                        Code39(barCode);
                        NewLines(1);

                        AlignCenter();
                        barCode = barCode.ToUpper().Remove(0, 2);
                        Append(barCode);
                        NewLines(2);
                        #endregion
                    }
                    else
                    {
                        #region FOOTER
                        NewLines(2);
                        AlignCenter();
                        Append("ITEM COUNT: " + Convert.ToInt32(Math.Floor(ItemCount)));
                        NewLines(2);
                        if (XMLData.EPXPaymenrServiceON)
                        {
                            if (dt.Rows[0]["ResponseValue"].ToString() == "00")
                            {
                                Append("TVR: " + dt.Rows[0]["TVR"].ToString());
                                NewLine();
                                Append("Card Type: " + dt.Rows[0]["CardType"].ToString());
                                NewLine();
                                Append("EMV AID: " + dt.Rows[0]["EmvAid"].ToString());
                                NewLine();
                                Append("TSI: " + dt.Rows[0]["TSI"].ToString());
                                NewLines(2);
                            }
                        }
                        AlignCenter();
                        string[] Disclaimer = LoginInfo.StoreDisclaimer.ToLower().Split('\n');
                        if (Disclaimer != null)
                        {
                            for (int i = 0; i < Disclaimer.Length; i++)
                            {
                                CondensedMode(PrinterModeState.On);
                                Append(Disclaimer[i].Replace("\\n", ""));
                                CondensedMode(PrinterModeState.Off);
                            }
                        }
                        NewLine();
                        #endregion

                        //#region BARCODE
                        //AlignCenter();
                        //string barCode = dt.Rows[0]["OrdNo"].ToString().Replace("TS", "").Substring(2, dt.Rows[0]["OrdNo"].ToString().Replace("TS", "").Length - 2);
                        //Code39(barCode);

                        //Append(barCode);
                        //NewLines(2);
                        //#endregion
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "Printer" + ex.StackTrace, ex.LineNumber());

            }
        }

        public static string GetDisplayAmount(string Amount)
        {
            return Convert.ToDouble(Amount).ToString("N2");
        }

        public void Separator()
        {
            Append(_command.Separator());
        }

        public void AutoTest()
        {
            Append(_command.AutoTest());
        }

        public BitmapData GetBitmapData(string bmpFileName)
        {
            using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
            {
                var threshold = 127;
                var index = 0;
                double multiplier = 500;
                double scale = (double)(multiplier / (double)bitmap.Width);
                int xheight = (int)(bitmap.Height * scale);
                int xwidth = (int)(bitmap.Width * scale);
                var dimensions = xwidth * xheight;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < xheight; y++)
                {
                    for (var x = 0; x < xwidth; x++)
                    {
                        var _x = (int)(x / scale);
                        var _y = (int)(y / scale);
                        var color = bitmap.GetPixel(_x, _y);
                        var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminance < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                {
                    Dots = dots,
                    Height = (int)(bitmap.Height * scale),
                    Width = (int)(bitmap.Width * scale)
                };
            }
        }

        public class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }

        private byte[] GetDocument()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                RenderLogo(bw);
                bw.Flush();

                return ms.ToArray();
            }
        }

        private void RenderLogo(BinaryWriter bw)
        { 
            //var data = GetBitmapData(Application.StartupPath + "\\SF_logo_a.png");
            if (XMLData.IsDemoVersion == 1)
            {
                 data = GetBitmapData(Application.StartupPath + "\\sf_Logo_Demo.png"); 
            }
            else
            {
                 data = GetBitmapData(Application.StartupPath + "\\SF_logo_a.png");
            }
            var dots = data.Dots;
            var width = BitConverter.GetBytes(data.Width);

            bw.Write((char)0x1B);
            bw.Write('3');
            bw.Write((byte)24);
            int offset = 0;
            while (offset < data.Height)
            {

                bw.Write((char)0x1B);
                bw.Write('*');
                bw.Write((byte)33);
                bw.Write(width[0]);
                bw.Write(width[1]);

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;

                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;

                            int i = (y * data.Width) + x;
                            bool v = false;
                            if (i < dots.Length)
                            {
                                v = dots[i];
                            }
                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }

                        bw.Write(slice);
                    }
                }
                offset += 24;
                bw.Write((char)0x0A);
            }

            bw.Write((char)0x1B);
            bw.Write('2');
            //bw.Write((byte)30);
        }

        public void LowPaper()
        {
            try
            {
                Append(_command.Drawer.LowPaper());
            }
            catch(Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        #region FontMode

        public void ItalicMode(string value)
        {
            Append(_command.FontMode.Italic(value));
        }

        public void ItalicMode(PrinterModeState state)
        {
            Append(_command.FontMode.Italic(state));
        }

        public void BoldMode(string value)
        {
            Append(_command.FontMode.Bold(value));
        }

        public void BoldMode(PrinterModeState state)
        {
            Append(_command.FontMode.Bold(state));
        }

        public void UnderlineMode(string value)
        {
            Append(_command.FontMode.Underline(value));
        }

        public void UnderlineMode(PrinterModeState state)
        {
            Append(_command.FontMode.Underline(state));
        }

        public void ExpandedMode(string value)
        {
            Append(_command.FontMode.Expanded(value));
        }

        public void ExpandedMode(PrinterModeState state)
        {
            Append(_command.FontMode.Expanded(state));
        }

        public void CondensedMode(string value)
        {
            Append(_command.FontMode.Condensed(value));
        }

        public void CondensedMode(PrinterModeState state)
        {
            Append(_command.FontMode.Condensed(state));
        }

        #endregion

        #region FontWidth

        public void NormalWidth()
        {
            Append(_command.FontWidth.Normal());
        }

        public void DoubleWidth2()
        {
            Append(_command.FontWidth.DoubleWidth2());
        }

        public void DoubleWidth3()
        {
            Append(_command.FontWidth.DoubleWidth3());
        }

        public void DoubleWidth4()
        {
            Append(_command.FontWidth.DoubleWidth4());
        }
        #endregion

        #region Alignment

        public void AlignLeft()
        {
            Append(_command.Alignment.Left());
        }

        public void AlignRight()
        {
            Append(_command.Alignment.Right());
        }

        public void AlignCenter()
        {
            Append(_command.Alignment.Center());
        }

        #endregion

        #region PaperCut

        public void FullPaperCut()
        {
            Append(_command.PaperCut.Full());
        }

        public void PartialPaperCut()
        {
            Append(_command.PaperCut.Partial());
        }

        #endregion

        #region Drawer
        public void OpenDrawer()
        {
            Append(_command.Drawer.Open());
        }

        #endregion

        #region QrCode

        public void QrCode(string qrData)
        {
            Append(_command.QrCode.Print(qrData));
        }

        public void QrCode(string qrData, QrCodeSize qrCodeSize)
        {
            Append(_command.QrCode.Print(qrData, qrCodeSize));
        }

        #endregion

        #region BarCode

        public void Code128(string code)
        {
            Append(_command.BarCode.Code128(code));
        }

        public void Code39(string code)
        {
            Append(_command.BarCode.Code39(code));
        }

        public void Ean13(string code)
        {
            Append(_command.BarCode.Ean13(code));
        }

        #endregion

        #region InitializePrint

        public void InitializePrint()
        {
            RawPrinterHelper.SendBytesToPrinter(_printerName, _command.InitializePrint.Initialize());
        }

        #endregion

        #endregion
    }
}