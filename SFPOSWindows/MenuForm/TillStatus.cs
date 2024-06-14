using SFPOS.Common;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOSWindows.Reports;
using TillStatusReport;
using TillStatusReport.RDLCReports;

namespace SFPOSWindows.MenuForm
{
    public partial class frmMnTillStatus : Form
    {
        public frmMnTillStatus()
        {
            InitializeComponent();
            if (RolePermission.DailyManagementReport)
            {
                btnSummaryReport.Visible = true;
            }
            else
            {
                btnSummaryReport.Visible = false;
            }
            if (RolePermission.DepositVerifierReport)
            {
                btnDepositeVerifier.Visible = true;
                if (!RolePermission.DailyManagementReport)
                {
                    btnDepositeVerifier.Location = new Point(22, 128);
                }
            }
            else
            {
                btnDepositeVerifier.Visible = false;
            }
            if (RolePermission.DepositVerification)
            {
                btnDepVeri.Visible = true;
                if (!RolePermission.DailyManagementReport && !RolePermission.DepositVerifierReport)
                {
                    btnDepVeri.Location = new Point(22, 128);
                }
                else if (!RolePermission.DailyManagementReport || !RolePermission.DepositVerifierReport)
                {
                    btnDepVeri.Location = new Point(22, 173);
                }
            }
            else
            {
                btnDepVeri.Visible = false;
            }
            
        }

        private bool CheckForm(Form form)
        {
            form = Application.OpenForms[form.Text];
            if (form != null)
                return true;
            else
                return false;
        }

        private void btntillstatus_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            FrmAddReport objFrmAddReport = new FrmAddReport();
            PictureWatermark.Visible = false;
            if (!CheckForm(objFrmAddReport))
            {
                objFrmAddReport.TopLevel = false;
                PanelGrid.Controls.Add(objFrmAddReport);
                objFrmAddReport.FormBorderStyle = FormBorderStyle.None;
                objFrmAddReport.Width = PanelGrid.Width;
                objFrmAddReport.Height = PanelGrid.Height;
                objFrmAddReport.Show();
            }
            else
            {
                Application.OpenForms[objFrmAddReport.Name].Focus();
            }
        }

        private void btnDepVeri_Click(object sender, EventArgs e)
        {
            try
            {
                PanelGrid.Controls.Clear();
                FrmDepositVerification objFrmDepositVerification = new FrmDepositVerification();

                if (!CheckForm(objFrmDepositVerification))
                {
                    objFrmDepositVerification.TopLevel = false;
                    PanelGrid.Controls.Add(objFrmDepositVerification);
                    objFrmDepositVerification.FormBorderStyle = FormBorderStyle.None;
                    objFrmDepositVerification.Width = objFrmDepositVerification.Width;
                    objFrmDepositVerification.Location = new Point(0, 0);
                    objFrmDepositVerification.Show();
                }
                else
                {
                    Application.OpenForms[objFrmDepositVerification.Name].Focus();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }

        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            try
            {
                //PanelGrid.Controls.Clear();
                ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
                objReportDateTimeFilter.FormNo = 2;
                objReportDateTimeFilter.Location = new Point(0, 0);
                objReportDateTimeFilter.ShowDialog();

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }

        private void PictureWatermark_Click(object sender, EventArgs e)
        {

        }

        private void btnDepositeVerifier_Click(object sender, EventArgs e)
        {
            try
            {
                //PanelGrid.Controls.Clear();
                ReportDateTimeFilter objReportDateTimeFilter = new ReportDateTimeFilter();
                objReportDateTimeFilter.FormNo = 3;
                objReportDateTimeFilter.Location = new Point(0, 0);
                objReportDateTimeFilter.ShowDialog();

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }
    }
}
