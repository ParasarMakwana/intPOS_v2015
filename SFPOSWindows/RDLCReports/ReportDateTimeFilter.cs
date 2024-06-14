using MetroFramework.Forms;
using System;
using System.Globalization;
using System.Windows;

namespace SFPOSWindows.RDLCReports
{
    public partial class ReportDateTimeFilter : MetroForm
    {
        public int FormNo = 0;
        public DateTime StartDate;
        public DateTime EndDate;
        public string FilterVal = "";
        public int FilterMonth = 0;
        public ReportDateTimeFilter()
        {
            InitializeComponent();
            filter();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                StartDate = dtStartDate.Value;
                EndDate = dtEndDate.Value;
                if (radioTodayfilter.Checked)
                {
                    FilterVal = "DAILY";
                }
                //else if (radioYesterdayfilter.Checked)
                //{
                //    FilterVal = "YESTERDAY";
                //}
                else if (radioMonthfilter.Checked)
                {
                    FilterVal = "MONTH";
                    if (cmbMonth.SelectedItem == null)
                    {
                        ClsCommon.MsgBox("Information", "Please select month.!", false);
                        return;
                    }
                    else
                    {
                        FilterMonth = DateTime.ParseExact((string)cmbMonth.SelectedItem, "MMMM", CultureInfo.InvariantCulture).Month;
                    }
                }
                else if (radioYearfilter.Checked)
                {
                    FilterVal = "YEAR";
                }
                else if (radioCustomfilter.Checked)
                {
                    FilterVal = "YEAR";
                }
                this.Close();
                formOpen(FormNo);
            }
            catch (Exception ex)
            {

            }
        }

