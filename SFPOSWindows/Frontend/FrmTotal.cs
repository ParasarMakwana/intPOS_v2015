using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.CustomControl;
using System;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
namespace SFPOSWindows.Frontend
{
    public partial class FrmTotal : MetroForm
    {
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        decimal totalReceivedAmt = 0;
        string receiveAmt = "";
        ErrorProvider ep = new ErrorProvider();
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public FrmTotal()
        {
            InitializeComponent();
        }

        private void txtReceiveAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    UpdateTotal();
                    //if (txtReceiveAmt.Text != CommonModelCont.EmptyString)
                    //{
                    //    string result = "";
                    //    if (txtReceiveAmt.Text.Length >= 3)
                    //    {
                    //        result = txtReceiveAmt.Text.Substring(txtReceiveAmt.Text.Length - 2);
                    //    }
                    //    decimal ReceiveCashAmt = 0;
                    //    decimal ReceiveCheckAmt = 0;
                    //    decimal ReceiveCreditAmt = 0;

                    //    #region CLEAR
                    //    if (txtReceiveAmt.Text.Trim().ToLower().Contains("cl"))
                    //    {
                    //        txtReceiveAmt.Text = "";
                    //    }
                    //    #endregion

                    //    #region CREDIT CARD
                    //    else if (txtReceiveAmt.Text.Trim().ToLower().Contains("ta"))
                    //    {
                    //        if (txtReceiveAmt.Text.ToLower().Replace("t", "").Replace("a", "") == "")
                    //        {
                    //            if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    //            {
                    //                if (Functions.GetDecimal(txtChange.Text.Replace("$", "")) == Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")))
                    //                {
                    //                    if (txtRemainingAmount.Text.Replace("$", "") != "" && txtRemainingAmount.Text.Replace("$", "") != "0")
                    //                    {
                    //                        ReceiveCreditAmt = Functions.GetDecimal(txtRemainingAmount.Text.Replace("$", ""));
                    //                        OrderInfo.CreditAmt += ReceiveCreditAmt;
                    //                        AddCreditPaymentList(ReceiveCreditAmt);
                    //                    }
                    //                    else
                    //                    {
                    //                        ReceiveCreditAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""));
                    //                        OrderInfo.CreditAmt += ReceiveCreditAmt;
                    //                        AddCreditPaymentList(ReceiveCreditAmt);
                    //                        if (OrderInfo.CreditAmt < 0)
                    //                        {
                    //                            OrderInfo.CreditAmt = Functions.GetDecimal(OrderInfo.CreditAmt.ToString().Replace("-", ""));
                    //                        }
                    //                    }
                    //                    OrderInfo.Change = 0;
                    //                    totalReceivedAmt += ReceiveCreditAmt;
                    //                    txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                    //                    txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                    //                    txtReceiveAmt.Text = "";
                    //                }
                    //                else
                    //                {
                    //                    ClsCommon.MsgBox("Information", "Refund Amount must be equal to the Total Amount.", false);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (txtRemainingAmount.Text.Replace("$", "") != "" && txtRemainingAmount.Text.Replace("$", "") != "0")
                    //                {
                    //                    ReceiveCreditAmt = Functions.GetDecimal(txtRemainingAmount.Text.Replace("$", ""));
                    //                    OrderInfo.CreditAmt += ReceiveCreditAmt;
                    //                    AddCreditPaymentList(ReceiveCreditAmt);
                    //                }
                    //                else
                    //                {
                    //                    ReceiveCreditAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""));
                    //                    OrderInfo.CreditAmt += ReceiveCreditAmt;
                    //                    AddCreditPaymentList(ReceiveCreditAmt);
                    //                    if (OrderInfo.CreditAmt < 0)
                    //                    {
                    //                        OrderInfo.CreditAmt = Functions.GetDecimal(OrderInfo.CreditAmt.ToString().Replace("-", ""));
                    //                    }
                    //                }
                    //                OrderInfo.Change = 0;
                    //                totalReceivedAmt += ReceiveCreditAmt;
                    //                txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                    //                txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                    //                txtReceiveAmt.Text = "";
                    //            }

                    //        }

