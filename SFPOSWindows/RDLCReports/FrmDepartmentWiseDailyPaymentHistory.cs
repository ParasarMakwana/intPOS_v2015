using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SFPOS.Entities.MasterDataClasses;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmDepartmentWiseDailyPaymentHistory : MetroForm
    {
        DepartmentWiseSaleService _DepartmentWiseSaleService = new DepartmentWiseSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmDepartmentWiseDailyPaymentHistory()
        {
            InitializeComponent();
        }

        private void FrmDepartmentWiseDailyPaymentHistory_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = DepartmentWiseDailyPayment();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CounterWisePaymentHistory", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CounterWisePaymentHistory.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();
        }
        public DataSet DepartmentWiseDailyPayment()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<DepartmentWiseDailyPaymentModel> lstDepartmentWiseDailyPayment = _DepartmentWiseSaleService.DepartmentWiseDailyPayment();

                dt.Columns.Add("CounterNo", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns.Add("CashAmount", typeof(decimal));
                dt.Columns.Add("CheckAmount", typeof(decimal));
                dt.Columns.Add("CreditCardAmount", typeof(decimal));
                dt.Columns.Add("FoodStampAmount", typeof(decimal));
                dt.Columns.Add("CounterIP", typeof(string));

                foreach (var item in lstDepartmentWiseDailyPayment)
                {
                    DataRow dr = dt.NewRow();
                    //dr["CounterNo"] = item.CounterNo;
                    //dr["SalesAmount"] = item.SalesAmount;
                    //dr["RefundAmount"] = item.RefundAmount;
                    //dr["CashAmount"] = item.CashAmount;
                    //dr["CheckAmount"] = item.CheckAmount;
                    //dr["CreditCardAmount"] = item.CreditCardAmount;
                    //dr["FoodStampAmount"] = item.FoodStampAmount;
                    //dr["CounterIP"] = item.CounterIP;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmDepartmentWiseDailyPaymentHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
