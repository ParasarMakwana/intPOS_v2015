using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.BAL.MasterDataServices;
using System.Text.RegularExpressions;
using SFPOSWindows.Frontend;
using SFPOS.DAL;
using SFPOS.BAL.Frontend;
using System.Data.Entity;
using System.IO;
using System.Xml.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Data.SQLite;

namespace SFPOSWindows.CustomControl
{

    public partial class UCTotal : UserControl
    {
        #region Properties
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        decimal totalReceivedAmt = 0;
        string receiveAmt = "";
        CustomerAppliedCouponService _customerAppliedService = new CustomerAppliedCouponService();
        ErrorProvider ep = new ErrorProvider();
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        #endregion

        #region Events
        private void txtReceiveAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    //UpdateTotal();                    
                }
            }
            catch (Exception ex)
            {
                OrderInfo.IsOrder = false;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UCTotal_Load(object sender, EventArgs e)
        {
            txtTotalAmt.Text = txtRemainingAmount.Text;
            txtTotal.Text = txtRemainingAmount.Text;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                OrderInfo.IsOrder = false;
                this.Hide();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtTotalReceivedAmount_TextChanged(object sender, EventArgs e)
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public UCTotal()
        {
            InitializeComponent();
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

                                    if (ReceiveCreditAmt < 0 && Functions.GetDecimal(txtChange.Text.Replace("$", "")) < 0 && Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                                    {
                                        ReceiveCreditAmt = System.Math.Abs(ReceiveCreditAmt);
                                    }

                                    if (XMLData.EPXPaymenrServiceON)
                                    {
                                        var trans = EPXTransaction((ReceiveCreditAmt / 100).ToString(), (CardRefundType.CardType == "Credit" ? "CCR9" : "DB01"));
                                        if (trans == "Successful")
                                        {
                                            OrderInfo.CreditAmt += ReceiveCreditAmt;
                                            AddCreditPaymentList(ReceiveCreditAmt);
                                        }
                                        else
                                        {
                                            txtReceiveAmt.Text = "";
                                            ClsCommon.MsgBox("Information", "Transaction Not Successful", false);
                                        }
                                    }
                                    else
                                    {
                                        OrderInfo.CreditAmt += ReceiveCreditAmt;
                                        AddCreditPaymentList(ReceiveCreditAmt);
                                    }
                                }
                                else
                                {
                                    ReceiveCreditAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""));
                                    if (ReceiveCreditAmt < 0 && Functions.GetDecimal(txtChange.Text.Replace("$", "")) < 0 && Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                                    {
                                        ReceiveCreditAmt = System.Math.Abs(ReceiveCreditAmt);
                                    }

                                    if (XMLData.EPXPaymenrServiceON)
                                    {
                                        var trans = EPXTransaction((ReceiveCreditAmt / 100).ToString(), (CardRefundType.CardType == "Credit" ? "CCR9" : "DB01"));
                                        if (trans == "Successful")
                                        {
                                            OrderInfo.CreditAmt += ReceiveCreditAmt;
                                            AddCreditPaymentList(ReceiveCreditAmt);
                                        }
                                        else
                                        {
                                            txtReceiveAmt.Text = "";
                                            ClsCommon.MsgBox("Information", "Transaction Not Successful", false);
                                        }
                                    }
                                    else
                                    {
                                        OrderInfo.CreditAmt += ReceiveCreditAmt;
                                        AddCreditPaymentList(ReceiveCreditAmt);
                                    }
                                    //AddCreditPaymentList(ReceiveCreditAmt);
                                    if (OrderInfo.CreditAmt < 0)
                                    {
                                        OrderInfo.CreditAmt = Functions.GetDecimal(OrderInfo.CreditAmt.ToString().Replace("-", ""));
                                    }
                                }
                                OrderInfo.Change = 0;
                                totalReceivedAmt += ReceiveCreditAmt;
                                txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                                txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                                OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                                OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
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

                                if (XMLData.EPXPaymenrServiceON)
                                {
                                    var trans = EPXTransaction((ReceiveCreditAmt / 100).ToString(), (CardRefundType.CardType == "Credit" ? "CCR9" : "DB01"));
                                    if (trans == "Successful")
                                    {
                                        OrderInfo.CreditAmt += ReceiveCreditAmt;
                                        AddCreditPaymentList(ReceiveCreditAmt);
                                    }
                                    else
                                    {
                                        txtReceiveAmt.Text = "";
                                        ClsCommon.MsgBox("Information", "Transaction Not Successful", false);
                                    }
                                }
                                else
                                {
                                    OrderInfo.CreditAmt += ReceiveCreditAmt;
                                    AddCreditPaymentList(ReceiveCreditAmt);
                                }
                            }
                            else
                            {
                                ReceiveCreditAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""));

                                if (XMLData.EPXPaymenrServiceON)
                                {
                                    var trans = EPXTransaction((ReceiveCreditAmt / 100).ToString(), "SALE");
                                    if (trans == "Successful")
                                    {
                                        OrderInfo.CreditAmt += ReceiveCreditAmt;
                                        AddCreditPaymentList(ReceiveCreditAmt);
                                    }
                                    else
                                    {
                                        txtReceiveAmt.Text = "";
                                        ClsCommon.MsgBox("Information", "Transaction Not Successful", false);
                                    }
                                }
                                else
                                {
                                    OrderInfo.CreditAmt += ReceiveCreditAmt;
                                    AddCreditPaymentList(ReceiveCreditAmt);
                                }
                                if (OrderInfo.CreditAmt < 0)
                                {
                                    OrderInfo.CreditAmt = Functions.GetDecimal(OrderInfo.CreditAmt.ToString().Replace("-", ""));
                                }
                            }
                            OrderInfo.Change = 0;
                            totalReceivedAmt += ReceiveCreditAmt;
                            txtTotalReceivedAmount.Text = Functions.GetDisplayAmount(totalReceivedAmt.ToString());
                            txtRemainingAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) - totalReceivedAmt).ToString());
                            OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));

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

                                    if (XMLData.EPXPaymenrServiceON)
                                    {
                                        var trans = EPXTransaction((ReceiveCreditAmt / 100).ToString(), (CardRefundType.CardType == "Credit" ? "CCR9" : "DB01"));
                                        if (trans == "Successful")
                                        {
                                            creditCardAmount(ReceiveCreditAmt / 100);
                                            AddCreditPaymentList(ReceiveCreditAmt / 100);
                                        }
                                        else
                                        {
                                            txtReceiveAmt.Text = "";
                                            ClsCommon.MsgBox("Information", "Transaction Not Successful", false);
                                        }
                                    }
                                    else
                                    {
                                        creditCardAmount(ReceiveCreditAmt / 100);
                                        AddCreditPaymentList(ReceiveCreditAmt / 100);
                                    }
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

                                if (XMLData.EPXPaymenrServiceON)
                                {
                                    var trans = EPXTransaction((ReceiveCreditAmt / 100).ToString(), "SALE");
                                    if (trans == "Successful")
                                    {
                                        creditCardAmount(ReceiveCreditAmt / 100);
                                        AddCreditPaymentList(ReceiveCreditAmt / 100);
                                    }
                                    else
                                    {
                                        txtReceiveAmt.Text = "";
                                        ClsCommon.MsgBox("Information", "Transaction Not Successful", false);
                                    }
                                }
                                else
                                {
                                    creditCardAmount(ReceiveCreditAmt / 100);
                                    AddCreditPaymentList(ReceiveCreditAmt / 100);
                                }
                            }
                        }

                    }

                    if (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", "")) < 0)
                    {
                        if (totalReceivedAmt < 0)
                        {
                            if ((totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                            {
                                OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                                txtTotalAmt.Text = "";
                                txtReceiveAmt.Text = "";
                                OrderInfo.PaymentType = 2;
                                totalReceivedAmt = 0;
                                receiveAmt = "";
                                OrderInfo.IsOrder = true;
                                this.Hide();
                                //OnMyEvent(this, new EventArgs());
                            }
                        }
                        else if ((-totalReceivedAmt) == (Functions.GetDecimal(txtTotalAmt.Text.Replace("$", ""))))
                        {
                            OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                            txtTotalAmt.Text = "";
                            txtReceiveAmt.Text = "";
                            OrderInfo.PaymentType = 2;
                            totalReceivedAmt = 0;
                            receiveAmt = "";
                            OrderInfo.IsOrder = true;
                            this.Hide();
                            //OnMyEvent(this, new EventArgs());
                        }
                    }
                    else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    {
                        OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                        txtTotalAmt.Text = "";
                        txtReceiveAmt.Text = "";
                        OrderInfo.PaymentType = 2;
                        totalReceivedAmt = 0;
                        receiveAmt = "";
                        OrderInfo.IsOrder = true;
                        this.Hide();
                        //OnMyEvent(this, new EventArgs());
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
                            OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                            txtTotalAmt.Text = "";
                            txtReceiveAmt.Text = "";
                            OrderInfo.PaymentType = 1;
                            totalReceivedAmt = 0;
                            receiveAmt = "";
                            OrderInfo.IsOrder = true;
                            this.Hide();

                            //OnMyEvent(this, new EventArgs());
                        }
                    }
                    else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    {
                        OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                        txtTotalAmt.Text = "";
                        txtReceiveAmt.Text = "";
                        OrderInfo.PaymentType = 1;
                        totalReceivedAmt = 0;
                        receiveAmt = "";
                        OrderInfo.IsOrder = true;
                        this.Hide();

                        //OnMyEvent(this, new EventArgs());
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
                            OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                            txtTotalAmt.Text = "";
                            txtReceiveAmt.Text = "";
                            OrderInfo.PaymentType = 3;
                            totalReceivedAmt = 0;
                            receiveAmt = "";
                            OrderInfo.IsOrder = true;
                            this.Hide();
                            //OnMyEvent(this, new EventArgs());
                        }
                    }
                    else if (totalReceivedAmt >= (Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""))))
                    {
                        OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                        txtTotalAmt.Text = "";
                        txtReceiveAmt.Text = "";
                        OrderInfo.PaymentType = 3;
                        totalReceivedAmt = 0;
                        receiveAmt = "";
                        OrderInfo.IsOrder = true;
                        this.Hide();
                        //OnMyEvent(this, new EventArgs());
                    }

                }
                #endregion

                #region CLOSE
                else if (txtReceiveAmt.Text.Trim().ToLower() == "cn")
                {
                    this.Hide();
                    OnMyEvent(this, new EventArgs());
                }
                #endregion
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
                    OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                    txtReceiveAmt.Text = "";
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
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
                    OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                    txtReceiveAmt.Text = "";
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
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
                    OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                    txtReceiveAmt.Text = "";
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmTotal + ex.StackTrace, ex.LineNumber());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                this.Hide();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public string DollarReplace(string text)
        {
            return text = text.Replace("$", "");
        }

        public void AddCreditPaymentList(decimal Amount)
        {
            PaymentTransMasterModel _PaymentTransMasterModel = new PaymentTransMasterModel();
            _PaymentTransMasterModel.CreditCardAmount = Amount;
            _PaymentTransMasterModel.PaymentMethodID = 2;

            MultiPaymentInfo.lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
            if(OrderInfo.IsPaymentResume == false)
            {
                PaymentSaveLocally();
            }
        }

        public void AddCashPaymentList(decimal Amount)
        {
            PaymentTransMasterModel _PaymentTransMasterModel = new PaymentTransMasterModel();
            _PaymentTransMasterModel.CashAmount = Amount;
            _PaymentTransMasterModel.PaymentMethodID = 1;
            MultiPaymentInfo.lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
            if (OrderInfo.IsPaymentResume == false)
            {
                PaymentSaveLocally();
            }
        }

        public void AddCheckPaymentList(decimal Amount)
        {
            PaymentTransMasterModel _PaymentTransMasterModel = new PaymentTransMasterModel();
            _PaymentTransMasterModel.CheckAmount = Amount;
            _PaymentTransMasterModel.PaymentMethodID = 3;
            MultiPaymentInfo.lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
            if (OrderInfo.IsPaymentResume == false)
            {
                PaymentSaveLocally();
            }
        }

        public void CheckCouponCodeIsApplied()
        {

            if (CouponInfo.isCoupon)
            {
                this.Size = new System.Drawing.Size(457, 550);
                pnlCoupon.Visible = true;
                pnlCoupon.Location = new System.Drawing.Point(33, 43);
                pnlBalance.Location = new System.Drawing.Point(33, 124);
                txtReceiveAmt.Location = new System.Drawing.Point(33, 233);
                lblReceive.Location = new System.Drawing.Point(50, 203);
                pnlChange.Location = new System.Drawing.Point(33, 304);
                lblTotalReceivedAmt.Location = new System.Drawing.Point(47, 380);
                txtTotalReceivedAmount.Location = new System.Drawing.Point(275, 380);
                btnCash.Location = new Point(33, 410);
                btnRemoveCoupon.Location = new Point(33, 470);
                btnCredit.Location = new Point(245, 410);
                btnCheck.Location = new Point(245, 470);
                btnRemoveCoupon.IsAccessible = false;

                txtCoupon.Text = CouponInfo.CouponCode;
                txtDiscount.Text = "(" + CouponInfo.Discount.ToString() + "%)";
                txtDiscountAmount.Text = "$" + (CouponInfo.DiscAmt).ToString("0.00");
                //CouponInfo.DiscAmt = Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text));
                txtTotal.Text = txtRemainingAmount.Text; //"$"+ (Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text))+ Functions.GetDecimal(CouponInfo.DiscAmt.ToString("0.00")));
                //txtChange.Text = txtRemainingAmount.Text;
                txtTotalAmt.Text = txtRemainingAmount.Text;
            }
            else
            {
                pnlCoupon.Visible = false;
                pnlBalance.Location = new System.Drawing.Point(24, 43);
                txtReceiveAmt.Location = new System.Drawing.Point(24, 172);
                lblReceive.Location = new System.Drawing.Point(44, 142);
                pnlChange.Location = new System.Drawing.Point(24, 259);
                lblTotalReceivedAmt.Location = new System.Drawing.Point(44, 353);
                txtTotalReceivedAmount.Location = new System.Drawing.Point(275, 353);
                //decimal taxamount = OrderInfo.TaxAmount - CouponInfo.TaxAmount;
                txtRemainingAmount.Text = "$" + (Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) + Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text)));
                //txtRemainingAmount.Text = "$" + (Functions.GetDecimal(DollarReplace(txtRemainingAmount.Text)) + Functions.GetDecimal(DollarReplace(txtDiscountAmount.Text)) + CouponInfo.TaxAmount - CouponInfo.CouponTaxAmount);
                txtDiscountAmount.Text = "$ 0.00";
                //CouponInfo.TaxAmount = 0;
                OnMyEvent(this, new EventArgs());
                txtTotalAmt.Text = txtRemainingAmount.Text;
                //FrmOrderScanner_P8 _FrmOrderScanner_P8 = new FrmOrderScanner_P8();
                //_FrmOrderScanner_P8.lblFinalAmount
                //FrmOrderScanner_P8.lblFinalAmount.Text = txtRemainingAmount.Text;
                //txtChange.Text = txtRemainingAmount.Text;
                OrderInfo.RecivedAmt = Functions.GetDecimal(txtTotalAmt.Text.Replace("-", "").Replace("$", ""));
                txtReceiveAmt.Text = "";
                CouponInfo.CouponCode = "";
                CouponInfo.DiscAmt = 0;
                txtReceiveAmt.Focus();
            }
        }

        #endregion

        private void btnCash_Click(object sender, EventArgs e)
        {
            txtReceiveAmt.Text += "CA";
            UpdateTotal();
            OnMyEvent(this, new EventArgs());
        }


        private void btnCredit_Click(object sender, EventArgs e)
        {
            txtReceiveAmt.Text += "TA";
            CardRefundType.CardType = "Credit";
            UpdateTotal();
            OnMyEvent(this, new EventArgs());
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            txtReceiveAmt.Text += "CK";
            UpdateTotal();
            OnMyEvent(this, new EventArgs());
        }

        private void btnRemoveCoupon_Click(object sender, EventArgs e)
        {

            #region Old code for remove coupon
            //if (CouponInfo.isCoupon)
            //{
            //    CouponInfo.isCoupon = false;
            //    CustomerInfo.IsCustomerVerfied = false;
            //    CheckCouponCodeIsApplied();
            //}
            #endregion

            ClsCommon.MsgBox("Information", "You can't remove the coupon Once it's applied, You have to cancle the transaction for remove the coupon.", false);

        }
        public string EPXTransaction(string totalReceivedAmt, string type)
        {
            if (XMLData.EPXPaymenrServiceON)
            {
                Random generator = new Random();
                String transNo = generator.Next(0, 1000000).ToString("D6");
                string resptext = ClsCommon.EPXSubmitCreditAndFoodStamp(totalReceivedAmt, type, transNo);
                string saaa = "";
                string HTTPHeaderDelimiter = "\r\n\r\n";
                LoginInfo.transcationNo += transNo.ToString() + ",";
                if (resptext.IndexOf("HTTP/1.1 200 OK") > -1)
                {
                    saaa = resptext.Substring(resptext.IndexOf(HTTPHeaderDelimiter) + HTTPHeaderDelimiter.Length).Trim();


                    var xml = XDocument.Parse(saaa);
                    var element = xml.GetElementByName("RESPONSE", "AUTH_RESP");

                    if (element != null)
                    {
                        if (!string.IsNullOrEmpty(element.Value) && element.Value.Trim() == "00")
                        {
                            tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                            var TVR = xml.GetElementByName("RESPONSE", "SI_EMV_TVR");
                            var CardType = xml.GetElementByName("RESPONSE", "SI_EMV_APP_LABEL");
                            var EmvAid = xml.GetElementByName("RESPONSE", "SI_EMV_AID");
                            var TSI = xml.GetElementByName("RESPONSE", "SI_EMV_TSI");
                            epx = _db.tbl_OrderEPXLog.AsNoTracking().Where(p => p.TransactionNo == transNo).FirstOrDefault();

                            epx.Response = saaa;

                            epx.ResponseValue = element.Value;
                            epx.TVR = TVR.Value.ToString();
                            epx.CardType = CardType.Value.ToString();
                            epx.EmvAid = EmvAid.Value.ToString();
                            epx.TSI = TSI.Value.ToString();
                            epx.PaymentMethodId = 2;
                            _db.Entry(epx).State = EntityState.Modified;
                            _db.SaveChanges();
                            return "Successful";
                            //MessageBox.Show("Successful Transaction");
                        }
                        else
                        {
                            var element2 = xml.GetElementByName("RESPONSE", "AUTH_RESP_TEXT");
                            if (element2 != null)
                            {
                                tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                                epx = _db.tbl_OrderEPXLog.AsNoTracking().Where(p => p.TransactionNo == transNo).FirstOrDefault();

                                epx.Response = saaa;
                                epx.PaymentMethodId = 2;
                                _db.Entry(epx).State = EntityState.Modified;
                                _db.SaveChanges();

                                //MessageBox.Show(element.Value + " - " + element2.Value);
                            }
                            return "Not Successful";
                        }
                    }
                    else
                    {
                        var element2 = xml.GetElementByName("RESPONSE", "AUTH_RESP_TEXT");
                        if (element2 != null)
                        {
                            tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                            epx = _db.tbl_OrderEPXLog.AsNoTracking().Where(p => p.TransactionNo == transNo).FirstOrDefault();

                            epx.Response = saaa;
                            epx.PaymentMethodId = 2;
                            _db.Entry(epx).State = EntityState.Modified;
                            _db.SaveChanges();

                            //MessageBox.Show(element.Value + " - " + element2.Value);
                        }
                        return "Not Successful";
                    }

                }
                else
                {
                    saaa = resptext.Substring(resptext.IndexOf(HTTPHeaderDelimiter) + HTTPHeaderDelimiter.Length).Trim();
                    //var xml = XDocument.Parse(saaa);
                    tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                    epx.UserId = Convert.ToInt32(LoginInfo.UserId);
                    epx.StoreID = Convert.ToInt32(LoginInfo.StoreID);
                    epx.Response = saaa.ToString();
                    epx.Amount = Convert.ToDecimal(totalReceivedAmt);
                    epx.TransactionNo = transNo;
                    epx.PaymentMethodId = 2;
                    _db.tbl_OrderEPXLog.Add(epx);
                    _db.SaveChanges();
                    return "error";
                }

            }
            else
            {
                return "Not Allowed";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtReceiveAmt.Text += "TA";
            CardRefundType.CardType = "Debit";
            UpdateTotal();
            OnMyEvent(this, new EventArgs());
        }

        public void PaymentSaveLocally()
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection(ClsCommon.SQLiteConn);
                conn.Open();

                var PaymentData = MultiPaymentInfo.lstPaymentTransMasterModel.Last();
                var cmdInsert = "Insert into tbl_PaymentTrans values(" + LoginInfo.UserId + ","
                    + PaymentData.PaymentMethodID + ","
                    + (PaymentData.CashAmount == null ? "NULL," : PaymentData.CashAmount.ToString() + ",")
                    + (PaymentData.CreditCardAmount == null ? "NULL," : PaymentData.CreditCardAmount.ToString() + ",")
                    + (PaymentData.CheckAmount == null ? "NULL," : PaymentData.CheckAmount.ToString() + ",")
                    + (PaymentData.FoodStampAmount == null ? "NULL" : PaymentData.FoodStampAmount.ToString()) + ")";

                var LiteCmd = new SQLiteCommand(cmdInsert, conn);
                LiteCmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }
    }

    public static class ProjectExtensions
    {
        // retrieve a single element by tag name
        public static XElement GetElementByName(this XDocument xmlDocument, string parentElementName, string elementName)
        {
            var element = xmlDocument.Descendants(parentElementName).Elements().Where(x => x.Name == elementName).FirstOrDefault();
            return element != null ? element : null;
        }

        // retrieve attribute by name
        public static string GetAttributeValueByName(this XElement item, string attributeName)
        {
            var attribute = item.Attribute(attributeName);
            return attribute != null ? attribute.Value : string.Empty;
        }

        public static byte[] ReceiveAll(this Socket socket)
        {
            var buffer = new List<byte>();

            while (socket.Available > 0)
            {
                var currByte = new Byte[1];
                var byteCounter = socket.Receive(currByte, currByte.Length, SocketFlags.None);

                if (byteCounter.Equals(1))
                {
                    buffer.Add(currByte[0]);
                }
            }

            return buffer.ToArray();
        }
    }
}
