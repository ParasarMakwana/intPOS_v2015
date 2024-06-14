using SFPOS.Common;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Properties;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SFPOSWindows.MenuForm
{
    public partial class frmMenuProduct : Form
    {
        public frmMenuProduct()
        {
            InitializeComponent();
            
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    PictureWatermark.Image = SFPOSWindows.Properties.Resources.intPOSDemo_lightX;
            //    PictureWatermark.Refresh();
            //}
            //else
            //{
            //    PictureWatermark = new PictureBox();
            //    PictureWatermark.SizeMode = PictureBoxSizeMode.AutoSize;
            //    PictureWatermark.Anchor = AnchorStyles.None;
            //    CenterPictureBox(PictureWatermark, Resources.sf_);
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

        private void btnCategory_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmDepartment objFrmDepartment = new FrmDepartment();
            if (!CheckForm(objFrmDepartment))
            {
                objFrmDepartment.dataLoad();

                if (objFrmDepartment.DepartmentGrdView.Rows.Count > 0)
                {
                    DataGridViewImageColumn imgSection = new DataGridViewImageColumn();
                    var Sub_Category_Management = new Bitmap(Resources.Sub_Category_Management);
                    imgSection.Image = Sub_Category_Management;
                    objFrmDepartment.DepartmentGrdView.Columns.Add(imgSection);
                    imgSection.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var edit = new Bitmap(Resources.edit);
                    imgEdit.Image = edit;
                    objFrmDepartment.DepartmentGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    //DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    //var bmp = new Bitmap(Resources.delete);
                    //imgDelete.Image = bmp;
                    //objFrmDepartment.DepartmentGrdView.Columns.Add(imgDelete);
                    //imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                objFrmDepartment.TopLevel = false;
                PanelGrid.Controls.Add(objFrmDepartment);
                objFrmDepartment.FormBorderStyle = FormBorderStyle.None;
                objFrmDepartment.Width = PanelGrid.Width;
                objFrmDepartment.Height = PanelGrid.Height;
                objFrmDepartment.Show();
            }
            else
            {
                Application.OpenForms[objFrmDepartment.Name].Focus();
            }

            

        }

        private void btnUOM_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            //PictureWatermark.Visible = false;
            FrmUnitofMeasure objFrmUOM = new FrmUnitofMeasure();
            if (!CheckForm(objFrmUOM))
            {
                objFrmUOM.dataLoad();
                if (objFrmUOM.UoMGrdView.Rows.Count > 0)
                {
                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmUOM.UoMGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    var bmp = new Bitmap(Resources.delete);
                    imgDelete.Image = bmp;
                    objFrmUOM.UoMGrdView.Columns.Add(imgDelete);
                    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                objFrmUOM.TopLevel = false;
                //objFrmStore.Visible = true;
                PanelGrid.Controls.Add(objFrmUOM);
                objFrmUOM.FormBorderStyle = FormBorderStyle.None;
                objFrmUOM.Width = PanelGrid.Width;
                objFrmUOM.Height = PanelGrid.Height;
                objFrmUOM.Show();
            }
            else
            {
                Application.OpenForms[objFrmUOM.Name].Focus();
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            try
            {
                PanelGrid.Controls.Clear();
                FrmProduct objFrmProduct = new FrmProduct();

                if (!CheckForm(objFrmProduct))
                {
                    objFrmProduct.TopLevel = false;
                    PanelGrid.Controls.Add(objFrmProduct);
                    objFrmProduct.FormBorderStyle = FormBorderStyle.None;
                    objFrmProduct.Width = PanelGrid.Width;
                    objFrmProduct.Height = PanelGrid.Height;
                    objFrmProduct.Show();
                }   
                else
                {
                    Application.OpenForms[objFrmProduct.Name].Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CenterPictureBox(PictureBox picBox, Bitmap picImage)
        {
            picBox.Image = picImage;
            picBox.Location = new Point((this.Width / 2) - (picImage.Width / 2),
                                        (this.Height / 2) - (picImage.Height / 2));
            picBox.Refresh();
        }
    }
}
