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
    public partial class frmMenuTax : Form
    {
        public frmMenuTax()
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

        private void btnTaxGroup_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmTaxGroup objFrmTaxGroup = new FrmTaxGroup();
            if (!CheckForm(objFrmTaxGroup))
            {
                objFrmTaxGroup.dataLoad();
                if (objFrmTaxGroup.TaxGroupGrdView.Rows.Count > 0)
                {
                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmTaxGroup.TaxGroupGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    var bmp = new Bitmap(Resources.delete);
                    imgDelete.Image = bmp;
                    objFrmTaxGroup.TaxGroupGrdView.Columns.Add(imgDelete);
                    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                objFrmTaxGroup.TopLevel = false;
                PanelGrid.Controls.Add(objFrmTaxGroup);
                objFrmTaxGroup.FormBorderStyle = FormBorderStyle.None;
                objFrmTaxGroup.Width = PanelGrid.Width;
                objFrmTaxGroup.Height = PanelGrid.Height;
                objFrmTaxGroup.Show();
            }
            else
            {
                Application.OpenForms[objFrmTaxGroup.Name].Focus();
            }
        }

        private void btnTaxDetail_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmTaxRateDetail objFrmTaxRateDetail = new FrmTaxRateDetail();
            if (!CheckForm(objFrmTaxRateDetail))
            {
                objFrmTaxRateDetail.dataLoad();
                if (objFrmTaxRateDetail.TaxRateGrdView.Rows.Count > 0)
                {
                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmTaxRateDetail.TaxRateGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    var bmp = new Bitmap(Resources.delete);
                    imgDelete.Image = bmp;
                    objFrmTaxRateDetail.TaxRateGrdView.Columns.Add(imgDelete);
                    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                objFrmTaxRateDetail.TopLevel = false;
                PanelGrid.Controls.Add(objFrmTaxRateDetail);
                objFrmTaxRateDetail.FormBorderStyle = FormBorderStyle.None;
                objFrmTaxRateDetail.Width = PanelGrid.Width;
                objFrmTaxRateDetail.Height = PanelGrid.Height;
                objFrmTaxRateDetail.Show();
            }
            else
            {
                Application.OpenForms[objFrmTaxRateDetail.Name].Focus();
            }
        }
    }
}
