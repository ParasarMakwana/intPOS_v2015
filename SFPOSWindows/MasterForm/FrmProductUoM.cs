using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmProductUoM : Form
    {
        #region Properties
        public static long PrimaryId = 0;
        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(CommonModelCont.EmptyString);
        ErrorProvider ep = new ErrorProvider();
        ProductUoMMasterModel objProductUoMMasterModel = new ProductUoMMasterModel();
        ProductUoMService _ProductUoMService = new ProductUoMService();
        ProductService _ProductService = new ProductService();
        UoMService _UoMService = new UoMService();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public long ProductId { get; set; }
        #endregion

        #region Events        

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    var isUoM = _ProductUoMService.CheckProductUoMName(Convert.ToInt64(cmbUoM.SelectedValue), Convert.ToInt64(cmbProductName.SelectedValue));
                    if (!isUoM)
                    {
                        objProductUoMMasterModel.ProductID = Convert.ToInt32(cmbProductName.SelectedValue);
                        objProductUoMMasterModel.QtyPerUoM = Convert.ToInt64(txtQtyPerUoM.Text);
                        objProductUoMMasterModel.UnitMeasureID = Convert.ToInt32(cmbUoM.SelectedValue);
                        objProductUoMMasterModel.Discription = txtDiscription.Text;
                        if (PrimaryId <= 0)
                        {
                            var add = _ProductUoMService.AddProductUoM(objProductUoMMasterModel, 1);
                            if (add != null)
                            {
                                DialogResult result = MessageBox.Show(AlertMessages.Add, AlertMessages.SuccessAlert, MessageBoxButtons.OK);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show(AlertMessages.Error, AlertMessages.InformationAlert, MessageBoxButtons.OK);
                            }
                        }
                        else if (PrimaryId > 0)
                        {
                            objProductUoMMasterModel.ProductUoMID = PrimaryId;
                            var add = _ProductUoMService.AddProductUoM(objProductUoMMasterModel, 2);
                            if (add != null)
                            {
                                DialogResult result = MessageBox.Show(AlertMessages.Update, AlertMessages.SuccessAlert, MessageBoxButtons.OK);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show(AlertMessages.Error, AlertMessages.InformationAlert, MessageBoxButtons.OK);
                            }
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.AlreadyExist, AlertMessages.InformationAlert, MessageBoxButtons.OK);
                    }
                    Clear();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ProductUoMGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    txtDiscription.Text = ProductUoMGrdView.Rows[e.RowIndex].Cells[ProductUoMMasterModelCont.Description].Value.ToString().Trim();
                    txtQtyPerUoM.Text = ProductUoMGrdView.Rows[e.RowIndex].Cells[ProductUoMMasterModelCont.QtyPerUoM].Value.ToString().Trim();
                    cmbUoM.SelectedValue = ProductUoMGrdView.Rows[e.RowIndex].Cells[UoMMasterModelCont.UnitMeasureID].Value;
                    cmbProductName.SelectedValue = ProductUoMGrdView.Rows[e.RowIndex].Cells[ProductMasterModelCont.ProductID].Value;
                    PrimaryId = Convert.ToInt64(ProductUoMGrdView.Rows[e.RowIndex].Cells[ProductUoMMasterModelCont.ProductUoMID].Value.ToString());
                    btnAddEdit.Text = AlertMessages.btnChangeUpdate;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ProductUoMGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    //bool IsProductUoM = _ProductUoMService.CheckProductUoMName(txtDiscription.Text.Trim());
                    //if (IsProductUoM == true && PrimaryId > 0)
                    if (PrimaryId > 0)
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objProductUoMMasterModel.ProductUoMID = PrimaryId;
                            var add = _ProductUoMService.AddProductUoM(objProductUoMMasterModel, 3);
                            if (add != null)
                            {
                                DialogResult res = MessageBox.Show(AlertMessages.DeleteSuccess, AlertMessages.SuccessAlert, MessageBoxButtons.OK);
                            }
                            PrimaryId = 0;
                            dataLoad();
                            txtDiscription.Text = null;
                            txtQtyPerUoM.Text = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtDiscription_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtDiscription);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtQtyPerUoM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtQtyPerUoM);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbUoM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.cmbUoM);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public FrmProductUoM()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(CommonModelCont.EmptyString);
                var onjtbl_ProductUoM = (from pu in _db.tbl_ProductUoM.Where(o => o.IsDelete == false)
                                         join pm in _db.tbl_ProductMaster.Where(o => o.IsDelete == false && o.ProductID == ProductId)
                                         on pu.ProductID equals pm.ProductID
                                         join um in _db.tbl_UnitMeasureMaster
                                         on pu.UnitMeasureID equals um.UnitMeasureID
                                         select new
                                         {
                                             ProductUoMID = pu.ProductUoMID,
                                             ProductID = pu.ProductID,
                                             ProductName = pm.ProductName,
                                             UnitMeasureCode = um.UnitMeasureCode,
                                             UnitMeasureID = pu.UnitMeasureID,
                                             Description = pu.Discription,
                                             QtyPerUoM = pu.QtyPerUoM
                                         }).ToList();

                ProductUoMGrdView.DataSource = onjtbl_ProductUoM;

                ProductUoMGrdView.Columns[ProductUoMMasterModelCont.ProductUoMID].Visible = false;
                ProductUoMGrdView.Columns[ProductMasterModelCont.ProductID].Visible = false;
                ProductUoMGrdView.Columns[UoMMasterModelCont.UnitMeasureID].Visible = false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        public void RefreshProduct()
        {
            try
            {
                List<ProductMasterModel> objProductMasterModel = new List<ProductMasterModel>();
                objProductMasterModel = _ProductService.GetAllProduct();
                cmbProductName.DisplayMember = ProductMasterModelCont.ProductName;
                cmbProductName.ValueMember = ProductMasterModelCont.ProductID;
                cmbProductName.DataSource = objProductMasterModel;
                cmbProductName.SelectedValue = ProductId;
                cmbProductName.Enabled = false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        public void RefreshUoM()
        {
            try
            {
                List<UoMMasterModel> lstUoMMasterModel = new List<UoMMasterModel>();
                lstUoMMasterModel = _UoMService.GetAllUoM();
                //lstUoMMasterModel.Insert(0, new UoMMasterModel { UnitMeasureID = 0, UnitMeasureCode = UoMMasterModelCont.cmbUoMFirst });

                cmbUoM.DisplayMember = UoMMasterModelCont.UnitMeasureCode;
                cmbUoM.ValueMember = UoMMasterModelCont.UnitMeasureID;
                cmbUoM.DataSource = lstUoMMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message,CommonTextBoxs.frmProductUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtDiscription:
                    //txtDiscription
                    if ((string.IsNullOrWhiteSpace(txtDiscription.Text)))
                    {
                        txtDiscription.Focus();
                        ep.SetError(txtDiscription, AlertMessages.ProductUoMDiscriptionValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtDiscription, CommonModelCont.EmptyString);
                    }
                    break;

                case CommonTextBoxs.txtQtyPerUoM:
                    //txtQtyPerUoM
                    if ((string.IsNullOrWhiteSpace(txtQtyPerUoM.Text)))
                    {
                        txtDiscription.Focus();
                        ep.SetError(txtQtyPerUoM, AlertMessages.ProductUoMQtyValid);
                        status = false;
                    }
                    else if((!(Regex.Match(txtQtyPerUoM.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        txtQtyPerUoM.Focus();
                         ep.SetError(txtQtyPerUoM, AlertMessages.ProductUoMQtyValid);
                         status = false;
                    }
                    else
                    {
                        ep.SetError(txtQtyPerUoM, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbUoM:
                    //cmbUoM
                    if ((string.IsNullOrWhiteSpace(cmbUoM.Text)))
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbUoM.SelectedIndex < 0)
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbUoM, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtDiscription
                    if ((string.IsNullOrWhiteSpace(txtDiscription.Text)))
                    {
                        txtDiscription.Focus();
                        ep.SetError(txtDiscription, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtDiscription, CommonModelCont.EmptyString);
                    }
                    //txtQtyPerUoM
                    if ((string.IsNullOrWhiteSpace(txtQtyPerUoM.Text)))
                    {
                        txtDiscription.Focus();
                        ep.SetError(txtQtyPerUoM, AlertMessages.ProductUoMQtyValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtQtyPerUoM, CommonModelCont.EmptyString);
                    }
                    //cmbUoM
                    if ((string.IsNullOrWhiteSpace(cmbUoM.Text)))
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbUoM.SelectedIndex < 0)
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbUoM, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtDiscription.Text = null;
            ep.SetError(txtDiscription, CommonModelCont.EmptyString);
            txtQtyPerUoM.Text = null;
            ep.SetError(txtQtyPerUoM, CommonModelCont.EmptyString);
            cmbUoM.SelectedIndex = 0;
            ep.SetError(cmbUoM, CommonModelCont.EmptyString);
            btnAddEdit.Text = AlertMessages.btnChangeAdd;
            dataLoad();
            flagSave = false;
            PrimaryId = 0;
        }
        #endregion


    }
}