using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmEmployeeInvoiceWiseDailyPaymentHistory : MetroForm
    {
        EmloyeeSaleService _EmloyeeSaleService = new EmloyeeSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        public FrmEmployeeInvoiceWiseDailyPaymentHistory()
        {
            InitializeComponent();
        }

        private void FrmEmployeeInvoiceWiseDailyPaymentHistory_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = EmployeeInvoiceWiseDailyPayment();
            reportViewer1.LocalReport.DataSources.Clear(); 
             ReportDataSource rds = new ReportDataSource("EmployeeInvoiceWiseDailyPaymentHistory", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"EmployeeInvoiceWiseDailyPaymentHistory.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();
        }
        public DataSet EmployeeInvoiceWiseDailyPayment()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<EmployeeInvoiceWiseDailyPaymentModel> lstEmployeeInvoiceWiseDailyPayment = _EmloyeeSaleService.EmployeeInvoiceWiseDailyPayment();
                dt.Columns.Add("EmployeeID", typeof(string));
                dt.Columns.Add("OrdNo", typeof(string));
                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns.Add("CashAmount", typeof(decimal));
                dt.Columns.Add("CheckAmount", typeof(decimal));
                dt.Columns.Add("CreditCardAmount", typeof(decimal));
                dt.Columns.Add("FoodStampAmount", typeof(decimal));
                dt.Columns.Add("CancelledAmount", typeof(decimal));
                foreach (var item in lstEmployeeInvoiceWiseDailyPayment)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmployeeID"] = item.EmployeeID;
                    dr["TotalAmount"] = item.TotalAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["RefundAmount"] = item.RefundAmount;
                    dr["CashAmount"] = item.CashAmount;
                    dr["CheckAmount"] = item.CheckAmount;
                    dr["CreditCardAmount"] = item.CreditCardAmount;
                    dr["FoodStampAmount"] = item.FoodStampAmount;
                    dr["OrdNo"] = item.OrdNo;
                    dr["EmpName"] = item.EmpName;
                    dr["CancelledAmount"] = item.CancelledAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployeeInvoiceWiseDailyPayment + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
