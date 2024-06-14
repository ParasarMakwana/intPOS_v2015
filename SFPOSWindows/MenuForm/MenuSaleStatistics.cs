using SFPOSWindows.MasterForm;
using SFPOSWindows.Properties;
using SFPOSWindows.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOS.Common;

namespace SFPOSWindows.MenuForm
{
    public partial class MenuSaleStatistics : Form
    {
        public MenuSaleStatistics()
        {
            InitializeComponent();
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    PictureWatermark.Image = SFPOSWindows.Properties.Resources.intPOSDemo_lightX;
            //    PictureWatermark.Refresh();
            //}
        }

        private bool CheckForm(Form form)
        {
            form = Application.OpenForms[form.Text];
            if (form != null)
                return true;
            else
                return false;
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmSalesOrders objFrmSalesOrders = new FrmSalesOrders();
            if (!CheckForm(objFrmSalesOrders))
            {
                objFrmSalesOrders.dataLoadToday();
    
                //if (objFrmSalesOrders.SaleOrderGrdView.Rows.Count > 0)
                //{
                    //DataGridViewImageColumn imgSubOrder = new DataGridViewImageColumn();
                    //var Sub_Order = new Bitmap(Resources.Sub_Category_Management);
                    //imgSubOrder.Image = Sub_Order;
                    //objFrmSalesOrders.SaleOrderGrdView.Columns.Add(imgSubOrder);
                    //imgSubOrder.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //}
                objFrmSalesOrders.LoadCmbCashierCode();
                objFrmSalesOrders.TopLevel = false;
                PanelGrid.Controls.Add(objFrmSalesOrders);
                objFrmSalesOrders.FormBorderStyle = FormBorderStyle.None;
                objFrmSalesOrders.Width = PanelGrid.Width;
                objFrmSalesOrders.Height = PanelGrid.Height;
                objFrmSalesOrders.Show();
            }
            else
            {
                Application.OpenForms[objFrmSalesOrders.Name].Focus();
            }
        }

        private void btnProductSales_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmProductSales objFrmProductSales = new FrmProductSales();
            if (!CheckForm(objFrmProductSales))
            {
                objFrmProductSales.LoadCmbDepartmentName();
                objFrmProductSales.LoadCmbVendorName();
                objFrmProductSales.TopLevel = false;
                PanelGrid.Controls.Add(objFrmProductSales);
                objFrmProductSales.FormBorderStyle = FormBorderStyle.None;
                objFrmProductSales.Width = PanelGrid.Width;
                objFrmProductSales.Height = PanelGrid.Height;
                objFrmProductSales.Show();
            }
            else
            {
                Application.OpenForms[objFrmProductSales.Name].Focus();
            }
        }

        private void btnProductMovement_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmProductMovement objFrmProductMovement = new FrmProductMovement();
            if (!CheckForm(objFrmProductMovement))
            {
                objFrmProductMovement.TopLevel = false;
                PanelGrid.Controls.Add(objFrmProductMovement);
                objFrmProductMovement.FormBorderStyle = FormBorderStyle.None;
                objFrmProductMovement.Width = PanelGrid.Width;
                objFrmProductMovement.Height = PanelGrid.Height;
                objFrmProductMovement.Show();
            }
            else
            {
                Application.OpenForms[objFrmProductMovement.Name].Focus();
            }
        }

        private void btnTillStatus_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            frmTillStatus objfrmTillStatus = new frmTillStatus();
            if (!CheckForm(objfrmTillStatus))
            {
                objfrmTillStatus.TopLevel = false;
                PanelGrid.Controls.Add(objfrmTillStatus);
                objfrmTillStatus.FormBorderStyle = FormBorderStyle.None;
                objfrmTillStatus.Width = PanelGrid.Width;
                objfrmTillStatus.Height = PanelGrid.Height;
                objfrmTillStatus.Show();
            }
            else
            {
                Application.OpenForms[objfrmTillStatus.Name].Focus();
            }
        }
    }
}
