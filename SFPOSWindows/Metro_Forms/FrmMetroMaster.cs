using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.MasterForm;
using SFPOSWindows.MenuForm;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms
{
    public partial class FrmMetroMaster : MetroForm
    {
        bool x;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmMetroMaster()
        {
            InitializeComponent();
            timer1.Start();
            x = false;
            menuStrip1.Renderer = new myRenderer();           
            Role();
            lblStoreName.Text = LoginInfo.StoreName;
        }

        private class myRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs myMenu)
            {
                if (!myMenu.Item.Selected)
                {
                    base.OnRenderMenuItemBackground(myMenu);
                    myMenu.Item.ForeColor = Color.Black;
                }
                else
                {
                    Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                    SolidBrush brush = new SolidBrush(Color.FromArgb(0, 174, 219));

                    myMenu.Graphics.FillRectangle(brush, menuRectangle);
                    myMenu.Item.ForeColor = Color.White;
                }
            }
        }

        private void FrmMetroMaster_Load(object sender, EventArgs e)
        {
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    Functions.SetIcon(this);
            //    splitContainer1.Panel2.BackgroundImage = SFPOSWindows.Properties.Resources.intPOSDemo_light;
            //    //splitContainer1.Panel2.BackgroundImageLayout = ImageLayout.Stretch;
            //    splitContainer1.Refresh();
            //    metroPanel8.BackgroundImage = SFPOSWindows.Properties.Resources.intPOSDemo;
            //    metroPanel8.BackgroundImageLayout = ImageLayout.Stretch;
            //    metroPanel8.Width = 70;
            //    metroPanel8.Height = 50;
            //    metroPanel8.Refresh();
            //}
            if (RolePermission.Dashboard)
                dashboardToolStripMenuItem.PerformClick();
        }

        public void menuClick(int menuNumber)
        {
            if (menuNumber == 1)
            {
                Pen1.Visible = true;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 2)
            {
                Pen1.Visible = false;
                Pen2.Visible = true;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 3)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = true;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 4)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = true;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 5)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = true;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 6)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = true;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 7)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = true;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 8)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = true;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 9)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = true;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 10)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = true;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 11)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = true;
                Pen12.Visible = false;
                Pen13.Visible = false;
            }
            else if (menuNumber == 12)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = true;
                Pen13.Visible = false;
            }
            else if (menuNumber == 13)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = true;
            }
            else if (menuNumber == 14)
            {
                Pen1.Visible = false;
                Pen2.Visible = false;
                Pen3.Visible = false;
                Pen4.Visible = false;
                Pen5.Visible = false;
                Pen6.Visible = false;
                Pen7.Visible = false;
                Pen8.Visible = false;
                Pen9.Visible = false;
                Pen10.Visible = false;
                Pen11.Visible = false;
                Pen12.Visible = false;
                Pen13.Visible = true;
            }
        }

        public void ActiveMenu(string strControlVal)
        {
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (strControlVal == item.Name)
                {
                    item.ForeColor = Color.FromArgb(0, 174, 219);
                    item.BackColor = Color.White;
                    item.Font = new Font(roleSetupToolStripMenuItem.Font, FontStyle.Bold);
                }
                else
                {
                    item.ForeColor = Color.Black;
                    item.BackColor = SystemColors.ControlLightLight;
                    item.Font = new Font(dashboardToolStripMenuItem.Font, FontStyle.Regular);
                }
            }
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                menuClick(1);
                ActiveMenu("dashboardToolStripMenuItem");
                MenuDashBoard objfrmDashboard = new MenuDashBoard();
                objfrmDashboard.TopLevel = false;
                objfrmDashboard.Visible = true;
                objfrmDashboard.Dock = DockStyle.Fill;

                if (x == false)
                {
                    objfrmDashboard.Width = Screen.PrimaryScreen.WorkingArea.Size.Width - (menuStrip1.Width - 80);
                    x = true;
                }
                else
                {
                    objfrmDashboard.Width = splitContainer1.Panel2.Width;
                }
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objfrmDashboard);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void storeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMenuStore objMenuStore = new frmMenuStore();
                menuClick(RolePermission.A_Store);
                ActiveMenu("storeToolStripMenuItem");
                objMenuStore.TopLevel = false;
                objMenuStore.Visible = true;
                objMenuStore.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objMenuStore);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.IsMdiContainer = true;
                menuClick(RolePermission.A_Product);
                ActiveMenu("productToolStripMenuItem");
                frmMenuProduct objMenuProduct = new frmMenuProduct();
                objMenuProduct.MdiParent = this;
                objMenuProduct.TopLevel = false;
                objMenuProduct.Visible = true;
                objMenuProduct.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objMenuProduct);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void procurementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.IsMdiContainer = true;
                frmMenuProcurement objMenuProcurement = new frmMenuProcurement();
                menuClick(RolePermission.A_Procurement);
                ActiveMenu("procurementToolStripMenuItem");
                objMenuProcurement.MdiParent = this;
                objMenuProcurement.TopLevel = false;
                objMenuProcurement.Visible = true;
                objMenuProcurement.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objMenuProcurement);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void taxSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMenuTax objMenuTax = new frmMenuTax();
                menuClick(RolePermission.A_Tax_Setup);
                ActiveMenu("taxSetupToolStripMenuItem");
                //objMenuTax.MdiParent = this;
                objMenuTax.TopLevel = false;
                objMenuTax.Visible = true;
                objMenuTax.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objMenuTax);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void roleSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MenuRole objMenuRole = new MenuRole();
                menuClick(RolePermission.A_Role_Setup);
                ActiveMenu("roleSetupToolStripMenuItem");
                //objMenuRole.MdiParent = this;
                objMenuRole.TopLevel = false;
                objMenuRole.Visible = true;
                objMenuRole.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objMenuRole);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void liveCountersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MenuLiveCounter objMenuReports = new MenuLiveCounter();
                menuClick(RolePermission.A_Live_Counters);
                ActiveMenu("liveCountersToolStripMenuItem");
                //objMenuReports.MdiParent = this;
                objMenuReports.TopLevel = false;
                objMenuReports.Visible = true;
                objMenuReports.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objMenuReports);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void salesStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MenuSaleStatistics objMenuSaleStatistics = new MenuSaleStatistics();
                menuClick(RolePermission.A_Sale_Statistics);
                ActiveMenu("salesStatisticsToolStripMenuItem");
                //objMenuReports.MdiParent = this;
                objMenuSaleStatistics.TopLevel = false;
                objMenuSaleStatistics.Visible = true;
                objMenuSaleStatistics.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objMenuSaleStatistics);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MenuReports objMenuReports = new MenuReports();
                menuClick(RolePermission.A_Reports);
                ActiveMenu("reportsToolStripMenuItem");
                objMenuReports.TopLevel = false;
                objMenuReports.Visible = true;
                objMenuReports.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objMenuReports);

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        public void Role()
        {
            try
            {
                storeToolStripMenuItem.Visible = RolePermission.Store;
                productToolStripMenuItem.Visible = RolePermission.Product;
                procurementToolStripMenuItem.Visible = RolePermission.Procurement;
                taxSetupToolStripMenuItem.Visible = RolePermission.Tax_Setup;
                roleSetupToolStripMenuItem.Visible = RolePermission.Role_Setup;
                salesStatisticsToolStripMenuItem.Visible = RolePermission.Sale_Statistics;
                liveCountersToolStripMenuItem.Visible = RolePermission.Live_Counters;
                reportsToolStripMenuItem.Visible = RolePermission.Reports;
                SyncDataToolStripMenuItem.Visible = RolePermission.Sync;
                settingsToolStripMenuItem.Visible = RolePermission.Settings;
                dashboardToolStripMenuItem.Visible = RolePermission.Dashboard;
                couponToolStripMenuItem.Visible = RolePermission.Coupon;
                tillStatusToolStripMenuItem.Visible = RolePermission.TillStatus;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        public void ChangeSyncStatus(string TblName)
        {
            try
            {
                
                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var UpdatedTableName = _db.tbl_UpdateLog.Where(x => x.TblName == TblName).ToList();
                foreach (tbl_UpdateLog tbl in UpdatedTableName)
                {
                    tbl.IsChange = true;
                    tbl.IsSync = false;
                    tbl.UpdatedDate = DateTime.Now;
                    tbl.UpdatedBy = LoginInfo.UserId;
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SyncDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSyncData objFrmSyncData = new FrmSyncData();
                menuClick(RolePermission.A_Sync);
                ActiveMenu("SyncDataToolStripMenuItem");
                objFrmSyncData.TopLevel = false;
                objFrmSyncData.Visible = true;
                objFrmSyncData.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(objFrmSyncData);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSettings_BE objfrmSettings = new frmSettings_BE();
                menuClick(RolePermission.A_Settings);
                ActiveMenu("settingsToolStripMenuItem");
                objfrmSettings.TopLevel = false;
                objfrmSettings.Visible = true;
                objfrmSettings.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(objfrmSettings);
                objfrmSettings.LoadPort();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        private void couponToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCoupon objFrmCoupon = new FrmCoupon();
                menuClick(RolePermission.A_Coupon);
                ActiveMenu("couponToolStripMenuItem");
                objFrmCoupon.TopLevel = false;
                objFrmCoupon.Visible = true;
                objFrmCoupon.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(objFrmCoupon);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tillStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMnTillStatus objTillstatus = new frmMnTillStatus();
                menuClick(RolePermission.A_TillStatus);
                ActiveMenu("tillStatusToolStripMenuItem");
                objTillstatus.TopLevel = false;
                objTillstatus.Visible = true;
                objTillstatus.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(objTillstatus);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetroMaster + ex.StackTrace, ex.LineNumber());
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = Functions.GetCurrentDateTime().ToLongTimeString();
            lblDate.Text = Functions.GetCurrentDateTime().ToShortDateString();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
