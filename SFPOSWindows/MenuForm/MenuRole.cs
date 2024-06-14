using SFPOS.Common;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.MenuForm
{
    public partial class MenuRole : Form
    {
        public MenuRole()
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

        private void btnRoles_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmRole objFrmRole = new FrmRole();
            if (!CheckForm(objFrmRole))
            {
                objFrmRole.dataLoad();
                if (objFrmRole.RoleGrdView.Rows.Count > 0)
                {
                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmRole.RoleGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    var bmp = new Bitmap(Resources.delete); 
                    imgDelete.Image = bmp;
                    objFrmRole.RoleGrdView.Columns.Add(imgDelete);
                    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                objFrmRole.TopLevel = false;
                PanelGrid.Controls.Add(objFrmRole);
                objFrmRole.FormBorderStyle = FormBorderStyle.None;
                objFrmRole.Width = PanelGrid.Width;
                objFrmRole.Height = PanelGrid.Height;
                objFrmRole.Show();
            }
            else
            {
                Application.OpenForms[objFrmRole.Name].Focus();
            }
        }

        private void btnRolePermission_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmRolePermissions objFrmRolePermission = new FrmRolePermissions();
            if (!CheckForm(objFrmRolePermission))
            {
                objFrmRolePermission.dataLoad();
                if (objFrmRolePermission.RoleGrdView.Rows.Count > 0)
                {
                    objFrmRolePermission.chkBoxListLoad();
                }
                objFrmRolePermission.TopLevel = false;
                PanelGrid.Controls.Add(objFrmRolePermission);
                objFrmRolePermission.FormBorderStyle = FormBorderStyle.None;
                objFrmRolePermission.Width = PanelGrid.Width;
                objFrmRolePermission.Height = PanelGrid.Height;
                objFrmRolePermission.Show();
            }
            else
            {
                Application.OpenForms[objFrmRolePermission.Name].Focus();
            }
        }

        private void PanelGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFunctionalPermission_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmRolePermissions objFrmRolePermission = new FrmRolePermissions();
            frmFunctionalPermission objFrmFunctionalPermission = new frmFunctionalPermission();
            if (!CheckForm(objFrmFunctionalPermission))
            {
                objFrmFunctionalPermission.dataLoad();
                if (objFrmFunctionalPermission.RoleGrdView.Rows.Count > 0)
                {
                    objFrmFunctionalPermission.chkBoxListLoad();
                }
                objFrmFunctionalPermission.TopLevel = false;
                PanelGrid.Controls.Add(objFrmFunctionalPermission);
                objFrmFunctionalPermission.FormBorderStyle = FormBorderStyle.None;
                objFrmFunctionalPermission.Width = PanelGrid.Width;
                objFrmFunctionalPermission.Height = PanelGrid.Height;
                objFrmFunctionalPermission.Show();
            }
            else
            {
                Application.OpenForms[objFrmRolePermission.Name].Focus();
            }
        }
    }
}
