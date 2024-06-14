using SFPOSWindows.MasterForm;
using SFPOSWindows.RDLCReports;
using System;
using System.Windows.Forms;

namespace SFPOSWindows.MenuForm
{
    public partial class MenuReports : Form
    {
        bool IsSalesHistory = false;
        bool IsPaymentHistory = false;
        bool IsLabelPrint = false;
        bool IsLabelPrint_R = false;
        public MenuReports()
        {
            InitializeComponent();
        }
        private void btnCounterWiseSalesHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 2;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnCounterInvoiceWiseSalesHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 1;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnEmployeeInvoiceWiseSalesHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 7;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnEmployeeWiseSalesHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 6;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnDepartmentWiseSalesHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 4;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnProductWiseSalesHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 3;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnSectionWiseSalesHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 5;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            if (IsSalesHistory == false)
            {
                SalesHistoryPanel.Height = 460;
                PaymentHistoryPanel.Height = 35;
                PanelLabelPrint.Height = 35;
                IsSalesHistory = true;
                IsPaymentHistory = false;
                IsLabelPrint = false;
                SalesHistoryPanel.Location = new System.Drawing.Point(25, 67);
                PaymentHistoryPanel.Location = new System.Drawing.Point(25, 520);
                PanelLabelPrint.Location = new System.Drawing.Point(25, 469);
                PanelLabelPrint_R.Location = new System.Drawing.Point(25, 510);
            }
            else if (IsSalesHistory == true)
            {
                IsSalesHistory = false;
                IsPaymentHistory = false;
                IsLabelPrint = false;
                SalesHistoryPanel.Height = 35;
                PaymentHistoryPanel.Height = 35;
                SalesHistoryPanel.Location = new System.Drawing.Point(25, 67);
                PaymentHistoryPanel.Location = new System.Drawing.Point(25, 108);
                PanelLabelPrint.Location = new System.Drawing.Point(25, 150);
                PanelLabelPrint_R.Location = new System.Drawing.Point(25, 192);
            }
        }

        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            if (IsPaymentHistory == false)
            {
                SalesHistoryPanel.Height = 35;
                PaymentHistoryPanel.Height = 241;
                IsSalesHistory = false;
                IsLabelPrint = false;
                IsPaymentHistory = true;
                SalesHistoryPanel.Location = new System.Drawing.Point(25, 67);
                PaymentHistoryPanel.Location = new System.Drawing.Point(25, 108);
                PanelLabelPrint.Location = new System.Drawing.Point(25, 310);
                PanelLabelPrint_R.Location = new System.Drawing.Point(25,352);
            }
            else if (IsPaymentHistory == true)
            {
                IsSalesHistory = false;
                IsPaymentHistory = false;
                IsLabelPrint = false;
                SalesHistoryPanel.Height = 35;
                PaymentHistoryPanel.Height = 35;
                SalesHistoryPanel.Location = new System.Drawing.Point(25, 67);
                PaymentHistoryPanel.Location = new System.Drawing.Point(25, 108);
                PanelLabelPrint.Location = new System.Drawing.Point(25, 150);
                PanelLabelPrint_R.Location = new System.Drawing.Point(25, 192);
            }
        }
        private void btnLabelPrint_Click(object sender, EventArgs e)
        {
            IsLabelPrint = true;
            //FrmLabelPrintNew objFrmLabelPrint = new FrmLabelPrintNew();
            //objFrmLabelPrint.ShowDialog();
        }
        private void btnCounterWisePaymentHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 11;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnCounterInvoiceWisePaymentHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 12;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }


        private void btnEmployeeWisePayment_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 8;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnEmployeeInvoiceWisePayment_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 9;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnTaxHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 10;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnLabelPrint_R_Click(object sender, EventArgs e)
        {
            IsLabelPrint_R = true;
            FrmLabelPrintNew_R objFrmLabelPrint_R = new FrmLabelPrintNew_R();
            objFrmLabelPrint_R.ShowDialog();
        }

        private void btnProductMovement_Click(object sender, EventArgs e)
        {
            ReportUPCCodeFilter objReportDateTimeFilter = new ReportUPCCodeFilter();
            objReportDateTimeFilter.FormNo = 13;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnLottoTransaction_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 14;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }

        private void btnSectionWiseTaxHistory_Click(object sender, EventArgs e)
        {
            ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
            objReportDateTimeFilter.FormNo = 15;
            objReportDateTimeFilter.Show();
            objReportDateTimeFilter.TopMost = true;
            objReportDateTimeFilter.BringToFront();
            objReportDateTimeFilter.Focus();
        }
    }
}