                    //        else if (txtReceiveAmt.Text.ToLower().Replace("t", "").Replace("a", "") != "")
                    //        {
                    //            ReceiveCreditAmt = Functions.GetDecimal(Regex.Replace(txtReceiveAmt.Text, "[^0-9.]", ""));
                    //            if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    //            {
                    //                if (Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", "")) == 0)
                    //                {
                    //                    if (ReceiveCreditAmt != 0)
                    //                    {
                    //                        creditCardAmount(ReceiveCreditAmt / 100);
                    //                        AddCreditPaymentList(ReceiveCreditAmt / 100);
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    ClsCommon.MsgBox("Information", "Refund Amount must be equal to the Total Amount.", false);
                    //                    txtReceiveAmt.Text = "";
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (ReceiveCreditAmt != 0)
                    //                {
                    //                    creditCardAmount(ReceiveCreditAmt / 100);
                    //                    AddCreditPaymentList(ReceiveCreditAmt / 100);
                    //                }
                    //            }
                    //        }

                    //        if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    //        {
                    //            if (totalReceivedAmt < 0)
                    //            {
                    //                if ((totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                    //                {
                    //                    txtTotalAmt.Text = "";
                    //                    txtReceiveAmt.Text = "";
                    //                    OrderInfo.PaymentType = 2;
                    //                    totalReceivedAmt = 0;
                    //                    receiveAmt = "";
                    //                    OrderInfo.IsOrder = true;
                    //                    this.Close();
                    //                }
                    //            }
                    //            else if (-(totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                    //            {
                    //                txtTotalAmt.Text = "";
                    //                txtReceiveAmt.Text = "";
                    //                OrderInfo.PaymentType = 2;
                    //                totalReceivedAmt = 0;
                    //                receiveAmt = "";
                    //                OrderInfo.IsOrder = true;
                    //                this.Close();
                    //            }
                    //        }
                    //        else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    //        {
                    //            txtTotalAmt.Text = "";
                    //            txtReceiveAmt.Text = "";
                    //            OrderInfo.PaymentType = 2;
                    //            totalReceivedAmt = 0;
                    //            receiveAmt = "";
                    //            OrderInfo.IsOrder = true;
                    //            this.Close();
                    //        }
                    //    }
                    //    #endregion

                    //    #region CASH
                    //    else if (result.Trim().ToLower() == "ca")
                    //    {
                    //        //ReceiveCashAmt = txtReceiveAmt.Text.ToLower().Replace("c", "").Replace("a", "") != "" ? Functions.GetDecimal(txtReceiveAmt.Text.ToLower().Replace("c", "").Replace("a", "")) : 0;
                    //        ReceiveCashAmt = Functions.GetDecimal(Regex.Replace(txtReceiveAmt.Text, "[^0-9.]", ""));
                    //        if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    //        {
                    //            if (Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", "")) == 0)
                    //            {
                    //                if (ReceiveCashAmt != 0)
                    //                {
                    //                    cashAmount(ReceiveCashAmt / 100);
                    //                    AddCashPaymentList(ReceiveCashAmt / 100);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                txtReceiveAmt.Text = "";
                    //                ClsCommon.MsgBox("Information", "Refund Amount must be equal to the Total Amount.", false);

                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (ReceiveCashAmt != 0)
                    //            {
                    //                cashAmount(ReceiveCashAmt / 100);
                    //                AddCashPaymentList(ReceiveCashAmt / 100);
                    //            }
                    //        }
                    //        if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    //        {
                    //            if ((-totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                    //            {
                    //                txtTotalAmt.Text = "";
                    //                txtReceiveAmt.Text = "";
                    //                OrderInfo.PaymentType = 1;
                    //                totalReceivedAmt = 0;
                    //                receiveAmt = "";
                    //                OrderInfo.IsOrder = true;
                    //                this.Close();
                    //            }
                    //        }
                    //        else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    //        {
                    //            txtTotalAmt.Text = "";
                    //            txtReceiveAmt.Text = "";
                    //            OrderInfo.PaymentType = 1;
                    //            totalReceivedAmt = 0;
                    //            receiveAmt = "";
                    //            OrderInfo.IsOrder = true;
                    //            this.Close();
                    //        }
                    //    }
                    //    #endregion

