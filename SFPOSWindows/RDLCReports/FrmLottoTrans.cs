using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
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
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.BAL.Frontend;
using SFPOS.DAL;
using SFPOS.Entities.FrontEnd;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmLottoTrans : MetroForm
    {
        LottoService _LottoService = new LottoService();
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmLottoTrans()
        {
            InitializeComponent();
        }

        private void FrmLottoSalesAndPayoutTrans_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = DepartmentWiseSaleHistory(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("LottoTrans", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"LottoTrans.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet DepartmentWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<LottoTotalTrans_ResultModel> lstGetLottoTrans = new List<LottoTotalTrans_ResultModel>();

                switch (FilterVal)
                {
                    case "TODAY":
                        lstGetLottoTrans = _LottoService.GetLottoSalesAndPayoutTrans(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstGetLottoTrans = _LottoService.GetLottoSalesAndPayoutTrans(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstGetLottoTrans = _LottoService.GetLottoSalesAndPayoutTrans(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstGetLottoTrans = _LottoService.GetLottoSalesAndPayoutTrans(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstGetLottoTrans = _LottoService.GetLottoSalesAndPayoutTrans(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("PayoutAmount", typeof(decimal));
                dt.Columns.Add("CreatedDate", typeof(DateTime));

                foreach (var item in lstGetLottoTrans)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmpName"] = item.EmpName;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["PayoutAmount"] = item.PayoutAmount;
                    dr["CreatedDate"] = item.CreatedDate;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmLottoTrans + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }

      
    }
}
