using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Management;
using System.Windows.Forms;

namespace SFPOS.Common
{
    public class PrintUtility
    {
        public static DataTable dt = new DataTable();
        public static decimal Balance = 0;
        public static decimal FSEligibleVoidAmt = 0;
        public static decimal TaxableAmount = 0;
        public static bool RePrint = false;
        public static bool Refund = false;
        private static int _LineCount = 0;
        private static int _row = 0;
        private static decimal totalAmtforFS = 0;
        private static decimal ItemCount = 0;
       
        #region PrintReceiptForEntry
        public static void PrintReceiptForCustomer()
        {
            try
            {
                if (CheckMyPrinter())
                {
                    PrintDocument recordDoc = new PrintDocument();
                    recordDoc.DocumentName = "Customer Receipt";
                    recordDoc.PrintPage += new PrintPageEventHandler(PrintCustomerReceipt);
                    recordDoc.PrintController = new StandardPrintController();
                    PrinterSettings ps = new PrinterSettings();
                    ps.PrinterName = "EPSON TM-H6000IV Receipt";
                    //ps.PrinterName = XMLData.PrinterName;
                    recordDoc.PrinterSettings = ps;
                    recordDoc.Print();
                    recordDoc.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Error >> " + ex.Message + ex.LineNumber());
            }
        }

        private static void PrintCustomerReceipt(object sender, PrintPageEventArgs e)
        {
            float x = 0;
            float y = 0;
            float width = 280;
            float height = 0;

            Font drawFontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
            Font drawFontArial9Regular = new Font("Arial", 9, FontStyle.Regular);
            Font drawFontArial8Regular = new Font("Arial", 8, FontStyle.Regular);

            SolidBrush drawBrush = new SolidBrush(Color.Black);

            #region SETFORMAT FOR STRING
            StringFormat drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            StringFormat drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            StringFormat drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            #endregion

            string text = "";
            float lineHeight = (drawFontArial12Bold.GetHeight(e.Graphics) + drawFontArial10Regular.GetHeight(e.Graphics) + drawFontArial9Regular.GetHeight(e.Graphics) + drawFontArial8Regular.GetHeight(e.Graphics));

            if (_row == 0)
            {
                #region REPRINT
                if (RePrint == true)
                {
                    text = "=== DUPLICATE CUSTOMER COPY ===";
                    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;
                }
                #endregion

                #region REFUND
                if (Refund == true)
                {
                    text = "=== REFUND ===";
                    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;
                }
                #endregion

                #region LOGO
                using (Image logo = Image.FromFile(Application.StartupPath + "\\SF_logo_a.png"))
                {
                    e.Graphics.DrawImage(logo, new Point(30, 20));
                }
                if(Refund == true || RePrint == true)
                {
                    y += 70;
                }
                else
                {
                    y += 80;
                }
                #endregion

                #region HEADER
                text = dt.Rows[0]["StoreName"].ToString();
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height; _LineCount++;

                text = dt.Rows[0]["SMAddress"].ToString() + "., " + dt.Rows[0]["SAddress2"].ToString();
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;

                text = "TEL: " + dt.Rows[0]["SPhone"].ToString() + "  " + "FAX: " + dt.Rows[0]["SFax"].ToString();
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;

                text = "";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;

                text = DateTime.Now.ToString();
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;

                text = "TRANS #: " + dt.Rows[0]["OrdNo"].ToString().Replace("TS", "");
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;

                text = "CASHIER: " + dt.Rows[0]["FirstName"].ToString();
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;

                text = "                                                        ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;
                #endregion
            }

            #region PRODUCT LIST

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                row = _row;
                if (row < dt.Rows.Count)
                {
                    if (_LineCount % 50 == 0)
                    {
                        e.HasMorePages = true;
                        _LineCount = 2;
                        return;
                    }
                    if (Convert.ToBoolean(dt.Rows[row]["IsScale"].ToString()) == true)
                    {
                        if (Convert.ToBoolean(dt.Rows[row]["DiscountApplyed"].ToString()) == true)
                        {
                            text = "GROUP DISCOUNT INCULDED";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;
                        }

                        text = dt.Rows[row]["Quantity"].ToString() + " Lb @ $ " + GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString()) + "/Lb";
                        e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;

                        if (dt.Rows[row]["ProductName"].ToString().Length > 25)
                        {
                            text = dt.Rows[row]["ProductName"].ToString().Substring(0, 25);
                        }
                        else
                        {
                            text = dt.Rows[row]["ProductName"].ToString();
                        }
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                        #region SALEPRICE & ABBRIVIATIONS
                        if (Convert.ToBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true && Convert.ToBoolean(dt.Rows[row]["IsTax"].ToString().ToLower()) == true)
                        {
                            text = GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + "  " + "TF";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                            if (LoginInfo.IsFSClicked == true)
                            {
                                totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["finalPrice"].ToString()).ToString("0.00"));
                            }
                        }
                        else if (Convert.ToBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == false)
                        {
                            text = GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + "  " + "FA";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        }
                        else
                        {
                            text = GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + "  " + "FC";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                            if (Convert.ToBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true)
                            {
                                if (LoginInfo.IsFSClicked == true)
                                {
                                    totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(text.Replace("FC", "")).ToString("0.00"));
                                }
                            }
                        }
                        #endregion
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                        ItemCount += 1;
                    }
                    else
                    {
                        if (Convert.ToBoolean(dt.Rows[row]["DiscountApplyed"].ToString()) == true)
                        {
                            text = "GROUP DISCOUNT INCLUDED";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                        }
                        string s = dt.Rows[row]["Quantity"].ToString();
                        string[] parts = s.Split('.');
                        int i1 = int.Parse(parts[0]);
                        int i2 = int.Parse(parts[1]);

                        if (i2 > 0)
                        {
                            text = dt.Rows[row]["Quantity"].ToString() + " @ $ " + GetDisplayAmount(GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString()));
                            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;
                        }
                        else if (Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) != 1)
                        {
                            text = Convert.ToInt32(Math.Floor(Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))) + " @ $ " + GetDisplayAmount(dt.Rows[row]["SellPrice"].ToString());
                            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;
                        }
       
                        if (Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()) < 0)
                        {
                            ItemCount += Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString());
                        }
                        else
                        {
                            ItemCount += 1;
                        }

                        if (dt.Rows[row]["ProductName"].ToString().Length > 25)
                        {
                            text = dt.Rows[row]["ProductName"].ToString().Substring(0, 25);
                        }
                        else
                        {
                            text = dt.Rows[row]["ProductName"].ToString();
                        }

                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                        #region SALEPRICE & ABBRIVIATIONS
                        if (Convert.ToBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true && Convert.ToBoolean(dt.Rows[row]["IsTax"].ToString().ToLower()) == true)
                        {
                            text = GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + "  " + "TF";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                            if (LoginInfo.IsFSClicked == true)
                            {
                                totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["finalPrice"].ToString()).ToString("0.00"));
                            }
                        }
                        else if (Convert.ToBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true)
                        {
                            text = GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + "     " + "F";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                            if (LoginInfo.IsFSClicked == true)
                            {
                                totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["finalPrice"].ToString()).ToString("0.00"));
                            }
                        }
                        else if (Convert.ToBoolean(dt.Rows[row]["IsTax"].ToString().ToLower()) == true)
                        {
                            text = GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + "     " + "T";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        }
                        else
                        {
                            text = GetDisplayAmount(dt.Rows[row]["finalPrice"].ToString()) + "  " + "FC";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        }
                        #endregion
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                   _row++;
                }
            }

            #endregion

            #region PRICE LIST
            text = "                                                        ";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

            text = "SUBTOTAL";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
            text = GetDisplayAmount(dt.Rows[0]["GrossAmount"].ToString());
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

            if (totalAmtforFS != 0)
            {
                text = "FS ELIGIBLE" + "                " + GetDisplayAmount(totalAmtforFS.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;

                if (FSEligibleVoidAmt > 0)
                {
                    text = "TAX EXEMPTED" + "           " + GetDisplayAmount(FSEligibleVoidAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;
                }
            }
            if (LoginInfo.IsFSVoidtax == false)
            {
                if (TaxableAmount != 0)
                {
                    text = "TAXABLE AMOUNT" + "    " + GetDisplayAmount(TaxableAmount.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height; _LineCount++;
                }
            }

            if (Convert.ToDecimal(dt.Rows[0]["TaxAmount"]) != 0)
            {
                text = "TAX ";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = GetDisplayAmount(dt.Rows[0]["TaxAmount"].ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
            }

            text = "TOTAL";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
            text = GetDisplayAmount((Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())).ToString());
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

            decimal remainingAmttoPay = 0;
            #endregion

            #region CASH/CREDIT/CHECK COMBINATIONS
            if (LoginInfo.PaymentType == 1)
            {
                if (totalAmtforFS != 0 && LoginInfo.CheckAmt != 0 && LoginInfo.CreditAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CreditAmt - LoginInfo.FSTotal - LoginInfo.CheckAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CheckAmt != 0 && LoginInfo.CreditAmt != 0)
                {
                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CreditAmt - LoginInfo.CheckAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0 && LoginInfo.CheckAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CheckAmt - LoginInfo.FSTotal;

                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0 && LoginInfo.CreditAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CreditAmt - LoginInfo.FSTotal;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    if (Balance > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(Balance.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CheckAmt != 0)
                {

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CheckAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CreditAmt != 0)
                {
                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CreditAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }

                text = "CASH";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                text = "CHANGE";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = GetDisplayAmount(LoginInfo.Change.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
            }

            else if (LoginInfo.PaymentType == 2)
            {
                if (totalAmtforFS != 0 && LoginInfo.CashAmt != 0 && LoginInfo.CheckAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CheckAmt - LoginInfo.CashAmt - LoginInfo.FSTotal;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CashAmt != 0 && LoginInfo.CheckAmt != 0)
                {
                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CheckAmt - LoginInfo.CashAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0 && LoginInfo.CashAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CashAmt - LoginInfo.FSTotal;

                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0 && LoginInfo.CheckAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CheckAmt - LoginInfo.FSTotal;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    if (Balance > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(Balance.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CashAmt != 0)
                {
                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CashAmt;
                    if (Balance > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CheckAmt != 0)
                {

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CheckAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                text = "CREDIT/DEBIT CARD";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                text = "CHANGE";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = GetDisplayAmount(LoginInfo.Change.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
            }

            else if (LoginInfo.PaymentType == 3)
            {
                if (totalAmtforFS != 0 && LoginInfo.CashAmt != 0 && LoginInfo.CreditAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CreditAmt - LoginInfo.FSTotal - LoginInfo.CashAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CashAmt != 0 && LoginInfo.CreditAmt != 0)
                {
                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CreditAmt - LoginInfo.CashAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0 && LoginInfo.CashAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CashAmt - LoginInfo.FSTotal;

                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0 && LoginInfo.CreditAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CreditAmt - LoginInfo.FSTotal;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (totalAmtforFS != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    if (Balance > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(Balance.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CashAmt != 0)
                {
                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CashAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CashAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }
                else if (LoginInfo.CreditAmt != 0)
                {
                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = GetDisplayAmount(LoginInfo.CreditAmt.ToString());
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["GrossAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - LoginInfo.CreditAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = GetDisplayAmount(remainingAmttoPay.ToString());
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                    }
                }

                text = "CHECK";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);

                text = GetDisplayAmount(LoginInfo.CheckAmt.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
                text = "CHANGE";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = GetDisplayAmount(LoginInfo.Change.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
            }

            else if (LoginInfo.PaymentType == 4)
            {
                text = "FOOD STAMPS";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = GetDisplayAmount(LoginInfo.FSTotal.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                text = "CHANGE";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = GetDisplayAmount(LoginInfo.Change.ToString());
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
            }


            text = "                                                        ";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;
            #endregion

            if (LoginInfo.PaymentType == 5)
            {
                #region TRANSACTION SUSPEND
                text = "TRANSACTION SUSPENDED BY: " + ManagerAction.ManagerName.ToUpper();
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                text = "                                                        ";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                #endregion

                #region BARCODE
                string barCode = dt.Rows[0]["TransSuspendCode"].ToString().ToLower();

                Bitmap bitm = new Bitmap(barCode.Length * 45, 160);
                using (Graphics graphic = Graphics.FromImage(bitm))
                {
                    Font newfont = new Font("IDAutomationHC39M", 12);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush black = new SolidBrush(Color.Black);
                    SolidBrush white = new SolidBrush(Color.White);
                    e.Graphics.FillRectangle(white, Convert.ToInt32(x), Convert.ToInt32(y + 10), bitm.Width, bitm.Height);
                    e.Graphics.DrawString("*" + barCode + "*", newfont, black, Convert.ToInt32(x), Convert.ToInt32(y + 10));
                }

                y += 50;

                text = "                                                        ";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                #endregion
            }
            else
            {
                #region FOOTER
                text = "ITEM COUNT: " + Convert.ToInt32(Math.Floor(ItemCount));
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                text = "                                                        ";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height; _LineCount++;

                string[] Disclaimer = LoginInfo.StoreDisclaimer.ToLower().Split('\n');
                if (Disclaimer != null)
                {
                    for (int i = 0; i < Disclaimer.Length; i++)
                    {
                        text = Disclaimer[i].Replace("\\n", ""); 
                        e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                    }
                }

                text = "";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                #endregion
            }

            _row = 0;
            _LineCount = 0;
            totalAmtforFS = 0;
            ItemCount = 0;
        }

        public static bool CheckMyPrinter()
        {
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_Printer where Name = 'EPSON TM-H6000IV Receipt'");

            string printerName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                printerName = printer["Name"].ToString().ToUpper();
                if (printerName.Equals((@"EPSON TM-H6000IV Receipt").ToUpper()))
                {
                    if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static string GetDisplayAmount(string Amount)
        {
            //return "$ " + String.Format("{0:0.00}", Amount);
            return Convert.ToDouble(Amount).ToString("N2");
        }
        #endregion
    }
}