                    //    #region CHECK
                    //    else if (result.Trim().ToLower() == "ck")
                    //    {
                    //        //ReceiveCheckAmt = txtReceiveAmt.Text.ToLower().Replace("c", "").Replace("k", "") != "" ? Functions.GetDecimal(txtReceiveAmt.Text.ToLower().Replace("c", "").Replace("k", "")) : 0;
                    //        ReceiveCheckAmt = Functions.GetDecimal(Regex.Replace(txtReceiveAmt.Text, "[^0-9.]", ""));
                    //        if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    //        {
                    //            if (Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", "")) == 0)
                    //            {
                    //                if (ReceiveCheckAmt != 0)
                    //                {
                    //                    CheckAmount(ReceiveCheckAmt / 100);
                    //                    AddCheckPaymentList(ReceiveCheckAmt / 100);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                txtReceiveAmt.Text = "";
                    //                ClsCommon.MsgBox("Information", "Refund Amount must be equal to the Total Amount.", false);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (ReceiveCheckAmt != 0)
                    //            {
                    //                CheckAmount(ReceiveCheckAmt / 100);
                    //                AddCheckPaymentList(ReceiveCheckAmt / 100);
                    //            }
                    //        }
                    //        if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    //        {
                    //            if ((-totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                    //            {
                    //                txtTotalAmt.Text = "";
                    //                txtReceiveAmt.Text = "";
                    //                OrderInfo.PaymentType = 3;
                    //                totalReceivedAmt = 0;
                    //                receiveAmt = "";
                    //                OrderInfo.IsOrder = true;
                    //                this.Close();
                    //            }
                    //        }
                    //        else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    //        {
                    //            txtTotalAmt.Text = "";
                    //            txtReceiveAmt.Text = "";
                    //            OrderInfo.PaymentType = 3;
                    //            totalReceivedAmt = 0;
                    //            receiveAmt = "";
                    //            OrderInfo.IsOrder = true;
                    //            this.Close();
                    //        }
                    //    }
                    //    #endregion

                    //    #region COUPON
                    //    else if (txtReceiveAmt.Text.Trim().ToLower() == "cc")
                    //    {
                    //        if (CouponInfo.isCoupon)
                    //        {
                    //            ClsCommon.MsgBox("Information", "Can't apply multiple coupon om single order.!", false);
                    //            txtReceiveAmt.Text = "";
                    //            txtReceiveAmt.Focus();
                    //            return;
                    //        }
                    //        PortOpen_Close(false);
                    //        FrmCheckCoupon objCheckCoupon = new FrmCheckCoupon();
                    //        objCheckCoupon.MinPurchaseAmount = Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text));
                    //        objCheckCoupon.ShowDialog();
                    //        PortOpen_Close(true);
                    //        if (CouponInfo.isCoupon)
                    //        {
                    //            this.Size = new System.Drawing.Size(457, 427);
                    //            pnlCoupon.Visible = true;
                    //            pnlCoupon.Location = new System.Drawing.Point(33, 43);
                    //            pnlBalance.Location = new System.Drawing.Point(33, 124);
                    //            txtReceiveAmt.Location = new System.Drawing.Point(33, 233);
                    //            lblReceive.Location = new System.Drawing.Point(50, 203);
                    //            pnlChange.Location = new System.Drawing.Point(33, 304);
                    //            lblTotalReceivedAmt.Location = new System.Drawing.Point(47, 380);
                    //            txtTotalReceivedAmount.Location = new System.Drawing.Point(275, 380);

                    //            txtCoupon.Text = CouponInfo.CouponCode;
                    //            txtDiscount.Text = "(" + CouponInfo.Discount.ToString() + "%)";
                    //            txtDiscountAmount.Text = "$" + ((Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) * CouponInfo.Discount) / 100).ToString("0.00");
                    //            CouponInfo.DiscAmt = Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text));
                    //            txtRemainingAmount.Text = "$ " + (Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) - Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text)));
                    //            txtTotalAmt.Text = txtRemainingAmount.Text;
                    //        }
                    //        txtReceiveAmt.Text = "";
                    //        txtReceiveAmt.Focus();
                    //    }
                    //    #endregion

                    //    #region REMOVE COUPON
                    //    else if (txtReceiveAmt.Text.Trim().ToLower() == "rcc")
                    //    {
                    //        pnlCoupon.Visible = false;
                    //        pnlBalance.Location = new System.Drawing.Point(33, 43);
                    //        txtReceiveAmt.Location = new System.Drawing.Point(33, 172);
                    //        lblReceive.Location = new System.Drawing.Point(50, 142);
                    //        pnlChange.Location = new System.Drawing.Point(33, 259);
                    //        lblTotalReceivedAmt.Location = new System.Drawing.Point(50, 353);
                    //        txtTotalReceivedAmount.Location = new System.Drawing.Point(281, 353);

