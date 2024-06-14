using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.Reports
{
    public partial class frmTillStatus : Form
    {
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public frmTillStatus()
        {
            InitializeComponent();
        }

        private void frmTillStatus_Load(object sender, EventArgs e)
        {
            LoadCmbCashierCode();
        }

        public void LoadCmbCashierCode()
        {
            try
            {
                long EmployeeID = 0;
                List<EmployeeMasterModel> lstEmployeeMasterModel = new List<EmployeeMasterModel>();
                EmployeeService _EmployeeService = new EmployeeService();
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var onjtbl_ProductUoM = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false)
                                         orderby EM.FirstName
                                         select new
                                         {
                                             FirstName = EM.FirstName + " " + EM.LastName,
                                             EmployeeID = EM.EmployeeID,
                                         }).ToList();
                onjtbl_ProductUoM.Insert(0, new { FirstName = "Cashier", EmployeeID = EmployeeID });
                cmbCashier.DisplayMember = "FirstName";
                cmbCashier.ValueMember = "EmployeeID";
                cmbCashier.DataSource = onjtbl_ProductUoM;
                //LoadData(2, Convert.ToDateTime("2019 - 06 - 01 16:44:48.607"), Convert.ToDateTime("2019-06-13 16:44:48.607"));
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadData(long EmployeeID, DateTime StartTime, DateTime EndTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var result = _db.SP_TillStatusEmployeeWise(EmployeeID, StartTime, EndTime).FirstOrDefault();
                if (result.OrderCount == null && result.CancelledCount == null && result.RefundCount == null)
                {
                    Clear();
                }
                else
                {
                    btnSave.Enabled = true;
                    txtS_Cash.Text = "$" + (!String.IsNullOrEmpty(result.CashAmount.ToString()) ? result.CashAmount : 0);
                    txtS_Check.Text = "$" + (!String.IsNullOrEmpty(result.CheckAmount.ToString()) ? result.CheckAmount : 0);
                    txtS_CreaditCard.Text = "$" + (!String.IsNullOrEmpty(result.CreditCardAmount.ToString()) ? result.CreditCardAmount : 0);
                    txtS_FoodStamp.Text = "$" + (!String.IsNullOrEmpty(result.FoodStampAmount.ToString()) ? result.FoodStampAmount : 0);
                    //20200722 - Komel Lakhani
                    //if ((!String.IsNullOrEmpty(result.CashAmount.ToString()) ? result.CashAmount : 0) >= 0)
                    //    txtB_Cash.Text = "$-" + (!String.IsNullOrEmpty(result.CashAmount.ToString()) ? result.CashAmount : 0);
                    //else
                    //    txtB_Cash.Text = "$" + (!String.IsNullOrEmpty(result.CashAmount.ToString()) ? result.CashAmount : 0);
                    //if ((!String.IsNullOrEmpty(result.CheckAmount.ToString()) ? result.CheckAmount : 0) >= 0)
                    //    txtB_Check.Text = "$-" + (!String.IsNullOrEmpty(result.CheckAmount.ToString()) ? result.CheckAmount : 0);
                    //else
                    //    txtB_Check.Text = "$" + (!String.IsNullOrEmpty(result.CheckAmount.ToString()) ? result.CheckAmount : 0);
                    //if ((!String.IsNullOrEmpty(result.CreditCardAmount.ToString()) ? result.CreditCardAmount : 0) >= 0)
                    //    txtB_CreaditCard.Text = "$-" + (!String.IsNullOrEmpty(result.CreditCardAmount.ToString()) ? result.CreditCardAmount : 0);
                    //else
                    //    txtB_CreaditCard.Text = "$" + (!String.IsNullOrEmpty(result.CreditCardAmount.ToString()) ? result.CreditCardAmount : 0);
                    //if ((!String.IsNullOrEmpty(result.FoodStampAmount.ToString()) ? result.FoodStampAmount : 0) >= 0)
                    //    txtB_FoodStamp.Text = "$-" + (!String.IsNullOrEmpty(result.FoodStampAmount.ToString()) ? result.FoodStampAmount : 0);
                    //else
                    //    txtB_FoodStamp.Text = "$" + (!String.IsNullOrEmpty(result.FoodStampAmount.ToString()) ? result.FoodStampAmount : 0);

                    txtB_Cash.Text = "$" + ((Convert.ToDecimal(txtA_Cash.Text.Replace("$", "")) - Convert.ToDecimal(txtS_Cash.Text.Replace("$", ""))).ToString());
                    txtB_Check.Text = "$" + ((Convert.ToDecimal(txtA_Check.Text.Replace("$", "")) - Convert.ToDecimal(txtS_Check.Text.Replace("$", ""))).ToString());
                    txtB_CreaditCard.Text = "$" + ((Convert.ToDecimal(txtA_CreaditCard.Text.Replace("$", "")) - Convert.ToDecimal(txtS_CreaditCard.Text.Replace("$", ""))).ToString());
                    txtB_FoodStamp.Text = "$" + ((Convert.ToDecimal(txtA_FoodStamp.Text.Replace("$", "")) - Convert.ToDecimal(txtS_FoodStamp.Text.Replace("$", ""))).ToString());


                    //
                    txtGrossAmount.Text = "$" + Convert.ToDecimal(!String.IsNullOrEmpty(result.GrossAmount.ToString()) ? result.GrossAmount : 0).ToString("0.00");
                    txtTaxAmount.Text = "$" + Convert.ToDecimal(!String.IsNullOrEmpty(result.TaxAmount.ToString()) ? result.TaxAmount : 0).ToString("0.00");
                    txtTotalOrder.Text = (!String.IsNullOrEmpty(result.OrderCount.ToString()) ? result.OrderCount : 0).ToString();
                    if (result.OrderCount != 0)
                    {
                        txtAveOrder.Text = "$" + (Convert.ToDecimal(!String.IsNullOrEmpty(result.GrossAmount.ToString()) ? result.GrossAmount : 0) / (Convert.ToDecimal(!String.IsNullOrEmpty(result.OrderCount.ToString()) ? result.OrderCount : 0))).ToString("0.00");
                    }
                    else
                    {
                        txtAveOrder.Text = "$0.00";
                    }
                    txtRefundAmount.Text = "$" + Convert.ToDecimal(!String.IsNullOrEmpty(result.RefundAmount.ToString()) ? result.RefundAmount : 0).ToString("0.00");
                    txtCheckRefund.Text = "$" + Convert.ToDecimal(!String.IsNullOrEmpty(result.CheckRefund.ToString()) ? result.CheckRefund : 0).ToString("0.00");
                    txtCanceledAmount.Text = "$" + Convert.ToDecimal(!String.IsNullOrEmpty(result.CancelledAmount.ToString()) ? result.CancelledAmount : 0).ToString("0.00");
                    txtOverrideTotal.Text = "$" + Convert.ToDecimal(!String.IsNullOrEmpty(result.OverridePriceTotal.ToString()) ? result.OverridePriceTotal : 0).ToString("0.00");
                    txtRefundOrder.Text = result.RefundCount.ToString();
                    txtRefundOrder.Text = result.RefundCount.ToString();
                    txtOverridwOrder.Text = result.OverridePriceCount.ToString();

                    //decimal Cash = Convert.ToDecimal(!String.IsNullOrEmpty(result.CashAmount.ToString()) ? result.CashAmount : 0);
                    //decimal Refund = Convert.ToDecimal(!String.IsNullOrEmpty(result.RefundAmount.ToString()) ? result.RefundAmount : 0); ;
                    //decimal Final = Cash - Refund;
                    //txtS_Cash.Text = "$" + Final;
                    //
                    TotalAmount();

                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog("Frm-TillStatus-LoadData", ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(cmbCashier.SelectedValue.ToString()) != 0)
                {
                    long _CashierID = Convert.ToInt64(cmbCashier.SelectedValue.ToString());
                    DateTime _SelectedDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    //DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    //var result = _db.tbl_TillBalancing.Where(x => x.CashierID == _CashierID && x.SelectedDate == _SelectedDate).FirstOrDefault();

                    //if (result != null)
                    //{
                    //    #region Set Values
                    //    btnSave.Enabled = true;
                    //    //
                    //    txtA_Cash.Text = "$" + (!String.IsNullOrEmpty(result.A_Cash.ToString()) ? result.A_Cash : 0);
                    //    txtA_Check.Text = "$" + (!String.IsNullOrEmpty(result.A_Check.ToString()) ? result.A_Check : 0);
                    //    txtA_CreaditCard.Text = "$" + (!String.IsNullOrEmpty(result.A_CreaditCard.ToString()) ? result.A_CreaditCard : 0);
                    //    txtA_FoodStamp.Text = "$" + (!String.IsNullOrEmpty(result.A_FoodStamp.ToString()) ? result.A_FoodStamp : 0);
                    //    //                       
                    //    txtB_Cash.Text = "$" + (!String.IsNullOrEmpty(result.B_Cash.ToString()) ? result.B_Cash : 0);
                    //    txtB_Check.Text = "$" + (!String.IsNullOrEmpty(result.B_Check.ToString()) ? result.B_Check : 0);
                    //    txtB_CreaditCard.Text = "$" + (!String.IsNullOrEmpty(result.B_CreaditCard.ToString()) ? result.B_CreaditCard : 0);
                    //    txtB_FoodStamp.Text = "$" + (!String.IsNullOrEmpty(result.B_FoodStamp.ToString()) ? result.B_FoodStamp : 0);
                    //    //
                    //    txtS_Cash.Text = "$" + (!String.IsNullOrEmpty(result.S_Cash.ToString()) ? result.S_Cash : 0);
                    //    txtS_Check.Text = "$" + (!String.IsNullOrEmpty(result.S_Check.ToString()) ? result.S_Check : 0);
                    //    txtS_CreaditCard.Text = "$" + (!String.IsNullOrEmpty(result.S_CreaditCard.ToString()) ? result.S_CreaditCard : 0);
                    //    txtS_FoodStamp.Text = "$" + (!String.IsNullOrEmpty(result.S_FoodStamp.ToString()) ? result.S_FoodStamp : 0);
                    //    //
                    //    txtActualTotal.Text = "$" + (!String.IsNullOrEmpty(result.A_Total.ToString()) ? result.A_Total : 0);
                    //    txtBalanceTotal.Text = "$" + (!String.IsNullOrEmpty(result.B_Total.ToString()) ? result.B_Total : 0);
                    //    txtSystemTotal.Text = "$" + (!String.IsNullOrEmpty(result.S_Total.ToString()) ? result.S_Total : 0);
                    //    //
                    //    txtGrossAmount.Text = "$" + (!String.IsNullOrEmpty(result.GrossAmount.ToString()) ? result.GrossAmount : 0);
                    //    txtTaxAmount.Text = "$" + (!String.IsNullOrEmpty(result.TaxAmount.ToString()) ? result.TaxAmount : 0);
                    //    txtAveOrder.Text = "$" + (!String.IsNullOrEmpty(result.AveOrder.ToString()) ? result.AveOrder : 0);
                    //    txtRefundAmount.Text = "$" + (!String.IsNullOrEmpty(result.RefundAmount.ToString()) ? result.RefundAmount : 0);
                    //    txtCheckRefund.Text = "$" + (!String.IsNullOrEmpty(result.CheckRefund.ToString()) ? result.CheckRefund : 0);
                    //    txtCanceledAmount.Text = "$" + (!String.IsNullOrEmpty(result.CancelledAmount.ToString()) ? result.CancelledAmount : 0);
                    //    //             
                    //    txtTotalOrder.Text = (!String.IsNullOrEmpty(result.TotalOrder.ToString()) ? result.TotalOrder : 0).ToString();
                    //    txtRefundOrder.Text = (!String.IsNullOrEmpty(result.RefundOrder.ToString()) ? result.TotalOrder : 0).ToString();
                    //    txtCanceledOrder.Text = (!String.IsNullOrEmpty(result.CancelledOrder.ToString()) ? result.TotalOrder : 0).ToString();
                    //    //
                    //    btnSave.Text = "Update";
                    //    #endregion
                    //}
                    //else
                    //{
                    LoadData(Convert.ToInt64(cmbCashier.SelectedValue.ToString()), Convert.ToDateTime(txtStartDate.Text.Trim()), DateTime.Now);
                    //}
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please select cashier", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog("Frm-TillStatus-btnView_Click", ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

                if (Convert.ToInt64(cmbCashier.SelectedValue.ToString()) != 0)
                {
                    long _CashierID = Convert.ToInt64(cmbCashier.SelectedValue.ToString());
                    DateTime _SelectedDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    var objTbl = _db.tbl_TillBalancing.Where(x => x.CashierID == _CashierID && x.SelectedDate == _SelectedDate).FirstOrDefault();
                    if (objTbl != null)
                    {
                        #region Update Current Record

                        objTbl.A_Cash = Functions.GetDecimal((txtA_Cash.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.A_Check = Functions.GetDecimal((txtA_Check.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.A_CreaditCard = Functions.GetDecimal((txtA_CreaditCard.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.A_FoodStamp = Functions.GetDecimal((txtA_FoodStamp.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        objTbl.B_Cash = Functions.GetDecimal((txtB_Cash.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.B_Check = Functions.GetDecimal((txtB_Check.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.B_CreaditCard = Functions.GetDecimal((txtB_CreaditCard.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.B_FoodStamp = Functions.GetDecimal((txtB_FoodStamp.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        objTbl.S_Cash = Functions.GetDecimal((txtS_Cash.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.S_Check = Functions.GetDecimal((txtS_Check.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.S_CreaditCard = Functions.GetDecimal((txtS_CreaditCard.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.S_FoodStamp = Functions.GetDecimal((txtS_FoodStamp.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        objTbl.A_Total = Functions.GetDecimal((txtActualTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.B_Total = Functions.GetDecimal((txtBalanceTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.S_Total = Functions.GetDecimal((txtSystemTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        objTbl.GrossAmount = Functions.GetDecimal((txtGrossAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.TaxAmount = Functions.GetDecimal((txtTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.AveOrder = Functions.GetDecimal((txtAveOrder.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.RefundAmount = Functions.GetDecimal((txtRefundAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.CheckRefund = Functions.GetDecimal((txtCheckRefund.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        objTbl.CancelledAmount = Functions.GetDecimal((txtCanceledAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        objTbl.TotalOrder = Functions.GetDecimal((txtTotalOrder.Text));
                        objTbl.RefundOrder = Functions.GetDecimal((txtRefundOrder.Text));
                        objTbl.CancelledOrder = Functions.GetDecimal((txtCanceledOrder.Text));

                        objTbl.UpdatedBy = LoginInfo.UserId;
                        objTbl.UpdatedDate = DateTime.Now;
                        #endregion
                    }
                    else
                    {
                        #region Add New Record
                        tbl_TillBalancing _tbl_TillBalancing = new tbl_TillBalancing();
                        _tbl_TillBalancing.SelectedDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                        _tbl_TillBalancing.CashierID = Convert.ToInt64(cmbCashier.SelectedValue.ToString());
                        _tbl_TillBalancing.CashierName = cmbCashier.SelectedItem.ToString();

                        _tbl_TillBalancing.A_Cash = Functions.GetDecimal((txtA_Cash.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.A_Check = Functions.GetDecimal((txtA_Check.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.A_CreaditCard = Functions.GetDecimal((txtA_CreaditCard.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.A_FoodStamp = Functions.GetDecimal((txtA_FoodStamp.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        _tbl_TillBalancing.B_Cash = Functions.GetDecimal((txtB_Cash.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.B_Check = Functions.GetDecimal((txtB_Check.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.B_CreaditCard = Functions.GetDecimal((txtB_CreaditCard.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.B_FoodStamp = Functions.GetDecimal((txtB_FoodStamp.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        _tbl_TillBalancing.S_Cash = Functions.GetDecimal((txtS_Cash.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.S_Check = Functions.GetDecimal((txtS_Check.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.S_CreaditCard = Functions.GetDecimal((txtS_CreaditCard.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.S_FoodStamp = Functions.GetDecimal((txtS_FoodStamp.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        _tbl_TillBalancing.A_Total = Functions.GetDecimal((txtActualTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.B_Total = Functions.GetDecimal((txtBalanceTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.S_Total = Functions.GetDecimal((txtSystemTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        _tbl_TillBalancing.GrossAmount = Functions.GetDecimal((txtGrossAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.TaxAmount = Functions.GetDecimal((txtTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.AveOrder = Functions.GetDecimal((txtAveOrder.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.RefundAmount = Functions.GetDecimal((txtRefundAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.CheckRefund = Functions.GetDecimal((txtCheckRefund.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                        _tbl_TillBalancing.CancelledAmount = Functions.GetDecimal((txtCanceledAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));

                        _tbl_TillBalancing.TotalOrder = Functions.GetDecimal((txtTotalOrder.Text));
                        _tbl_TillBalancing.RefundOrder = Functions.GetDecimal((txtRefundOrder.Text));
                        _tbl_TillBalancing.CancelledOrder = Functions.GetDecimal((txtCanceledOrder.Text));

                        _tbl_TillBalancing.CreatedBy = LoginInfo.UserId;
                        _tbl_TillBalancing.CreatedDate = DateTime.Now;

                        _db.tbl_TillBalancing.Add(_tbl_TillBalancing);
                        #endregion
                    }

                    _db.SaveChanges();
                    ClsCommon.MsgBox("Information", "Data have been saved successfully.", false);
                    Clear();
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please select cashier", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "frmTillStatus -->> btnSave_Click" + ex.StackTrace, ex.LineNumber());
            }

        }

        private void txtA_Cash_Leave(object sender, EventArgs e)
        {
            if (txtA_Cash.Text == "" || txtA_Cash.Text == "0" || txtA_Cash.Text == "$")
            {
                txtA_Cash.Text = "0.00";
            }
            try
            {
                txtB_Cash.Text = "$" + ((Convert.ToDecimal(txtA_Cash.Text.Replace("$", "")) - Convert.ToDecimal(txtS_Cash.Text.Replace("$", ""))).ToString());
                txtA_Cash.Text = "$" + Convert.ToDecimal(txtA_Cash.Text.Replace("$", "")).ToString("0.00");
                txtA_Cash.SelectionStart = txtA_Cash.Text.Length;
                txtA_Cash.SelectionLength = 0;
                TotalAmount();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog("Frm-TillStatus-txtA_Cash_Leave", ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtA_Check_Leave(object sender, EventArgs e)
        {
            if (txtA_Check.Text == "" || txtA_Check.Text == "0" || txtA_Check.Text == "$")
            {
                txtA_Check.Text = "0.00";
            }
            try
            {
                txtB_Check.Text = "$" + ((Convert.ToDecimal(txtA_Check.Text.Replace("$", "")) - Convert.ToDecimal(txtS_Check.Text.Replace("$", ""))).ToString());
                txtA_Check.Text = "$" + Convert.ToDecimal(txtA_Check.Text.Replace("$", "")).ToString("0.00");
                txtA_Check.SelectionStart = txtA_Check.Text.Length;
                txtA_Check.SelectionLength = 0;
                TotalAmount();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog("Frm-TillStatus-txtA_Check_Leave", ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtA_CreaditCard_Leave(object sender, EventArgs e)
        {
            if (txtA_CreaditCard.Text == "" || txtA_CreaditCard.Text == "0" || txtA_CreaditCard.Text == "$")
            {
                txtA_CreaditCard.Text = "0.00";
            }
            try
            {
                txtB_CreaditCard.Text = "$" + ((Convert.ToDecimal(txtA_CreaditCard.Text.Replace("$", "")) - Convert.ToDecimal(txtS_CreaditCard.Text.Replace("$", ""))).ToString());
                txtA_CreaditCard.Text = "$" + Convert.ToDecimal(txtA_CreaditCard.Text.Replace("$", "")).ToString("0.00");
                txtA_CreaditCard.SelectionStart = txtA_CreaditCard.Text.Length;
                txtA_CreaditCard.SelectionLength = 0;
                TotalAmount();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog("Frm-TillStatus-txtA_CreaditCard_Leave", ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtA_FoodStamp_Leave(object sender, EventArgs e)
        {
            if (txtA_FoodStamp.Text == "" || txtA_FoodStamp.Text == "0" || txtA_FoodStamp.Text == "$")
            {
                txtA_FoodStamp.Text = "0.00";
            }
            try
            {
                txtB_FoodStamp.Text = "$" + ((Convert.ToDecimal(txtA_FoodStamp.Text.Replace("$", "")) - Convert.ToDecimal(txtS_FoodStamp.Text.Replace("$", ""))).ToString());
                txtA_FoodStamp.Text = "$" + Convert.ToDecimal(txtA_FoodStamp.Text.Replace("$", "")).ToString("0.00");
                txtA_FoodStamp.SelectionStart = txtA_FoodStamp.Text.Length;
                txtA_FoodStamp.SelectionLength = 0;
                TotalAmount();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog("Frm-TillStatus-txtA_FoodStamp_Leave", ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void TotalAmount()
        {
            try
            {
                txtActualTotal.Text = "$" + ((Convert.ToDecimal(txtA_Cash.Text.Replace("$", ""))) + (Convert.ToDecimal(txtA_Check.Text.Replace("$", ""))) + (Convert.ToDecimal(txtA_CreaditCard.Text.Replace("$", ""))) + (Convert.ToDecimal(txtA_FoodStamp.Text.Replace("$", "")))).ToString();
                txtSystemTotal.Text = "$" + ((Convert.ToDecimal(txtS_Cash.Text.Replace("$", ""))) + (Convert.ToDecimal(txtS_Check.Text.Replace("$", ""))) + (Convert.ToDecimal(txtS_CreaditCard.Text.Replace("$", ""))) + (Convert.ToDecimal(txtS_FoodStamp.Text.Replace("$", "")))).ToString();
                txtBalanceTotal.Text = "$" + ((Convert.ToDecimal(txtB_Cash.Text.Replace("$", ""))) + (Convert.ToDecimal(txtB_Check.Text.Replace("$", ""))) + (Convert.ToDecimal(txtB_CreaditCard.Text.Replace("$", ""))) + (Convert.ToDecimal(txtB_FoodStamp.Text.Replace("$", "")))).ToString();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog("Frm-TillStatus-TotalAmount", ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void Clear()
        {
            btnSave.Text = "Save";
            btnSave.Enabled = false;
            //
            txtA_Cash.Text = "$0.00";
            txtA_Check.Text = "$0.00";
            txtA_CreaditCard.Text = "$0.00";
            txtA_FoodStamp.Text = "$0.00";
            //
            txtB_Cash.Text = "$0.00";
            txtB_Check.Text = "$0.00";
            txtB_CreaditCard.Text = "$0.00";
            txtB_FoodStamp.Text = "$0.00";
            //            
            txtS_Cash.Text = "$0.00";
            txtS_Check.Text = "$0.00";
            txtS_CreaditCard.Text = "$0.00";
            txtS_FoodStamp.Text = "$0.00";
            //
            txtGrossAmount.Text = "$0.00";
            txtTaxAmount.Text = "$0.00";
            txtTotalOrder.Text = "$0.00";
            txtAveOrder.Text = "$0.00";
            //
            txtRefundAmount.Text = "$0.00";
            txtCanceledAmount.Text = "$0.00";
            txtRefundOrder.Text = "$0.00";
            txtCheckRefund.Text = "$0.00";
            txtCanceledOrder.Text = "$0.00";
            txtOverrideTotal.Text = "$0.00";
            txtOverridwOrder.Text = "$0.00";
            //
            txtActualTotal.Text = "$0.00";
            txtSystemTotal.Text = "$0.00";
            txtBalanceTotal.Text = "$0.00";
        }

        private void txtStartDate_ValueChanged(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