        public void formOpen(int FormNo)
        {
            try
            {
                switch (FormNo)
                {
                    case 1:
                        FrmRegisterSaleTotalByTrans objFrmRegisterSaleTotalByTrans = new FrmRegisterSaleTotalByTrans();
                        objFrmRegisterSaleTotalByTrans.FromDate = StartDate;
                        objFrmRegisterSaleTotalByTrans.ToDate = EndDate;
                        objFrmRegisterSaleTotalByTrans.FilterVal = FilterVal;
                        objFrmRegisterSaleTotalByTrans.monthNumber = FilterMonth;
                        objFrmRegisterSaleTotalByTrans.Show();
                        objFrmRegisterSaleTotalByTrans.TopMost = true;
                        objFrmRegisterSaleTotalByTrans.BringToFront();
                        objFrmRegisterSaleTotalByTrans.Focus();
                        break;
                    case 2:
                        FrmRegisterSaleTotal objFrmRegisterSaleTotal = new FrmRegisterSaleTotal();
                        objFrmRegisterSaleTotal.FromDate = StartDate;
                        objFrmRegisterSaleTotal.ToDate = EndDate;
                        objFrmRegisterSaleTotal.FilterVal = FilterVal;
                        objFrmRegisterSaleTotal.monthNumber = FilterMonth;
                        objFrmRegisterSaleTotal.Show();
                        objFrmRegisterSaleTotal.TopMost = true;
                        break;
                    case 3:
                        FrmProductWiseSaleHistory objFrmProductWiseSaleHistory = new FrmProductWiseSaleHistory();
                        objFrmProductWiseSaleHistory.FromDate = StartDate;
                        objFrmProductWiseSaleHistory.ToDate = EndDate;
                        objFrmProductWiseSaleHistory.FilterVal = FilterVal;
                        objFrmProductWiseSaleHistory.monthNumber = FilterMonth;
                        objFrmProductWiseSaleHistory.Show();
                        objFrmProductWiseSaleHistory.TopMost = true;
                        break;
                    case 4:
                        FrmDepartmentWiseSaleHistory objFrmDepartmentWiseSaleHistory = new FrmDepartmentWiseSaleHistory();
                        objFrmDepartmentWiseSaleHistory.FromDate = StartDate;
                        objFrmDepartmentWiseSaleHistory.ToDate = EndDate;
                        objFrmDepartmentWiseSaleHistory.FilterVal = FilterVal;
                        objFrmDepartmentWiseSaleHistory.monthNumber = FilterMonth;
                        objFrmDepartmentWiseSaleHistory.Show();
                        objFrmDepartmentWiseSaleHistory.TopMost = true;
                        break;
                    case 5:
                        FrmSectionWiseSaleHistory objFrmSectionWiseSaleHistory = new FrmSectionWiseSaleHistory();
                        objFrmSectionWiseSaleHistory.FromDate = StartDate;
                        objFrmSectionWiseSaleHistory.ToDate = EndDate;
                        objFrmSectionWiseSaleHistory.FilterVal = FilterVal;
                        objFrmSectionWiseSaleHistory.monthNumber = FilterMonth;
                        objFrmSectionWiseSaleHistory.Show();
                        objFrmSectionWiseSaleHistory.TopMost = true;
                        break;
                    case 6:
                        FrmCashierSaleTotal objFrmCashierSaleTotal = new FrmCashierSaleTotal();
                        objFrmCashierSaleTotal.FromDate = StartDate;
                        objFrmCashierSaleTotal.ToDate = EndDate;
                        objFrmCashierSaleTotal.FilterVal = FilterVal;
                        objFrmCashierSaleTotal.monthNumber = FilterMonth;
                        objFrmCashierSaleTotal.Show();
                        objFrmCashierSaleTotal.TopMost = true;
                        break;
                    case 7:
                        FrmCashierSaleTotalByTrans objFrmCashierSaleTotalByTrans = new FrmCashierSaleTotalByTrans();
                        objFrmCashierSaleTotalByTrans.FromDate = StartDate;
                        objFrmCashierSaleTotalByTrans.ToDate = EndDate;
                        objFrmCashierSaleTotalByTrans.FilterVal = FilterVal;
                        objFrmCashierSaleTotalByTrans.monthNumber = FilterMonth;
                        objFrmCashierSaleTotalByTrans.Show();
                        objFrmCashierSaleTotalByTrans.TopMost = true;
                        break;
                    case 8:
                        FrmCashierSaleStatus objFrmCashierSaleStatus = new FrmCashierSaleStatus();
                        objFrmCashierSaleStatus.FromDate = StartDate;
                        objFrmCashierSaleStatus.ToDate = EndDate;
                        objFrmCashierSaleStatus.FilterVal = FilterVal;
                        objFrmCashierSaleStatus.monthNumber = FilterMonth;
                        objFrmCashierSaleStatus.Show();
                        objFrmCashierSaleStatus.TopMost = true;
                        break;
                    case 9:
                        FrmCashierSaleStatusByTrans objFrmCashierSaleStatusByTrans = new FrmCashierSaleStatusByTrans();
                        objFrmCashierSaleStatusByTrans.FromDate = StartDate;
                        objFrmCashierSaleStatusByTrans.ToDate = EndDate;
                        objFrmCashierSaleStatusByTrans.FilterVal = FilterVal;
                        objFrmCashierSaleStatusByTrans.monthNumber = FilterMonth;
                        objFrmCashierSaleStatusByTrans.Show();
                        objFrmCashierSaleStatusByTrans.TopMost = true;
                        break;
                    case 10:
                        FrmTaxReport objFrmTaxReport = new FrmTaxReport();
                        objFrmTaxReport.FromDate = StartDate;
                        objFrmTaxReport.ToDate = EndDate;
                        objFrmTaxReport.FilterVal = FilterVal;
                        objFrmTaxReport.monthNumber = FilterMonth;
                        objFrmTaxReport.TopMost = true;
                        objFrmTaxReport.Show();
                        break;
                    case 11:
                        FrmRegisterSaleStatus objFrmRegisterSaleStatus = new FrmRegisterSaleStatus();
                        objFrmRegisterSaleStatus.FromDate = StartDate;
                        objFrmRegisterSaleStatus.ToDate = EndDate;
                        objFrmRegisterSaleStatus.FilterVal = FilterVal;
                        objFrmRegisterSaleStatus.monthNumber = FilterMonth;
                        objFrmRegisterSaleStatus.Show();
                        objFrmRegisterSaleStatus.TopMost = true;
                        break;
                    case 12:
                        FrmRegisterSaleStatusByTrans objFrmRegisterSaleStatusByTrans = new FrmRegisterSaleStatusByTrans();
                        objFrmRegisterSaleStatusByTrans.FromDate = StartDate;
                        objFrmRegisterSaleStatusByTrans.ToDate = EndDate;
                        objFrmRegisterSaleStatusByTrans.FilterVal = FilterVal;
                        objFrmRegisterSaleStatusByTrans.monthNumber = FilterMonth;
                        objFrmRegisterSaleStatusByTrans.Show();
                        objFrmRegisterSaleStatusByTrans.TopMost = true;
                        break;
                    case 14:
                        FrmLottoTrans objFrmLottoSalesAndPayoutTrans = new FrmLottoTrans();
                        objFrmLottoSalesAndPayoutTrans.FromDate = StartDate;
                        objFrmLottoSalesAndPayoutTrans.ToDate = EndDate;
                        objFrmLottoSalesAndPayoutTrans.FilterVal = FilterVal;
                        objFrmLottoSalesAndPayoutTrans.monthNumber = FilterMonth;
                        objFrmLottoSalesAndPayoutTrans.Show();
                        objFrmLottoSalesAndPayoutTrans.TopMost = true;
                        break;
                    case 15:
                        FrmSectionWiseTaxHistory objFrmSectionWiseTaxHistory = new FrmSectionWiseTaxHistory();
                        objFrmSectionWiseTaxHistory.FromDate = StartDate;
                        objFrmSectionWiseTaxHistory.ToDate = EndDate;
                        objFrmSectionWiseTaxHistory.FilterVal = FilterVal;
                        objFrmSectionWiseTaxHistory.monthNumber = FilterMonth;
                        objFrmSectionWiseTaxHistory.Show();
                        objFrmSectionWiseTaxHistory.TopMost = true;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dtStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioCustomfilter.Checked)
                {
                    dtEndDate.MinDate = dtStartDate.Value;
                    dtEndDate.MaxDate = dtStartDate.Value.AddYears(1).AddDays(-1);
                }
                else if (radioYearfilter.Checked)
                {
                    DateTime dt = Convert.ToDateTime(dtStartDate.Value);
                    int year = dt.Year;
                    dtStartDate.Text = new DateTime(year, 1, 1).ToString();
                    dtEndDate.Text = new DateTime(year, 12, 31).ToString();
                }
                else if (radioTodayfilter.Checked)
                {
                    dtEndDate.Value = dtStartDate.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void radioTodayfilter_CheckedChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void radioYesterdayfilter_CheckedChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void radioMonthfilter_CheckedChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void radioYearfilter_CheckedChanged(object sender, EventArgs e)
        {
            filter();
        }

        public void filter()
        {
            try
            {
                if (radioTodayfilter.Checked)
                {
                    FilterVal = "DAILY";
                    cmbMonth.Visible = false;
                    lblMonth.Visible = false;
                    dtStartDate.Enabled = true;
                    dtEndDate.Enabled = false;
                    dtStartDate.Value = DateTime.Now;
                    dtEndDate.Value = DateTime.Now;
                }
                //else if (radioYesterdayfilter.Checked)
                //{
                //    FilterVal = "YESTERDAY";
                //    cmbMonth.Visible = false;
                //    lblMonth.Visible = false;
                //    dtStartDate.Enabled = false;
                //    dtEndDate.Enabled = false;
                //    dtStartDate.Value = DateTime.Now.AddDays(-1);
                //    dtEndDate.Value = DateTime.Now.AddDays(-1);
                //}
                else if (radioMonthfilter.Checked)
                {
                    FilterVal = "MONTH";
                    cmbMonth.Visible = true;
                    lblMonth.Visible = true;
                    dtStartDate.Enabled = false;
                    dtEndDate.Enabled = false;
                }
                else if (radioYearfilter.Checked)
                {
                    FilterVal = "YEAR";

                    cmbMonth.Visible = false;
                    lblMonth.Visible = false;
                    //dtStartDate.Visible = true;
                    //dtEndDate.Visible = true;
                    dtStartDate.Enabled = true;
                    //dtEndDate.Enabled = false;
                    //lblEndDate.Visible = true;
                    //lblStartDate.Visible = true;
                    dtStartDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
                }
                else if (radioCustomfilter.Checked)
                {
                    FilterVal = "YEAR";
                    cmbMonth.Visible = false;
                    lblMonth.Visible = false;
                    dtStartDate.Value = DateTime.Now;
                    dtEndDate.Value = DateTime.Now;
                    dtStartDate.Enabled = true;
                    dtEndDate.Enabled = true;
                    //lblEndDate.Visible = true;
                    //lblStartDate.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FilterMonth = DateTime.ParseExact((string)cmbMonth.SelectedItem, "MMMM", CultureInfo.InvariantCulture).Month;
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, FilterMonth, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                dtStartDate.Value = firstDayOfMonth;
                dtEndDate.Value = lastDayOfMonth;
            }
            catch (Exception ex)
            {

            }
        }

        private void radioCustomfilter_CheckedChanged(object sender, EventArgs e)
        {
            filter();
        }


    }
}
