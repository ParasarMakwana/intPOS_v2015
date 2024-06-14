using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Management;
using System.Windows.Forms;

namespace SFPOS.Common
{
    public class PrintTotalOrder
    {
        public static DataTable dt = new DataTable();

        public static string StoreName = "";
        public static string CashierName = "";
        public static int VoidCount = 0;
        //public static int RefundCount = 0;
        //public static int CancelCount = 0;
        //public static decimal CancelAmount = 0;
        //public static decimal RefundAmount = 0;
        public static decimal VoidAmount = 0;
        public static void PrintReceiptForCashier()
        {
            try
            {
                if (CheckMyPrinter())
                {
                    PrintDocument recordDoc = new PrintDocument();

                    recordDoc.DocumentName = "Customer Receipt";
                    recordDoc.PrintPage += new PrintPageEventHandler(PrintEntryReceipt);
                    recordDoc.PrintController = new StandardPrintController();

                    PrinterSettings ps = new PrinterSettings();
                    ps.PrinterName = XMLData.PrinterName;
                    recordDoc.PrinterSettings = ps;
                    recordDoc.Print();
                    recordDoc.Dispose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Print Error");
            }
        }

        private static void PrintEntryReceipt(object sender, PrintPageEventArgs e)
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
            StringFormat drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            StringFormat drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            StringFormat drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            using (Image logo = Image.FromFile(Application.StartupPath + "\\SF_logo_a.png"))
            {
                e.Graphics.DrawImage(logo, new Point(30, 0));
            }
            y += 70;

            string text = "";
            e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

            text = StoreName;
            e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

            text = "CASHIER: " + CashierName;
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "LOGIN: " + Convert.ToDateTime(LoginInfo.LoginTime);
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "LOGOUT: " + DateTime.Now;
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "----------------------------------------------------------------";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            //text = RefundCount.ToString();
            text = dt.Rows[0]["RefundCount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);


            text = "REFUNDS";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), null);

            text = "$ " + dt.Rows[0]["RefundAmount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = VoidCount.ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

            text = "VOIDS";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), null);

            text = "$ " + VoidAmount;
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = dt.Rows[0]["CancelledCount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

            text = "CANCELS";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), null);

            text = "$ " + dt.Rows[0]["CancelledAmount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = dt.Rows[0]["OverridePriceCount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

            text = "PRICE OVERRIDE";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), null);

            text = "$ " + dt.Rows[0]["OverridePriceTotal"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "----------------------------------------------------------------";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "CASH";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), drawFormatLeft);

            //text = "$ " + (Convert.ToDecimal(dt.Rows[0]["CashAmount"].ToString() == null ? "0" : dt.Rows[0]["CashAmount"].ToString()) + RefundAmount);
            text = "$ " + Functions.GetDecimal(dt.Rows[0]["CashAmount"].ToString());
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "CHECK";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), drawFormatLeft);

            text = "$ " + dt.Rows[0]["CheckAmount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "CREDIT CARD";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), drawFormatLeft);

            text = "$ " + dt.Rows[0]["CreditCardAmount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "FOOD STAMPS";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), drawFormatLeft);

            text = "$ " + dt.Rows[0]["FoodStampAmount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "----------------------------------------------------------------";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "TOTAL";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), drawFormatLeft);

            text = "$ " + (Functions.GetDecimal(dt.Rows[0]["FoodStampAmount"].ToString())
                    + Functions.GetDecimal(dt.Rows[0]["CheckAmount"].ToString())
                    + Functions.GetDecimal(dt.Rows[0]["CashAmount"].ToString())
                    + Functions.GetDecimal(dt.Rows[0]["CreditCardAmount"].ToString()));
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;



            #region Lotto Trans
            text = "----------------------------------------------------------------";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;


            text = "LOTTO SALES";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), drawFormatLeft);

            text = "$ " + dt.Rows[0]["LottoSalesAmount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "LOTTO PAYOUT";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), drawFormatLeft);

            text = "$ " + dt.Rows[0]["LottoPayoutAmount"].ToString();
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "----------------------------------------------------------------";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "LOTTO TOTAL";
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(50, y, width, height), drawFormatLeft);

            text = "$ " + (Functions.GetDecimal(dt.Rows[0]["LottoSalesAmount"].ToString())
                    - Functions.GetDecimal(dt.Rows[0]["LottoPayoutAmount"].ToString()));
            e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;


            #endregion


            //text = "CHANGE AMOUNT";
            //e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

            //text = dt.Rows[0]["ChangeAmount"].ToString();
            //e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            //y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            //text = "REFUND AMOUNT";
            //e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

            //text = dt.Rows[0]["RefundAmount"].ToString();
            //e.Graphics.DrawString(text, drawFontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            //y += e.Graphics.MeasureString(text, drawFontArial9Regular).Height;

            text = "**********";
            e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial8Regular).Height;
        }


        public static bool CheckMyPrinter()
        {
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            string printerName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                printerName = printer["Name"].ToString().ToUpper();
                if (printerName.Equals((XMLData.PrinterName).ToUpper()))
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
    }
}
