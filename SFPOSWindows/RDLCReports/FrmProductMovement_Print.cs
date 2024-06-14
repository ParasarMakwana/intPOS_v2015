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
    public partial class FrmProductMovement_Print : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        CashierSaleService _CashierSaleService = new CashierSaleService();
        ProductMovementService _ProductMovementService = new ProductMovementService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        public FrmProductMovement_Print()
        {
            InitializeComponent();
        }

        private void FrmEmployeeWisePaymentHistory_Load(object sender, EventArgs e)
        {
            try
            {
                this.BringToFront();
                this.reportViewer1.RefreshReport();
                DataSet dt = new DataSet();
                dt = CashierSaleStatus(FromDate, ToDate);
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("ProductInfo", dt.Tables[0]);
                ReportDataSource rds1 = new ReportDataSource("Week", dt.Tables[1]);
                ReportDataSource rds2 = new ReportDataSource("Month", dt.Tables[2]);
                ReportDataSource rds3 = new ReportDataSource("Year", dt.Tables[3]);
                ReportDataSource rds4 = new ReportDataSource("Days", dt.Tables[4]);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.LocalReport.DataSources.Add(rds1);
                reportViewer1.LocalReport.DataSources.Add(rds2);
                reportViewer1.LocalReport.DataSources.Add(rds3);
                reportViewer1.LocalReport.DataSources.Add(rds4);

                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"ProductMovement_Print.rdlc");

                ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
                ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
                ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
                ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());

                reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                string st = ex.Message.ToString();
            }
        }

        public DataSet CashierSaleStatus(DateTime FromDate, DateTime ToDate)
        {
            string UPCCode =ClsCommon.GetFullUPCCode(FilterVal);
            DataSet ds = new DataSet();
            try
            {
                List<CashierSaleStatusModel> lstCashierSaleStatus = new List<CashierSaleStatusModel>();
                List<ProductMovement_ResultModel> lstProductMovement_ResultModel = new List<ProductMovement_ResultModel>();

                if (UPCCode != "")
                {
                    lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(UPCCode, DateTime.Now, DateTime.Now);
                    if (lstProductMovement_ResultModel.Count == 0 && lstProductMovement_ResultModel == null)
                    {
                        UPCCode = Functions.GetUPC_E(FilterVal);
                        lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(UPCCode, DateTime.Now, DateTime.Now);
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("ProductID", typeof(long));
                dt.Columns.Add("UPCCode", typeof(string));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("ProductVendorID", typeof(long));
                dt.Columns.Add("ItemCode", typeof(string));
                dt.Columns.Add("UnitCost", typeof(decimal));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("VendorName", typeof(string));

                foreach (var item in lstProductMovement_ResultModel)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductID"] = item.ProductID;
                    dr["UPCCode"] = item.UPCCode;
                    dr["ProductName"] = item.ProductName;
                    dr["ProductVendorID"] = item.ProductVendorID;
                    dr["ItemCode"] = item.ItemCode;
                    dr["UnitCost"] = item.UnitCost;
                    dr["Price"] = item.Price;
                    dr["VendorName"] = item.VendorName;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);

                #region Weeks

                lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(UPCCode, 2);
                if (lstProductMovement_ResultModel != null && lstProductMovement_ResultModel.Count > 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("datevalue", typeof(string));
                    dt.Columns.Add("Qty", typeof(decimal));

                    foreach (var item in lstProductMovement_ResultModel)
                    {
                        DataRow dr = dt.NewRow();
                        int Month = item.datevalue.Value.Month;
                        int Day = item.datevalue.Value.Day;

                        dr["datevalue"] = Month + "/" + Day; ;
                        dr["Qty"] = item.Qty;

                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                }
                #endregion

                #region Months

                lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(UPCCode, 1);
                if (lstProductMovement_ResultModel != null && lstProductMovement_ResultModel.Count > 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("datevalue", typeof(string));
                    dt.Columns.Add("Qty", typeof(decimal));

                    foreach (var item in lstProductMovement_ResultModel)
                    {
                        DataRow dr = dt.NewRow();
                        string MonthName = item.datevalue.Value.ToString("MMM");
                        int yearName = item.datevalue.Value.Year;

                        dr["datevalue"] = MonthName + "-" + yearName;
                        dr["Qty"] = item.Qty;

                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                }
                #endregion

                #region Years

                lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(UPCCode, 3);
                if (lstProductMovement_ResultModel != null && lstProductMovement_ResultModel.Count > 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("datevalue", typeof(string));
                    dt.Columns.Add("Qty", typeof(decimal));

                    foreach (var item in lstProductMovement_ResultModel)
                    {
                        DataRow dr = dt.NewRow();

                        dr["datevalue"] = item.datevalue.Value.Year.ToString();
                        dr["Qty"] = item.Qty;

                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                }
                #endregion

                #region Days

                lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(UPCCode, 4);
                if (lstProductMovement_ResultModel != null && lstProductMovement_ResultModel.Count > 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("datevalue", typeof(string));
                    dt.Columns.Add("Qty", typeof(decimal));

                    foreach (var item in lstProductMovement_ResultModel)
                    {
                        DataRow dr = dt.NewRow();
                        string DayName = item.datevalue.Value.ToString("dd");
                        string yearName = item.datevalue.Value.ToString("MMM");

                        dr["datevalue"] = DayName + "-" + yearName;
                        dr["Qty"] = item.Qty;

                        dt.Rows.Add(dr);
                    }

                    ds.Tables.Add(dt);
                }
                #endregion
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmEmployeeWisePaymentHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
