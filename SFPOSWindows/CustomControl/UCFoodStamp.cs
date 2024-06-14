using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SFPOSWindows.CustomControl
{
    public partial class UCFoodStamp : UserControl
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public decimal FSEligibleAmt = 0;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                OrderInfo.CancelFS = true;
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public UCFoodStamp()
        {
            InitializeComponent();
        }
        public void UpdateFoodStamp()
        {
            try
            {
                txtFSEligible.Text = txtFSEligible.Text.Replace(CommonModelCont.AddDollorSign, string.Empty);
                if (txtFSAmount.Text != "")
                {
                    if (txtFSAmount.Text.Trim().ToLower().EndsWith("cl"))
                    {
                        txtFSAmount.Text = "";
                        txtFSAmount.Focus();
                    }

                    else if (txtFSAmount.Text.Trim().ToLower().Contains("fs"))
                    {
                        string fsAmt = (txtFSAmount.Text.ToLower().Replace("fs", ""));
                        if (fsAmt == "")
                        {
                            OrderInfo.FSTotal += Functions.GetDecimal(txtFSEligible.Text);
                            if (XMLData.EPXPaymenrServiceON)
                            {
                                var trans = EPXTransaction(OrderInfo.FSTotal.ToString(), "EB00");
                                if (trans != "error")
                                {
                                    AddPaymentList(Functions.GetDecimal(txtFSEligible.Text));
                                }
                            }
                            else
                            {
                                AddPaymentList(Functions.GetDecimal(txtFSEligible.Text));
                            }

                            this.Hide();
                        }
                        #region Refund By FoodStamp
                        else if ((Functions.GetDecimal(txtFSEligible.Text)) < 0)
                        {
                            if (-(Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) < (Functions.GetDecimal(txtFSEligible.Text)))
                            {
                                ClsCommon.MsgBox("Information", "Amount should be less than or equal to Foodstamp eligible amount.", false);
                                txtFSAmount.Text = "";
                                txtFSAmount.Focus();
                            }
                            else if (-(Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) == (Functions.GetDecimal(txtFSEligible.Text)))
                            {
                                OrderInfo.FSTotal += -Functions.GetDecimal(fsAmt) / 100;
                                if (XMLData.EPXPaymenrServiceON)
                                {
                                    var trans = EPXTransaction(OrderInfo.FSTotal.ToString(), "EB01");
                                    if (trans != "error")
                                    {
                                        AddPaymentList(-Functions.GetDecimal(fsAmt) / 100);
                                    }
                                }
                                else
                                {
                                    AddPaymentList(-Functions.GetDecimal(fsAmt) / 100);
                                }

                                this.Hide();
                            }
                            else if (-(Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) > (Functions.GetDecimal(txtFSEligible.Text)))
                            {
                                OrderInfo.FSTotal += -Functions.GetDecimal(fsAmt) / 100;

                                if (XMLData.EPXPaymenrServiceON)
                                {
                                    var trans = EPXTransaction(OrderInfo.FSTotal.ToString(), "EB01");
                                    if (trans != "error")
                                    {
                                        OrderInfo.remainingFSAmt = (-Functions.GetDecimal(txtFSEligible.Text) - (-Functions.GetDecimal(fsAmt) / 100));
                                        AddPaymentList(-Functions.GetDecimal(fsAmt) / 100);
                                    }
                                }
                                else
                                {
                                    OrderInfo.remainingFSAmt = (-Functions.GetDecimal(txtFSEligible.Text) - (-Functions.GetDecimal(fsAmt) / 100));
                                    AddPaymentList(-Functions.GetDecimal(fsAmt) / 100);
                                }
                                this.Hide();
                            }
                        }
                        #endregion
                        else if ((Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) > (Functions.GetDecimal(txtFSEligible.Text)))
                        {
                            ClsCommon.MsgBox("Information", "Amount should be less than or equal to Foodstamp eligible amount.", false);
                            txtFSAmount.Text = "";
                            txtFSAmount.Focus();
                        }
                        else if ((Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) == (Functions.GetDecimal(txtFSEligible.Text)))
                        {
                            OrderInfo.FSTotal += Functions.GetDecimal(fsAmt) / 100;
                            if (XMLData.EPXPaymenrServiceON)
                            {
                                var trans = EPXTransaction(OrderInfo.FSTotal.ToString(), "EB00");
                                if (trans != "error")
                                {
                                    AddPaymentList(Functions.GetDecimal(fsAmt) / 100);
                                }
                            }
                            else
                            {
                                AddPaymentList(Functions.GetDecimal(fsAmt) / 100);
                            }
                            this.Hide();
                        }
                        else if ((Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) < (Functions.GetDecimal(txtFSEligible.Text)))
                        {
                            OrderInfo.FSTotal += Functions.GetDecimal(fsAmt) / 100;

                            if (XMLData.EPXPaymenrServiceON)
                            {
                                var trans = EPXTransaction(OrderInfo.FSTotal.ToString(), "EB00");
                                if (trans != "error")
                                {
                                    OrderInfo.remainingFSAmt = (Functions.GetDecimal(txtFSEligible.Text) - (Functions.GetDecimal(fsAmt) / 100));
                                    AddPaymentList(Functions.GetDecimal(fsAmt) / 100);
                                }
                            }
                            else
                            {
                                OrderInfo.remainingFSAmt = (Functions.GetDecimal(txtFSEligible.Text) - (Functions.GetDecimal(fsAmt) / 100));
                                AddPaymentList(Functions.GetDecimal(fsAmt) / 100);
                            }
                            this.Hide();
                        }
                    }

                    else if (txtFSAmount.Text.Trim().ToLower().EndsWith("cn"))
                    {
                        this.Hide();
                        OnMyEvent(this, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.", false);
                txtFSAmount.Text = "";
                txtFSAmount.Focus();
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }

        public void AddPaymentList(decimal Amount)
        {
            PaymentTransMasterModel _PaymentTransMasterModel = new PaymentTransMasterModel();
            if (OrderInfo.IsRefundOrder)
                _PaymentTransMasterModel.FoodStampAmount = System.Math.Abs(Amount);
            else
                _PaymentTransMasterModel.FoodStampAmount = Amount;
            _PaymentTransMasterModel.PaymentMethodID = 4;
            //_PaymentTransMasterModel.Balance = (Functions.GetDecimal(txtFSEligible.Text)) - (Functions.GetDecimal(txtFSAmount.Text.ToLower().Replace("fs", "")) / 100);
            MultiPaymentInfo.lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
            if (OrderInfo.IsPaymentResume == false)
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {
                    this.Hide();
                    OnMyEvent(this, new EventArgs());
                    return true;

                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void txtFSAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    UpdateFoodStamp();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnFoodstamp_Click(object sender, EventArgs e)
        {
            txtFSAmount.Text += "FS";
            UpdateFoodStamp();
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        public string EPXTransaction(string totalReceivedAmt, string type)
        {
            if (XMLData.EPXPaymenrServiceON)
            {
                Random generator = new Random();
                String transNo = generator.Next(0, 1000000).ToString("D6");
                string resptext = ClsCommon.EPXSubmitCreditAndFoodStamp(totalReceivedAmt, type, transNo);
                string RespMsg = "";
                string HTTPHeaderDelimiter = "\r\n\r\n";
                LoginInfo.transcationNo += transNo.ToString() + ",";
                if (resptext.IndexOf("HTTP/1.1 200 OK") > -1)
                {
                    RespMsg = resptext.Substring(resptext.IndexOf(HTTPHeaderDelimiter) + HTTPHeaderDelimiter.Length).Trim();


                    var xml = XDocument.Parse(RespMsg);
                    var element = xml.GetElementByName("RESPONSE", "AUTH_RESP");

                    if (element != null)
                    {
                        if (!string.IsNullOrEmpty(element.Value) && element.Value.Trim() == "00")
                        {
                            tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                            epx = _db.tbl_OrderEPXLog.AsNoTracking().Where(p => p.TransactionNo == transNo).FirstOrDefault();

                            if (RespMsg.Contains("SI_EMV_TVR"))
                            {
                                var TVR = xml.GetElementByName("RESPONSE", "SI_EMV_TVR");
                                epx.TVR = TVR.Value.ToString();
                            }
                            if (RespMsg.Contains("SI_EMV_APP_LABEL"))
                            {
                                var CardType = xml.GetElementByName("RESPONSE", "SI_EMV_APP_LABEL");
                                epx.CardType = CardType.Value.ToString();
                            }
                            if (RespMsg.Contains("SI_EMV_AID"))
                            {
                                var EmvAid = xml.GetElementByName("RESPONSE", "SI_EMV_AID");
                                epx.EmvAid = EmvAid.Value.ToString();
                            }
                            if (RespMsg.Contains("SI_EMV_TSI"))
                            {
                                var TSI = xml.GetElementByName("RESPONSE", "SI_EMV_TSI");
                                epx.TSI = TSI.Value.ToString();
                            }

                            
                            epx.ResponseValue = element.Value;
                            epx.Response = RespMsg;
                            epx.PaymentMethodId = 4;
                            _db.Entry(epx).State = EntityState.Modified;
                            _db.SaveChanges();
                            CardRefundType.CardType = "Successful";
                            return "Successful";
                        }
                        else
                        {
                            var element2 = xml.GetElementByName("RESPONSE", "AUTH_RESP_TEXT");
                            if (element2 != null)
                            {
                                tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                                epx = _db.tbl_OrderEPXLog.AsNoTracking().Where(p => p.TransactionNo == transNo).FirstOrDefault();

                                epx.Response = RespMsg;
                                epx.PaymentMethodId = 4;
                                _db.Entry(epx).State = EntityState.Modified;
                                _db.SaveChanges();

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

                            epx.Response = RespMsg;
                            epx.PaymentMethodId = 4;
                            _db.Entry(epx).State = EntityState.Modified;
                            _db.SaveChanges();
                            //MessageBox.Show(element.Value + " - " + element2.Value);


                        }
                        return "Not Successful";
                    }

                }
                else
                {
                       RespMsg = resptext.Substring(resptext.IndexOf(HTTPHeaderDelimiter) + HTTPHeaderDelimiter.Length).Trim();
                        //var xml = XDocument.Parse(RespMsg);
                        tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                        epx.UserId = Convert.ToInt32(LoginInfo.UserId);
                        epx.StoreID = Convert.ToInt32(LoginInfo.StoreID);
                        epx.Response = RespMsg.ToString();
                        epx.Amount = Convert.ToDecimal(totalReceivedAmt);
                        epx.TransactionNo = transNo;
                        epx.PaymentMethodId = 4;
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
    }
}
