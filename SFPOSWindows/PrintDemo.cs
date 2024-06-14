using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows
{
    public partial class PrintDemo : Form
    {
        public long OrderID = 0;
        public static DataTable dt = new DataTable();
        public static int type = 0;
        public static decimal Balance = 0;
        public static decimal CashAmt = 0;
        public static decimal ChangeAmt = 0;
        public static decimal CheckAmt = 0;
        public static decimal CreditAmt = 0;
        public static bool RePrint = false;

        private static int _Line = 0;
        public PrintDemo()
        {
            InitializeComponent();
        }

        private void PrintDemo_Load(object sender, EventArgs e)
        {
            //ds = ReceiptDetailSP(OrderID);

            //PrintReceiptForEntry();
            //this.Close();
        }


        #region PrintReceiptForEntry

        public static void PrintReceiptForEntry()
        {

            PrintDocument recordDoc = new PrintDocument();

            recordDoc.DocumentName = "Customer Receipt";
            recordDoc.PrintPage += new PrintPageEventHandler(PrintEntryReceipt);
            recordDoc.PrintController = new StandardPrintController();
            _Line = 0;
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = XMLData.PrinterName;
            recordDoc.PrinterSettings = ps;
            recordDoc.Print();
            recordDoc.Dispose();
        }

        private static void PrintEntryReceipt(object sender, PrintPageEventArgs e)
        {

            float x = 0;
            float y = 0;
            float width = 280;
            float height = 0;
            decimal totalAmtforFS = 0;

            Font drawFontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
            Font drawFontArial9Regular = new Font("Arial", 9, FontStyle.Regular);
            Font drawFontArial8Regular = new Font("Arial", 8, FontStyle.Regular);

            SolidBrush drawBrush = new SolidBrush(Color.Black);

            #region setformat for string
            StringFormat drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            StringFormat drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            StringFormat drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            #endregion

            string text = "";
            Graphics g = e.Graphics;
            float lineHeight = (drawFontArial12Bold.GetHeight(e.Graphics) + drawFontArial10Regular.GetHeight(e.Graphics) + drawFontArial9Regular.GetHeight(e.Graphics) + drawFontArial8Regular.GetHeight(e.Graphics));
            //float yLineTop = e.MarginBounds.Top;

            #region reprint
            if (RePrint == true)
            {
                text = "=== DUPLICATE CUSTOMER COPY ===";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
            }
            #endregion

            #region Logo
            using (Image logo = Image.FromFile(Application.StartupPath + "\\SF_logo_a.png"))
            {
                e.Graphics.DrawImage(logo, new Point(30, 20));
            }
            y += 100;
            #endregion

            #region HEADER
            text = dt.Rows[0]["StoreName"].ToString();
            e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

            text = dt.Rows[0]["SMAddress"].ToString() + "., " + dt.Rows[0]["SAddress2"].ToString() + ",";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "TEL: " + dt.Rows[0]["SPhone"].ToString() + "  " + "FAX: " + dt.Rows[0]["SFax"].ToString();
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = DateTime.Now.ToString();
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "TRANS #: " + dt.Rows[0]["OrdNo"].ToString();
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "CASHIER: " + dt.Rows[0]["FirstName"].ToString();
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "                                                        ";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
            #endregion

            #region PRODUCT LIST
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                for (; _Line < 70; _Line++)
                {
                    if (y + lineHeight > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    if (Convert.ToBoolean(dt.Rows[row]["IsScale"].ToString()) == true)
                    {
                        if (Convert.ToBoolean(dt.Rows[row]["DiscountApplyed"].ToString()) == true)
                        {
                            text = "GROUP DISCOUNT INCULDED";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        }

                        text = dt.Rows[row]["Quantity"].ToString() + " Lb @ $" + dt.Rows[row]["SellPrice"].ToString() + "/Lb";
                        e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                        if (dt.Rows[row]["ProductName"].ToString().Length > 25)
                        {
                            text = dt.Rows[row]["ProductName"].ToString().Substring(0, 25);
                        }
                        else
                        {
                            text = dt.Rows[row]["ProductName"].ToString();
                        }
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                        #region IsFoodStamp
                        if (Convert.ToBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == false)
                        {
                            text = Convert.ToDecimal((Convert.ToDecimal(dt.Rows[row]["SellPrice"].ToString())) * (Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))).ToString("0.00") + "  " + "FA";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        }
                        else
                        {
                            text = Convert.ToDecimal((Convert.ToDecimal(dt.Rows[row]["SellPrice"].ToString())) * (Convert.ToDecimal(dt.Rows[row]["Quantity"].ToString()))).ToString("0.00") + "  " + "FC";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                            if (LoginInfo.IsFSClicked == true)
                            {
                                totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(text.Replace("FC", "")).ToString("0.00"));
                            }
                        }
                        #endregion
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                    else
                    {
                        if (Convert.ToBoolean(dt.Rows[row]["DiscountApplyed"].ToString()) == true)
                        {
                            text = "GROUP DISCOUNT INCLUDED";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
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

                        #region IsFoodStamp
                        if (Convert.ToBoolean(dt.Rows[row]["IsFoodStamp"].ToString().ToLower()) == true)
                        {
                            text = dt.Rows[row]["SellPrice"].ToString() + "     " + "F";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                            if (LoginInfo.IsFSClicked == true)
                            {
                                totalAmtforFS += Convert.ToDecimal(Convert.ToDecimal(dt.Rows[row]["SellPrice"].ToString()).ToString("0.00"));
                            }
                        }
                        else if (Convert.ToBoolean(dt.Rows[row]["IsTax"].ToString().ToLower()) == true)
                        {
                            text = dt.Rows[row]["SellPrice"].ToString() + "     " + "T";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        }
                        else
                        {
                            text = dt.Rows[row]["SellPrice"].ToString() + "  " + "FC";
                            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        }
                        #endregion
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }

                    y += lineHeight;
                }
            }
            e.HasMorePages = false;

            #endregion

            #region PRICE LIST
            text = "                                                        ";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "SUBTOTAL";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
            text = dt.Rows[0]["TotalAmount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            if (totalAmtforFS != 0)
            {
                text = "FS ELIGIBLE" + "                " + totalAmtforFS;
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
            }

            if (Convert.ToDecimal(dt.Rows[0]["TaxAmount"]) != 0)
            {
                text = "TAX ";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = dt.Rows[0]["TaxAmount"].ToString();
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
            }

            text = "TOTAL";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
            text = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())).ToString("0.00");
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            decimal remainingAmttoPay = 0;
            #endregion

            #region CASH/CREDIT/CHECK COMBINATIONS
            if (type == 1)
            {
                if (totalAmtforFS != 0 && CheckAmt != 0 && CreditAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CheckAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CreditAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CreditAmt - LoginInfo.FSTotal - CheckAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CheckAmt != 0 && CreditAmt != 0)
                {
                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CheckAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CreditAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CreditAmt - CheckAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0 && CheckAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CheckAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CheckAmt - LoginInfo.FSTotal;

                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0 && CreditAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CreditAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CreditAmt - LoginInfo.FSTotal;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    if (Balance > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = Balance.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CheckAmt != 0)
                {

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CheckAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CheckAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CreditAmt != 0)
                {
                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CreditAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CreditAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }

                text = "CASH";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = CashAmt.ToString("0.00");
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                text = "CHANGE";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = ChangeAmt.ToString("0.00");
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
            }

            else if (type == 2)
            {
                if (totalAmtforFS != 0 && CashAmt != 0 && CheckAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CashAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CheckAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CheckAmt - CashAmt - LoginInfo.FSTotal;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CashAmt != 0 && CheckAmt != 0)
                {
                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CashAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CheckAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CheckAmt - CashAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0 && CashAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CashAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CashAmt - LoginInfo.FSTotal;

                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0 && CheckAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CheckAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CheckAmt - LoginInfo.FSTotal;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    if (Balance > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = Balance.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CashAmt != 0)
                {
                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CashAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CashAmt;
                    if (Balance > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CheckAmt != 0)
                {

                    text = "CHECK";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CheckAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CheckAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                text = "CREDIT/DEBIT CARD";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                if (remainingAmttoPay > 0)
                {
                    text = remainingAmttoPay.ToString("0.00");
                }
                else
                {
                    text = Balance.ToString("0.00");
                }
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                text = "CHANGE";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = "0.00";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
            }

            else if (type == 3)
            {
                if (totalAmtforFS != 0 && CashAmt != 0 && CreditAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CashAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CreditAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CreditAmt - LoginInfo.FSTotal - CashAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CashAmt != 0 && CreditAmt != 0)
                {
                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CashAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CreditAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CreditAmt - CashAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0 && CashAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CashAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CashAmt - LoginInfo.FSTotal;

                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0 && CreditAmt != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CreditAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CreditAmt - LoginInfo.FSTotal;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (totalAmtforFS != 0)
                {
                    text = "FOOD STAMPS";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = LoginInfo.FSTotal.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    if (Balance > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = Balance.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CashAmt != 0)
                {
                    text = "CASH";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CashAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CashAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }
                else if (CreditAmt != 0)
                {
                    text = "CREDIT/DEBIT CARD";
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                    text = CreditAmt.ToString("0.00");
                    e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                    remainingAmttoPay = (Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) + Convert.ToDecimal(dt.Rows[0]["TaxAmount"].ToString())) - CreditAmt;
                    if (remainingAmttoPay > 0)
                    {
                        text = "BALANCE";
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                        text = remainingAmttoPay.ToString("0.00");
                        e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                        y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                    }
                }

                text = "CHECK";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                if (remainingAmttoPay > 0)
                {
                    text = remainingAmttoPay.ToString("0.00");
                }
                else
                {
                    text = CheckAmt.ToString("0.00");
                }
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
                text = "CHANGE";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = ChangeAmt.ToString("0.00");
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
            }

            else if (type == 4)
            {
                text = "FOOD STAMPS";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = LoginInfo.FSTotal.ToString("0.00");
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                text = "CHANGE";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(70, y, width, height), null);
                text = ChangeAmt.ToString("0.00");
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x - 23, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
            }


            text = "                                                        ";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;
            #endregion

            if (type == 5)
            {
                #region TRANSACTION SUSPEND
                text = "TRANSACTION SUSPENDED BY: " + ManagerAction.ManagerName.ToUpper();
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                text = "                                                        ";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                #endregion

                #region BarCode

                string barCode = dt.Rows[0]["TransSuspendCode"].ToString().ToLower() + "0";

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
                text = "ITEM COUNT: " + dt.Rows.Count.ToString();
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                text = "                                                        ";
                e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

                text = "Thank you for shopping at " + dt.Rows[0]["StoreName"].ToString().ToLower();
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                //text = dt.Rows[0]["StoreName"].ToString();
                //e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                //y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                text = "Refund/Exchange within 24 hrs. w/Receipt";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                text = "No Refund Or Exchange On Any Liquor";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                text = "All Electronics refundable/exchange";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                text = "within 2 weeks only. No exceptions.";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                text = "Have a Nice Day !!!!!!!!";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;

                text = "";
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
                #endregion
            }



        }

        #endregion
    }
}
