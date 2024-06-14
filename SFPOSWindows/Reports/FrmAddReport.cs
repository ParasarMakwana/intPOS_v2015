using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SFPOSWindows.MenuForm;
using SFPOSWindows.Metro_Forms;

namespace SFPOSWindows.Reports
{
    public partial class FrmAddReport : Form
    {
        public static decimal _BackInDrawer = 0;
        public static decimal _SortBy = 0;
        public static decimal _OverBy = 0;
        public static decimal ReportedBalance = 0;
        public static long TillStatusReportID = 0;
        public static decimal Cash = 0;
        public static decimal CreditCard = 0;
        public static decimal Check = 0;
        public static decimal CashPayout = 0;
        TillStatusService objTillStatusService = new TillStatusService();
        ErrorProvider ep = new ErrorProvider();
        public FrmAddReport()
        {
            InitializeComponent();
        }

        private void FrmAddReport_Load(object sender, EventArgs e)
        {
            LoadCmbCashierCode();
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(cmbCashiers.SelectedValue.ToString()) > 0)
                {
                    string msg1 = "You Are Submitting for " + cmbCashiers.Text.ToString() + "," + lblSortOver.Text;
                    string msg2 = "Verifier:" + LoginInfo.UserName;
                    string msg3 = "I, " + LoginInfo.UserName + ", verified that these inputs are correct.";
                    ClsCommon.MsgBoxForVerified("Information", msg1, msg2, msg3, true);

                    if (LoginInfo.IsVerifiedTillStatus)
                    {
                        TillReportModel _TillReportModel = new TillReportModel();
                        _TillReportModel.TillStatusReportID = TillStatusReportID;
                        _TillReportModel.SelectedDate = Convert.ToDateTime(txtDate.Text);
                        _TillReportModel.CashierID = Convert.ToInt64(cmbCashiers.SelectedValue.ToString());
                        _TillReportModel.CashierName = cmbCashiers.Text.ToString();
                        _TillReportModel.Coin = Convert.ToDecimal(Convert.ToDecimal(txtCoin.Text) / 100);
                        _TillReportModel.Cash = Convert.ToDecimal(txtCash.Text);
                        _TillReportModel.CreditCard = Convert.ToDecimal(Convert.ToDecimal(txtCreditCard.Text) / 100);
                        _TillReportModel.Checks = Convert.ToDecimal(Convert.ToDecimal(txtCheck.Text) / 100);
                        _TillReportModel.CashPayout = Convert.ToDecimal(txtCashPayout.Text);
                        _TillReportModel.BackInDrawer = _BackInDrawer;
                        _TillReportModel.SortBy = _SortBy;
                        _TillReportModel.OverBy = _OverBy;
                        _TillReportModel.Lotto = txtLotto.Text == "" ? Convert.ToDecimal(null) : Convert.ToDecimal(txtLotto.Text);
                        _TillReportModel.SelfService = txtselfService.Text == "" ? Convert.ToDecimal(null) : Convert.ToDecimal(txtselfService.Text);
                        _TillReportModel.Scrathers = txtScratchers.Text == "" ? Convert.ToDecimal(null) : Convert.ToDecimal(txtScratchers.Text);
                        _TillReportModel.CreatedDate = DateTime.Now;
                        _TillReportModel.CreatedBy = LoginInfo.UserId;
                        //var t = ClsCommon.getdata(_TillReportModel, "SP_TillStatusEmployeeReportInsert", _TillReportModel);
                        //ClsCommon.MsgBox("Information", "Cashier's report added!", false);
                        if (TillStatusReportID <= 0)
                        {
                            var add = objTillStatusService.AddTillStatus(_TillReportModel, 1);
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                        else if (TillStatusReportID > 0)
                        {
                            _TillReportModel.TillStatusReportID= TillStatusReportID;
                            var add = objTillStatusService.AddTillStatus(_TillReportModel, 2);
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Update, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                        LoadCmbCashierCode();
                        Clear();
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "select Cashier's Name", false);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            LoadCmbCashierCode();
            //Clear();
            //GetReportData(Convert.ToInt64(cmbCashiers.SelectedValue), Convert.ToDateTime(txtDate.Text));
        }

        private void cmbCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            if(((EmployeeMasterModel)cmbCashiers.SelectedItem).IsCashPayout)
            {
                lblCashPayout.Visible = true;
                txtCashPayout.Visible = true;
            }
            else
            {
                lblCashPayout.Visible = false;
                txtCashPayout.Visible = false;
            }
            if (((EmployeeMasterModel)cmbCashiers.SelectedItem).IsLottoFunction)
            {
                lblLotto.Visible = true;
                lblSelfService.Visible = true;
                lblScratchers.Visible = true;
                txtLotto.Visible = true;
                txtselfService.Visible = true;
                txtScratchers.Visible = true;
            }
            else
            {
                lblLotto.Visible = false;
                lblSelfService.Visible = false;
                lblScratchers.Visible = false;
                txtLotto.Visible = false;
                txtselfService.Visible = false;
                txtScratchers.Visible = false;
            }
            GetReportData(Convert.ToInt64(cmbCashiers.SelectedValue), Convert.ToDateTime(txtDate.Text));
            Cal();
            if(!InputValidation())
            {
                btnSubmit.Enabled = false;
                btnClear.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
                btnClear.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
            //frmMnTillStatus objTillstatus = new frmMnTillStatus();
            //objTillstatus.Show();
            //FrmMetroMaster.con
        }

        private void txtCoin_Leave(object sender, EventArgs e)
        {
            try
            {
                _BackInDrawer = 208 - Convert.ToDecimal(txtCoin.Text) / 100;
                lblNeedCashDrawer.Visible = true;
                lblNeedCashDrawer.Text = "*Need $" + Convert.ToInt32(_BackInDrawer) + " back in drawer.";
                Cal();
            }
            catch (Exception ex)
            {
            }
        }
        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (txtCash.Text.Contains("."))
            {
                txtCash.Text = txtCash.Text.Replace(".", "").Trim();
            }
            Cal();
        }

        private void txtCreditCard_Leave(object sender, EventArgs e)
        {
            if (txtCreditCard.Text.Contains("."))
            {
                txtCreditCard.Text = txtCreditCard.Text.Replace(".", "").Trim();
            }
            Cal();
        }

        private void txtCheck_Leave(object sender, EventArgs e)
        {
            if (txtCheck.Text.Contains("."))
            {
                txtCheck.Text = txtCheck.Text.Replace(".", "").Trim();
            }
            Cal();
        }

        public bool InputValidation()
        {
            bool retval = true;
            retval = txtcoinvalidate();
            if (retval)
            {
                retval = txtCashvalidate();
            }
            if (retval)
            {
                retval = txtCreditCardvalidate();
            }
            if (retval)
            {
                retval = txtCheckvalidate(); 
            }
            if (retval)
            {
                retval = txtLottovalidate();
            }
            if (retval)
            {
                retval = txtSelfServicevalidate();
            }
            if (retval)
            {
                retval = txtScratchersvalidate();
            }
            return retval;
        }
        #region Input Validations
        private void txtCoin_TextChanged(object sender, EventArgs e)
        {
            txtcoinvalidate();
        }

        public bool txtcoinvalidate()
        {
            bool retval = true;
            if (Convert.ToInt64(cmbCashiers.SelectedValue) != 0)
            {
                if (string.IsNullOrWhiteSpace(txtCoin.Text))
                {
                    txtCoin.Focus();
                    ep.SetError(txtCoin, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else if ((!(Regex.Match(txtCoin.Text, @"^[0-9]\d*(\.\d+)?$")).Success))
                {
                    txtCoin.Focus();
                    ep.SetError(txtCoin, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else
                {
                    retval = true;
                    btnSubmit.Enabled = true;
                    btnClear.Enabled = true;
                    ep.Clear();
                }
            }
            return retval;
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            txtCashvalidate();
        }

        public bool txtCashvalidate()
        {
            bool retval = true;
            if (Convert.ToInt64(cmbCashiers.SelectedValue) != 0)
            {
                if (string.IsNullOrWhiteSpace(txtCash.Text))
                {
                    txtCash.Focus();
                    ep.SetError(txtCash, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else if ((!(Regex.Match(txtCash.Text, @"^[0-9]\d*(\.\d+)?$")).Success))
                {
                    txtCash.Focus();
                    ep.SetError(txtCash, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else
                {
                    retval = true;
                    btnSubmit.Enabled = true;
                    btnClear.Enabled = true;
                    ep.Clear();
                }
            }
            return retval;
        }

        private void txtCreditCard_TextChanged(object sender, EventArgs e)
        {
            txtCreditCardvalidate();
        }

        public bool txtCreditCardvalidate()
        {
            bool retval = true;
            if (Convert.ToInt64(cmbCashiers.SelectedValue) != 0)
            {
                if (string.IsNullOrWhiteSpace(txtCreditCard.Text))
                {
                    txtCreditCard.Focus();
                    ep.SetError(txtCreditCard, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else if ((!(Regex.Match(txtCreditCard.Text, @"^[0-9]\d*(\.\d+)?$")).Success))
                {
                    txtCreditCard.Focus();
                    ep.SetError(txtCreditCard, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else
                {
                    retval = true;
                    btnSubmit.Enabled = true;
                    btnClear.Enabled = true;
                    ep.Clear();
                }
            }
            return retval;
        }
        private void txtCheck_TextChanged(object sender, EventArgs e)
        {
            txtCheckvalidate();
        }

        public bool txtCheckvalidate()
        {
            bool retval = true;
            if (Convert.ToInt64(cmbCashiers.SelectedValue) != 0)
            {
                if (string.IsNullOrWhiteSpace(txtCheck.Text))
                {
                    txtCheck.Focus();
                    ep.SetError(txtCheck, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else if ((!(Regex.Match(txtCheck.Text, @"^[0-9]\d*(\.\d+)?$")).Success))
                {
                    txtCheck.Focus();
                    ep.SetError(txtCheck, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else
                {
                    retval = true;
                    btnSubmit.Enabled = true;
                    btnClear.Enabled = true;
                    ep.Clear();
                }
            }
            return retval;
        }

        private void txtLotto_TextChanged(object sender, EventArgs e)
        {
            txtLottovalidate();
        }
        public bool txtLottovalidate()
        {
            bool retval = true;
            if (Convert.ToInt64(cmbCashiers.SelectedValue) != 0)
            {
                if (string.IsNullOrWhiteSpace(txtLotto.Text))
                {
                    txtLotto.Focus();
                    ep.SetError(txtLotto, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else if ((!(Regex.Match(txtLotto.Text, @"^[0-9]\d*(\.\d+)?$")).Success))
                {
                    txtLotto.Focus();
                    ep.SetError(txtLotto, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else
                {
                    retval = true;
                    btnSubmit.Enabled = true;
                    btnClear.Enabled = true;
                    ep.Clear();
                }
            }
            return retval;
        }
        private void txtSelfService_TextChanged(object sender, EventArgs e)
        {
            txtSelfServicevalidate();
        }
        public bool txtSelfServicevalidate()
        {
            bool retval = true;
            if (Convert.ToInt64(cmbCashiers.SelectedValue) != 0)
            {
                if (string.IsNullOrWhiteSpace(txtselfService.Text))
                {
                    txtselfService.Focus();
                    ep.SetError(txtselfService, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else if ((!(Regex.Match(txtselfService.Text, @"^[0-9]\d*(\.\d+)?$")).Success))
                {
                    txtselfService.Focus();
                    ep.SetError(txtselfService, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else
                {
                    retval = true;
                    btnSubmit.Enabled = true;
                    btnClear.Enabled = true;
                    ep.Clear();
                }
            }
            return retval;
        }
        private void txtScratchers_TextChanged(object sender, EventArgs e)
        {
            txtScratchersvalidate();
        }
        public bool txtScratchersvalidate()
        {
            bool retval = true;
            if (Convert.ToInt64(cmbCashiers.SelectedValue) != 0)
            {
                if (string.IsNullOrWhiteSpace(txtScratchers.Text))
                {
                    txtScratchers.Focus();
                    ep.SetError(txtScratchers, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else if ((!(Regex.Match(txtScratchers.Text, @"^[0-9]\d*(\.\d+)?$")).Success))
                {
                    txtScratchers.Focus();
                    ep.SetError(txtScratchers, "Input is not valid!");
                    btnSubmit.Enabled = false;
                    btnClear.Enabled = false;
                    retval = false;
                }
                else
                {
                    retval = true;
                    btnSubmit.Enabled = true;
                    btnClear.Enabled = true;
                    ep.Clear();
                }
            }
            return retval;
        }
        #endregion

        public void LoadCmbCashierCode()
        {
            try
            {
                EmployeeService _EmployeeService = new EmployeeService();
                EmployeeMasterModel userModel = new EmployeeMasterModel();
                DateTime Datval = Convert.ToDateTime(txtDate.Text);
                List<EmployeeMasterModel> UserModel = _EmployeeService.GetAllEmployeeByTransDate(Datval);//ClsCommon.getdata(userModel, "SP_GetEmployeeList", null);
                //List<EmployeeMasterModel> UserModel = new List<EmployeeMasterModel>();
                userModel.EmployeeID = 0;
                userModel.FullName = "-- Cashier --";
                userModel.IsCashPayout = false;
                userModel.IsLottoFunction = false;
                UserModel.Insert(0, userModel);
                cmbCashiers.DisplayMember = "FullName";
                cmbCashiers.ValueMember = "EmployeeID";
                cmbCashiers.DataSource = UserModel;
                btnSubmit.Enabled = false;
                btnClear.Enabled = false;
                Clear();
            }
            catch (Exception ex)
            {

            }
        }
        public void GetReportData(long EmployeeID, DateTime date)
        {
            try
            {
                TillReportModel _TillReportModel = new TillReportModel();
                ReportReq _ReportReq = new ReportReq();
                _ReportReq.EmployeeID = EmployeeID;
                _ReportReq.Date = date;
                List<TillReportModel> lst = objTillStatusService.GetTillStatusEmployeeReport(EmployeeID, date); //ClsCommon.getdata(_TillReportModel, "SP_TillStatusEmployeeReport", _ReportReq);

                if (lst != null && lst.Count > 0)
                {
                    _TillReportModel = new TillReportModel();
                    _TillReportModel = lst.FirstOrDefault();
                    if (_TillReportModel.TillStatusReportID > 0)
                    {
                        TillStatusReportID = _TillReportModel.TillStatusReportID;
                        txtCoin.Text = !String.IsNullOrEmpty(_TillReportModel.Coin.ToString()) ? Convert.ToString(_TillReportModel.Coin).Replace(".", "") : "0";
                        txtCash.Text = !String.IsNullOrEmpty(_TillReportModel.Cash.ToString()) ? Convert.ToString(_TillReportModel.Cash).Replace(".00", "").Trim() : "0";
                        txtCreditCard.Text = !String.IsNullOrEmpty(_TillReportModel.CreditCard.ToString()) ? Convert.ToString(_TillReportModel.CreditCard).Replace(".", "").Trim() : "0";
                        txtCheck.Text = !String.IsNullOrEmpty(_TillReportModel.Checks.ToString()) ? Convert.ToString(_TillReportModel.Checks).Replace(".", "").Trim() : "0";
                        txtCashPayout.Text = !String.IsNullOrEmpty(_TillReportModel.CashPayout.ToString()) ? Convert.ToString(_TillReportModel.CashPayout).Replace(".00", "").Trim() : "0";
                        txtLotto.Text = !String.IsNullOrEmpty(_TillReportModel.Lotto.ToString()) ? Convert.ToString(_TillReportModel.Lotto).Replace(".00", "").Trim() : "0";
                        txtselfService.Text = !String.IsNullOrEmpty(_TillReportModel.SelfService.ToString()) ? Convert.ToString(_TillReportModel.SelfService).Replace(".00", "").Trim() : "0";
                        txtScratchers.Text = !String.IsNullOrEmpty(_TillReportModel.Scrathers.ToString()) ? Convert.ToString(_TillReportModel.Scrathers).Replace(".00", "").Trim() : "0";
                        if (_TillReportModel.BackInDrawer > 0)
                        {
                            lblNeedCashDrawer.Visible = true;
                            lblNeedCashDrawer.Text = "*Need $" + _TillReportModel.BackInDrawer + " back in drawer.";
                        }
                        if (_TillReportModel.SortBy != 0 || _TillReportModel.OverBy != 0)
                        {
                            if (_TillReportModel.SortBy > 0)
                            {
                                lblSortOver.Visible = false;
                                lblSortOver.Text = "*Short by $" + _TillReportModel.SortBy + " amount.";
                            }
                            if (_TillReportModel.OverBy > 0)
                            {
                                lblSortOver.Visible = false;
                                lblSortOver.Text = "*Over by $" + _TillReportModel.OverBy + " amount.";
                            }
                        }
                        else
                        {
                            lblSortOver.Visible = false;
                            lblSortOver.Text = "";
                        }
                        if(_TillReportModel.Verifier.ToString() != "")
                        {
                            lblVerifiedBy.Visible = true;
                            lblVerifiedBy.Text = "*Verified by " + _TillReportModel.Verifier + ".";
                        }
                        else
                        {
                            lblVerifiedBy.Visible = false;
                            lblVerifiedBy.Text = "";
                        }
                        EnableDisable(0);
                    }
                    else
                    {
                        TillStatusReportID = 0;
                    }
                }
                else
                {
                    TillStatusReportID = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void GetSystemData(long EmployeeID, DateTime date)
        {
            try
            {
                ReportStatusModel _ReportStatusModel = new ReportStatusModel();
                ReportReq _ReportReq = new ReportReq();
                _ReportReq.EmployeeID = EmployeeID;
                _ReportReq.Date = date;
                //List<ReportStatusModel> lst = ClsCommon.getdata(_ReportStatusModel, "SP_TillStatusEmployeeReport_System", _ReportReq);
                List<ReportStatusModel> lst = objTillStatusService.GetTillStatusEmployeeReport_System(EmployeeID, date);
                if (lst != null && lst.Count > 0)
                {
                    _ReportStatusModel = new ReportStatusModel();
                    _ReportStatusModel = lst.FirstOrDefault();

                    ReportedBalance = _ReportStatusModel.ReportedBalance;
                }
                else
                {
                    // Clear();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Clear()
        {
            ep.Clear();
            txtCoin.Text = "";
            txtCash.Text = "";
            txtCreditCard.Text = "";
            txtCheck.Text = "";
            txtCashPayout.Text = "";
            txtScratchers.Text = "";
            txtLotto.Text = "";
            txtselfService.Text = "";
            lblNeedCashDrawer.Visible = false;
            lblSortOver.Visible = false;
            lblVerifiedBy.Visible = false;
            _BackInDrawer = 0;
            _SortBy = 0;
            _OverBy = 0;
            ReportedBalance = 0;
            //cmbCashiers.SelectedIndex = 0;
            //LoadCmbCashierCode();
        }

        public void GetVal()
        {
            try
            {
                Cash = (!String.IsNullOrWhiteSpace(txtCash.Text) ? Convert.ToDecimal(txtCash.Text) : 0);
                CreditCard = (!String.IsNullOrWhiteSpace(txtCreditCard.Text) ? Convert.ToDecimal(Convert.ToDecimal(txtCreditCard.Text) / 100) : 0);
                Check = (!String.IsNullOrWhiteSpace(txtCheck.Text) ? Convert.ToDecimal(Convert.ToDecimal(txtCheck.Text) / 100) : 0);
                CashPayout = (!String.IsNullOrWhiteSpace(txtCashPayout.Text) ? Convert.ToDecimal(txtCashPayout.Text) : 0);

                txtCash.Text = (!String.IsNullOrWhiteSpace(txtCash.Text) ? txtCash.Text : "0");
                txtCreditCard.Text = (!String.IsNullOrWhiteSpace(txtCreditCard.Text) ? txtCreditCard.Text : "0");
                txtCheck.Text = (!String.IsNullOrWhiteSpace(txtCheck.Text) ? txtCheck.Text : "0");
                txtCashPayout.Text = (!String.IsNullOrWhiteSpace(txtCashPayout.Text) ? txtCashPayout.Text : "0");
            }
            catch (Exception e)
            {

            }
        }
        public void Cal()
        {
            try
            {

                decimal FinalCal = 0;
                GetVal();
                if (ReportedBalance == 0)
                    GetSystemData(Convert.ToInt64(cmbCashiers.SelectedValue), Convert.ToDateTime(txtDate.Text));
                FinalCal = ReportedBalance - (Cash + CreditCard + Check + CashPayout);
                if (FinalCal > 0)
                {
                    _OverBy = 0;
                    _SortBy = FinalCal;
                    _OverBy = 0;
                    lblSortOver.Visible = false;
                    lblSortOver.Text = "*Short by " + _SortBy + " amount.";
                }
                else if (FinalCal < 0)
                {
                    _OverBy = FinalCal;
                    _SortBy = 0;
                    lblSortOver.Visible = false;
                    lblSortOver.Text = "*Over by " + _OverBy + " amount.";
                }
                else
                {
                    _SortBy = 0;
                    _OverBy = 0;
                    lblSortOver.Text = "";
                    lblSortOver.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void EnableDisable(int status)
        {
            if (status == 1)
            {
                txtCoin.Enabled = true;
                txtCash.Enabled = true;
                txtCreditCard.Enabled = true;
                txtCheck.Enabled = true;
                txtCashPayout.Enabled = true;
                txtLotto.Enabled = true;
                txtselfService.Enabled = true;
                txtScratchers.Enabled = true;
                btnSubmit.Enabled = true;
                btnClear.Enabled = true;
                txtCoin.Focus();
            }
            else
            {
                txtCoin.Enabled = false;
                txtCash.Enabled = false;
                txtCreditCard.Enabled = false;
                txtCheck.Enabled = false;
                txtCashPayout.Enabled = false;
                txtLotto.Enabled = false;
                txtselfService.Enabled = false;
                txtScratchers.Enabled = false;
                btnSubmit.Enabled = false;
                btnClear.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnableDisable(1);
        }

        private void txtCoin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtCash.Focus();
            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtCreditCard.Focus();
            }
        }

        private void txtCreditCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtCheck.Focus();
            }
        }

        private void txtCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSubmit.Focus();
            }
        }
    }
}
