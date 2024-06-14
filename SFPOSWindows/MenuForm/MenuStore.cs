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

namespace SFPOSWindows.MenuForm
{
    public partial class frmMenuStore : Form
    {
        public frmMenuStore()
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
            FrmStore objFrmStore = new FrmStore();
            if (!CheckForm(objFrmStore))
            {
                objFrmStore.dataLoad();
                if (objFrmStore.StoreGrdView.Rows.Count > 0)
                {
                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmStore.StoreGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    //DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    //var bmp = new Bitmap(Resources.delete);
                    //imgDelete.Image = bmp;
                    //objFrmStore.StoreGrdView.Columns.Add(imgDelete);
                    //imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                objFrmStore.TopLevel = false;
                PanelGrid.Controls.Add(objFrmStore);
                objFrmStore.FormBorderStyle = FormBorderStyle.None;
                objFrmStore.Width = PanelGrid.Width;
                objFrmStore.Height = PanelGrid.Height;
                objFrmStore.Show();
            }
            else
            {
                Application.OpenForms[objFrmStore.Name].Focus();
            }
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            FrmEmployee objFrmEmployee = new FrmEmployee();
            PictureWatermark.Visible = false;
            if (!CheckForm(objFrmEmployee))
            {
                objFrmEmployee.dataLoad();

                if (objFrmEmployee.EmployeeGrdView.Rows.Count > 0)
                {

                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmEmployee.EmployeeGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    var bmp = new Bitmap(Resources.delete);
                    imgDelete.Image = bmp;
                    objFrmEmployee.EmployeeGrdView.Columns.Add(imgDelete);
                    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    
                }
                //objFrmEmployee.CmbStore();
                //objFrmEmployee.CmbRole();
                objFrmEmployee.TopLevel = false;
                PanelGrid.Controls.Add(objFrmEmployee);
                objFrmEmployee.FormBorderStyle = FormBorderStyle.None;
                objFrmEmployee.Width = PanelGrid.Width;
                objFrmEmployee.Height = PanelGrid.Height;
                objFrmEmployee.Show();
            }
            else
            {
                Application.OpenForms[objFrmEmployee.Name].Focus();
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            FrmCustomer objFrmCustomer = new FrmCustomer();
            PictureWatermark.Visible = false;
            if (!CheckForm(objFrmCustomer))
            {
                objFrmCustomer.dataLoad();

                if (objFrmCustomer.CustomerGrdView.Rows.Count > 0)
                {

                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmCustomer.CustomerGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    var bmp = new Bitmap(Resources.delete);
                    imgDelete.Image = bmp;
                    objFrmCustomer.CustomerGrdView.Columns.Add(imgDelete);
                    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                }
                //objFrmEmployee.CmbStore();
                //objFrmEmployee.CmbRole();
                objFrmCustomer.TopLevel = false;
                PanelGrid.Controls.Add(objFrmCustomer);
                objFrmCustomer.FormBorderStyle = FormBorderStyle.None;
                objFrmCustomer.Width = PanelGrid.Width;
                objFrmCustomer.Height = PanelGrid.Height;
                objFrmCustomer.Show();
            }
            else
            {
                Application.OpenForms[objFrmCustomer.Name].Focus();
            }
        }
    }
}
