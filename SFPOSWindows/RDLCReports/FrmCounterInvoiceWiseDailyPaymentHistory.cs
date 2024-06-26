﻿using MetroFramework.Forms;
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
    public partial class FrmCounterInvoiceWiseDailyPaymentHistory : MetroForm
    {
        CounterWiseSaleService _CounterWiseSaleService = new CounterWiseSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        public FrmCounterInvoiceWiseDailyPaymentHistory()
        {
            InitializeComponent();
        }

        private void FrmCounterInvoiceWiseDailyPaymentHistory_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CounterWiseDailySale();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CounterInvoiceWiseDailySalePaymentHistory", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CounterInvoiceWiseSalesDailyPaymentHistory.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();
        }

        public DataSet CounterWiseDailySale()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<CounterInvoiceWiseDailyPaymentHistoryModel> lstCounterWiseSaleService = _CounterWiseSaleService.CounterInvoiceWiseDailyPaymentHistory();

                dt.Columns.Add("CounterIP", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns.Add("CashAmount", typeof(decimal));
                dt.Columns.Add("CheckAmount", typeof(decimal));
                dt.Columns.Add("CreditCardAmount", typeof(decimal));
                dt.Columns.Add("FoodStampAmount", typeof(decimal));
                dt.Columns.Add("OrdNo", typeof(string));
                dt.Columns.Add("CancelAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                foreach (var item in lstCounterWiseSaleService)
                {
                    DataRow dr = dt.NewRow();
                    dr["CounterIP"] = item.CounterIP;
                    dr["TotalAmount"] = item.TotalAmount;
                    dr["RefundAmount"] = item.RefundAmount;
                    dr["CashAmount"] = item.CashAmount;
                    dr["CheckAmount"] = item.CheckAmount;
                    dr["CreditCardAmount"] = item.CreditCardAmount;
                    dr["FoodStampAmount"] = item.FoodStampAmount;
                    dr["OrdNo"] = item.OrdNo;
                    dr["CancelAmount"] = item.CancelAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCounterInvoiceWiseDailyPaymentHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
