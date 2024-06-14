using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class frmAddEditSubCategory : Form
    {
        private SubCategoryService _SubCategoryService = new SubCategoryService();
        public long PrimaryId = 0;
        ErrorProvider ep = new ErrorProvider();
        SubCategoryModel objSubCategoryMasterModel = new SubCategoryModel();
        FrmSubCategory objFrmSubCategory = new FrmSubCategory();

        public frmAddEditSubCategory()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                txtCategoryName.Focus();
                ep.SetError(txtCategoryName, "Sub-Category name should not be left blank!");
            }
            else if (PrimaryId > 0)
            {
                objSubCategoryMasterModel.SubCategoryID = PrimaryId;
                objSubCategoryMasterModel.SubCategoryName = txtCategoryName.Text.Trim();
                var add = _SubCategoryService.AddCategory(objSubCategoryMasterModel, 2);
                PrimaryId = 0;
                lblMessage.Text = "Sub-Category Edited successfully";
                txtCategoryName.Text = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool IsCategory = _SubCategoryService.CheckCategoryName(txtCategoryName.Text.Trim());
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                txtCategoryName.Focus();
                ep.SetError(txtCategoryName, "Sub-Category name should not be left blank!");
            }
            else if (IsCategory == false)
            {
                objSubCategoryMasterModel.CategoryID = PrimaryId;
                objSubCategoryMasterModel.SubCategoryName = txtCategoryName.Text.Trim();
                var add = _SubCategoryService.AddCategory(objSubCategoryMasterModel, 1);
                lblMessage.Text = "Sub-Category Added successfully";
                txtCategoryName.Text = null;
            }
        }

        private void frmAddEditSubCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmSubCategory objFrmSubCategory = new FrmSubCategory();
            objFrmSubCategory.CategoryId = Convert.ToInt32(PrimaryId);
            objFrmSubCategory.dataLoad();
        }
    }
}