                    //        CouponInfo.isCoupon = false;
                    //        txtRemainingAmount.Text = "$ " + (Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) + Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text)));
                    //        txtDiscountAmount.Text = "$ 0.00";
                    //        txtTotalAmt.Text = txtRemainingAmount.Text;
                    //        txtReceiveAmt.Text = "";
                    //        txtReceiveAmt.Focus();
                    //    }
                    //    #endregion

                    //    #region CLOSE
                    //    else if (txtReceiveAmt.Text.Trim().ToLower() == "cn")
                    //    {
                    //        this.Close();
                    //    }
                    //    #endregion

                    //}
                }
            }
            catch (Exception ex)
            {
                OrderInfo.IsOrder = false;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                OrderInfo.IsOrder = false;
                this.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtReceivedAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalReceivedAmount.Text.Replace("$", "") != "")
                {
                    decimal change = Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "") == "" ? "0" : txtTotalAmt.Text.Replace("$", "")) - Functions.GetDecimal(txtTotalReceivedAmount.Text.Replace("$", "") == "" ? "0" : txtTotalReceivedAmount.Text.Replace("$", ""));
                    if (change < 0)
                    {
                        txtRemainingAmount.Text = "$0.00";
                    }
                    else
                    {
                        txtRemainingAmount.Text = Functions.GetDisplayAmount((change).ToString().Replace("$", ""));
                    }
                }
                else
                {
                    txtRemainingAmount.Text = "$0.00";
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        public void creditCardAmount(decimal Amount)
        {
            try
            {
                if (Amount != 0)
                {
                    OrderInfo.CreditAmt += Amount;
                    OrderInfo.Change = Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", ""));
                    totalReceivedAmt += Amount;
                    txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                    txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                    txtReceiveAmt.Text = "";
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        public void cashAmount(decimal Amount)
        {
            try
            {
                if (Amount != 0)
                {
                    OrderInfo.CashAmt += Amount;
                    OrderInfo.Change = Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", ""));
                    totalReceivedAmt += Amount;
                    txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                    txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                    txtReceiveAmt.Text = "";
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        public void CheckAmount(decimal Amount)
        {
            try
            {
                if (Amount != 0)
                {
                    OrderInfo.CheckAmt += Amount;
                    OrderInfo.Change = Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", ""));
                    totalReceivedAmt += Amount;
                    txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                    txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                    txtReceiveAmt.Text = "";
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmTotal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //PortOpen_Close(false);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtReceiveAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                receiveAmt = Regex.Replace(txtReceiveAmt.Text, "[^0-9.]", "");

                decimal change = ((Functions.GetDecimal(receiveAmt == "" ? "0" : receiveAmt) / 100) + totalReceivedAmt) - (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "") == "" ? "0" : txtTotalAmt.Text.Replace("$", "")));
                if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "") == "" ? "0" : txtTotalAmt.Text.Replace("$", "")) < 0)
                {
                    txtChange.Text = Functions.GetDisplayAmount((((Functions.GetDecimal(receiveAmt == "" ? "0" : receiveAmt) / 100) + totalReceivedAmt)
                                        + (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "") == "" ? "0" : txtTotalAmt.Text.Replace("$", "")))).ToString());
                }
                else
                {
                    if (change < 0)
                    {
                        txtChange.Text = "$0.00";
                    }
                    else
                    {
                        txtChange.Text = Functions.GetDisplayAmount(change.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                OrderInfo.IsOrder = false;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        public string DollarReplace(string text)
        {
            return text = text.Replace("$", "");
        }

        private void FrmTotal_Load(object sender, EventArgs e)
        {
            txtTotalAmt.Text = txtRemainingAmount.Text;
            txtTotal.Text = txtRemainingAmount.Text;
        }

        public void AddCreditPaymentList(decimal Amount)
        {
            PaymentTransMasterModel _PaymentTransMasterModel = new PaymentTransMasterModel();
            _PaymentTransMasterModel.CreditCardAmount = Amount;
            _PaymentTransMasterModel.PaymentMethodID = 2;
            //if (Amount == Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)))
            //{
            //    _PaymentTransMasterModel.Balance = 0;
            //}
            //else
            //{
            //    if (Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) < 0)
            //    {
            //        _PaymentTransMasterModel.Balance = 0;
            //    }
            //    else
            //    {
            //        _PaymentTransMasterModel.Balance = Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text));
            //    }
            //}
            MultiPaymentInfo.lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
        }
        public void AddCashPaymentList(decimal Amount)
        {
            PaymentTransMasterModel _PaymentTransMasterModel = new PaymentTransMasterModel();
            _PaymentTransMasterModel.CashAmount = Amount;
            _PaymentTransMasterModel.PaymentMethodID = 1;
            //_PaymentTransMasterModel.Balance = Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text));
            MultiPaymentInfo.lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
        }
        public void AddCheckPaymentList(decimal Amount)
        {
            PaymentTransMasterModel _PaymentTransMasterModel = new PaymentTransMasterModel();
            _PaymentTransMasterModel.CheckAmount = Amount;
            _PaymentTransMasterModel.PaymentMethodID = 3;
            //_PaymentTransMasterModel.Balance = Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text));
            MultiPaymentInfo.lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
        }
        public void UpdateTotal()
        {
            if (txtReceiveAmt.Text != CommonModelCont.EmptyString)
            {
                string result = "";
                if (txtReceiveAmt.Text.Length >= 3)
                {
                    result = txtReceiveAmt.Text.Substring(txtReceiveAmt.Text.Length - 2);
                }
                decimal ReceiveCashAmt = 0;
                decimal ReceiveCheckAmt = 0;
                decimal ReceiveCreditAmt = 0;

                #region CLEAR
                if (txtReceiveAmt.Text.Trim().ToLower().Contains("cl"))
                {
                    txtReceiveAmt.Text = "";
                }
                #endregion

                #region CREDIT CARD
                else if (txtReceiveAmt.Text.Trim().ToLower().Contains("ta"))
                {
                    if (txtReceiveAmt.Text.ToLower().Replace("t", "").Replace("a", "") == "")
                    {
                        if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                        {
                            if (Functions.GetDecimal(txtChange.Text.Replace("$", "")) == Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")))
                            {
                                if (txtRemainingAmount.Text.Replace("$", "") != "" && txtRemainingAmount.Text.Replace("$", "") != "0")
                                {
                                    ReceiveCreditAmt = Functions.GetDecimal(txtRemainingAmount.Text.Replace("$", ""));
                                    OrderInfo.CreditAmt += ReceiveCreditAmt;
                                    AddCreditPaymentList(ReceiveCreditAmt);
                                }
                                else
                                {
                                    ReceiveCreditAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""));
                                    OrderInfo.CreditAmt += ReceiveCreditAmt;
                                    AddCreditPaymentList(ReceiveCreditAmt);
                                    if (OrderInfo.CreditAmt < 0)
                                    {
                                        OrderInfo.CreditAmt = Functions.GetDecimal(OrderInfo.CreditAmt.ToString().Replace("-", ""));
                                    }
                                }
                                OrderInfo.Change = 0;
                                totalReceivedAmt += ReceiveCreditAmt;
                                txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                                txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                                txtReceiveAmt.Text = "";
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "Refund Amount must be equal to the Total Amount.", false);
                            }
                        }
                        else
                        {
                            if (txtRemainingAmount.Text.Replace("$", "") != "" && txtRemainingAmount.Text.Replace("$", "") != "0")
                            {
                                ReceiveCreditAmt = Functions.GetDecimal(txtRemainingAmount.Text.Replace("$", ""));
                                OrderInfo.CreditAmt += ReceiveCreditAmt;
                                AddCreditPaymentList(ReceiveCreditAmt);
                            }
                            else
                            {
                                ReceiveCreditAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""));
                                OrderInfo.CreditAmt += ReceiveCreditAmt;
                                AddCreditPaymentList(ReceiveCreditAmt);
                                if (OrderInfo.CreditAmt < 0)
                                {
                                    OrderInfo.CreditAmt = Functions.GetDecimal(OrderInfo.CreditAmt.ToString().Replace("-", ""));
                                }
                            }
                            OrderInfo.Change = 0;
                            totalReceivedAmt += ReceiveCreditAmt;
                            txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                            txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                            txtReceiveAmt.Text = "";
                        }

                    }

                    else if (txtReceiveAmt.Text.ToLower().Replace("t", "").Replace("a", "") != "")
                    {
                        ReceiveCreditAmt = Functions.GetDecimal(Regex.Replace(txtReceiveAmt.Text, "[^0-9.]", ""));
                        if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                        {
                            if (Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", "")) == 0)
                            {
                                if (ReceiveCreditAmt != 0)
                                {
                                    creditCardAmount(ReceiveCreditAmt / 100);
                                    AddCreditPaymentList(ReceiveCreditAmt / 100);
                                }
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "Refund Amount must be equal to the Total Amount.", false);
                                txtReceiveAmt.Text = "";
                            }
                        }
                        else
                        {
                            if (ReceiveCreditAmt != 0)
                            {
                                creditCardAmount(ReceiveCreditAmt / 100);
                                AddCreditPaymentList(ReceiveCreditAmt / 100);
                            }
                        }
                    }

                    if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    {
                        if (totalReceivedAmt < 0)
                        {
                            if ((totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                            {
                                txtTotalAmt.Text = "";
                                txtReceiveAmt.Text = "";
                                OrderInfo.PaymentType = 2;
                                totalReceivedAmt = 0;
                                receiveAmt = "";
                                OrderInfo.IsOrder = true;
                                this.Hide();
                            }
                        }
                        else if (-(totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                        {
                            txtTotalAmt.Text = "";
                            txtReceiveAmt.Text = "";
                            OrderInfo.PaymentType = 2;
                            totalReceivedAmt = 0;
                            receiveAmt = "";
                            OrderInfo.IsOrder = true;
                            this.Hide();
                        }
                    }
                    else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    {
                        txtTotalAmt.Text = "";
                        txtReceiveAmt.Text = "";
                        OrderInfo.PaymentType = 2;
                        totalReceivedAmt = 0;
                        receiveAmt = "";
                        OrderInfo.IsOrder = true;
                        this.Hide();
                    }
                }
                #endregion

                #region CASH
                else if (result.Trim().ToLower() == "ca")
                {
                    //ReceiveCashAmt = txtReceiveAmt.Text.ToLower().Replace("c", "").Replace("a", "") != "" ? Functions.GetDecimal(txtReceiveAmt.Text.ToLower().Replace("c", "").Replace("a", "")) : 0;
                    ReceiveCashAmt = Functions.GetDecimal(Regex.Replace(txtReceiveAmt.Text, "[^0-9.]", ""));
                    if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    {
                        if (Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", "")) == 0)
                        {
                            if (ReceiveCashAmt != 0)
                            {
                                cashAmount(ReceiveCashAmt / 100);
                                AddCashPaymentList(ReceiveCashAmt / 100);
                            }
                        }
                        else
                        {
                            txtReceiveAmt.Text = "";
                            ClsCommon.MsgBox("Information", "Refund Amount must be equal to the Total Amount.", false);

                        }
                    }
                    else
                    {
                        if (ReceiveCashAmt != 0)
                        {
                            cashAmount(ReceiveCashAmt / 100);
                            AddCashPaymentList(ReceiveCashAmt / 100);
                        }
                    }
                    if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    {
                        if ((-totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                        {
                            txtTotalAmt.Text = "";
                            txtReceiveAmt.Text = "";
                            OrderInfo.PaymentType = 1;
                            totalReceivedAmt = 0;
                            receiveAmt = "";
                            OrderInfo.IsOrder = true;
                            this.Hide();
                        }
                    }
                    else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    {
                        txtTotalAmt.Text = "";
                        txtReceiveAmt.Text = "";
                        OrderInfo.PaymentType = 1;
                        totalReceivedAmt = 0;
                        receiveAmt = "";
                        OrderInfo.IsOrder = true;
                        this.Hide();
                    }
                }
                #endregion

                #region CHECK
                else if (result.Trim().ToLower() == "ck")
                {
                    //ReceiveCheckAmt = txtReceiveAmt.Text.ToLower().Replace("c", "").Replace("k", "") != "" ? Functions.GetDecimal(txtReceiveAmt.Text.ToLower().Replace("c", "").Replace("k", "")) : 0;
                    ReceiveCheckAmt = Functions.GetDecimal(Regex.Replace(txtReceiveAmt.Text, "[^0-9.]", ""));
                    if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    {
                        if (Functions.GetDecimal(txtChange.Text.Replace("$", "") == "" ? "0" : txtChange.Text.Replace("$", "")) == 0)
                        {
                            if (ReceiveCheckAmt != 0)
                            {
                                CheckAmount(ReceiveCheckAmt / 100);
                                AddCheckPaymentList(ReceiveCheckAmt / 100);
                            }
                        }
                        else
                        {
                            txtReceiveAmt.Text = "";
                            ClsCommon.MsgBox("Information", "Refund Amount must be equal to the Total Amount.", false);
                        }
                    }
                    else
                    {
                        if (ReceiveCheckAmt != 0)
                        {
                            CheckAmount(ReceiveCheckAmt / 100);
                            AddCheckPaymentList(ReceiveCheckAmt / 100);
                        }
                    }
                    if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    {
                        if ((-totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                        {
                            txtTotalAmt.Text = "";
                            txtReceiveAmt.Text = "";
                            OrderInfo.PaymentType = 3;
                            totalReceivedAmt = 0;
                            receiveAmt = "";
                            OrderInfo.IsOrder = true;
                            this.Hide();
                        }
                    }
                    else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    {
                        txtTotalAmt.Text = "";
                        txtReceiveAmt.Text = "";
                        OrderInfo.PaymentType = 3;
                        totalReceivedAmt = 0;
                        receiveAmt = "";
                        OrderInfo.IsOrder = true;
                        this.Hide();
                    }
                }
                #endregion

                #region COUPON
                else if (txtReceiveAmt.Text.Trim().ToLower() == "cc")
                {
                    if (CouponInfo.isCoupon)
                    {
                        ClsCommon.MsgBox("Information", "Can't apply multiple coupon om single order.!", false);
                        txtReceiveAmt.Text = "";
                        txtReceiveAmt.Focus();
                        return;
                    }

                    UCCheckCoupon _UCCheckCoupon = new UCCheckCoupon();
                    _UCCheckCoupon.MinPurchaseAmount = Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text));
                    _UCCheckCoupon.Location = new Point(this.Width / 2 - _UCCheckCoupon.Size.Width / 2, this.Height / 2 - _UCCheckCoupon.Size.Height / 2);
                    _UCCheckCoupon.Show();

                    if (CouponInfo.isCoupon)
                    {
                        this.Size = new System.Drawing.Size(457, 427);
                        pnlCoupon.Visible = true;
                        pnlCoupon.Location = new System.Drawing.Point(33, 43);
                        pnlBalance.Location = new System.Drawing.Point(33, 124);
                        txtReceiveAmt.Location = new System.Drawing.Point(33, 233);
                        lblReceive.Location = new System.Drawing.Point(50, 203);
                        pnlChange.Location = new System.Drawing.Point(33, 304);
                        lblTotalReceivedAmt.Location = new System.Drawing.Point(47, 380);
                        txtTotalReceivedAmount.Location = new System.Drawing.Point(275, 380);

                        txtCoupon.Text = CouponInfo.CouponCode;
                        txtDiscount.Text = "(" + CouponInfo.Discount.ToString() + "%)";
                        txtDiscountAmount.Text = "$" + ((Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) * CouponInfo.Discount) / 100).ToString("0.00");
                        CouponInfo.DiscAmt = Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text));
                        txtRemainingAmount.Text = "$ " + (Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) - Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text)));
                        txtTotalAmt.Text = txtRemainingAmount.Text;
                    }
                    txtReceiveAmt.Text = "";
                    txtReceiveAmt.Focus();
                }
                #endregion

                #region REMOVE COUPON
                else if (txtReceiveAmt.Text.Trim().ToLower() == "rcc")
                {
                    pnlCoupon.Visible = false;
                    pnlBalance.Location = new System.Drawing.Point(33, 43);
                    txtReceiveAmt.Location = new System.Drawing.Point(33, 172);
                    lblReceive.Location = new System.Drawing.Point(50, 142);
                    pnlChange.Location = new System.Drawing.Point(33, 259);
                    lblTotalReceivedAmt.Location = new System.Drawing.Point(50, 353);
                    txtTotalReceivedAmount.Location = new System.Drawing.Point(281, 353);

                    CouponInfo.isCoupon = false;
                    txtRemainingAmount.Text = "$ " + (Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) + Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text)));
                    txtDiscountAmount.Text = "$ 0.00";
                    txtTotalAmt.Text = txtRemainingAmount.Text;
                    txtReceiveAmt.Text = "";
                    txtReceiveAmt.Focus();
                }
                #endregion

                #region CLOSE
                else if (txtReceiveAmt.Text.Trim().ToLower() == "cn")
                {
                    this.Hide();
                }
                #endregion
            }
        }
    }
}

