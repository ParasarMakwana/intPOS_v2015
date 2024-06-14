using SFPOS.BAL;
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
    public partial class FrmAddEditCategory : Form
    {
        private CategoryService _CategoryService = new CategoryService();
        public long PrimaryId = 0;
        ErrorProvider ep = new ErrorProvider();
        CategoryMasterModel objCategoryMasterModel = new CategoryMasterModel();
        FrmCategory fc = new FrmCategory();

        public FrmAddEditCategory()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                txtCategoryName.Focus();
                ep.SetError(txtCategoryName, "Category name should not be left blank!");
            }
            else if (PrimaryId > 0)
            {
                objCategoryMasterModel.CategoryID = PrimaryId;
                objCategoryMasterModel.CategoryName = txtCategoryName.Text.Trim();
                var add = _CategoryService.AddCategory(objCategoryMasterModel, 2);
                PrimaryId = 0;
                lblMessage.Text = "Category Edited successfully";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool IsCategory = _CategoryService.CheckCategoryName(txtCategoryName.Text.Trim());
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                txtCategoryName.Focus();
                ep.SetError(txtCategoryName, "Category name should not be left blank!");
            }
            else if(IsCategory == false)
            {
                objCategoryMasterModel.CategoryName = txtCategoryName.Text.Trim();
                var add = _CategoryService.AddCategory(objCategoryMasterModel, 1);
                lblMessage.Text = "Category has been added successfully";
                txtCategoryName.Text = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddEditCategory_FormClosed(object sender, FormClosedEventArgs e)
        {
            fc.dataLoad();
        }
    }
}
