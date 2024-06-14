using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmFoodStamp : MetroForm
    {
        public decimal FSEligibleAmt = 0;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        SerialPort ComPort = new SerialPort();
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        delegate void SetTextCallback(string text);
        string InputData = String.Empty;
        public FrmFoodStamp()
        {
            InitializeComponent();
            //ComPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived_1);
            //OpenPort();
            
        }
        public void OpenPort()
        {
            try
            {

                string[] Ports = SerialPort.GetPortNames();
                if (Ports.Count() > 0)
                {
                    if (ComPort.IsOpen == false)
                    {
                        ComPort.PortName = ComInfo.ComPort;
                        ComPort.BaudRate = ComInfo.BaudRate;
                        ComPort.DataBits = ComInfo.DataBits;
                        ComPort.StopBits = ComInfo.StopBits;
                        ComPort.Handshake = ComInfo.Handshake;
                        ComPort.Parity = ComInfo.Parity;
                        ComPort.RtsEnable = true;
                        ComPort.DtrEnable = true;
                        ComPort.Open();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }
        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Thread.Sleep(100);
                InputData = ComPort.ReadExisting();
                if (InputData != String.Empty)
                {
                    this.BeginInvoke(new SetTextCallback(SetText), new object[] { InputData });
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }

        public void PortOpen_Close(bool Status)
        {
            try
            {
                string[] Ports = SerialPort.GetPortNames();
                if (Ports.Count() > 0)
                {
                    if (Status)
                    {
                        if (ComPort.IsOpen == false)
                        {
                            ComPort.Open();
                        }
                    }
                    else
                    {
                        if (ComPort.IsOpen == true)
                        {
                            ComPort.Close();
                        }
                    }
                }
            }  
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
}
        private void SetText(string text)
        {
            try
            {
                hidelbl.Text = text;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UpdateFoodStamp()
        {
            try
            {
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
                            AddPaymentList(Functions.GetDecimal(txtFSEligible.Text));
                            this.Close();
                        }
                        else if ((Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) > (Functions.GetDecimal(txtFSEligible.Text)))
                        {
                            ClsCommon.MsgBox("Information","Amount should be less than or equal to Foodstamp eligible amount.", false);
                            txtFSAmount.Text = "";
                            txtFSAmount.Focus();
                        }
                        else if ((Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) == (Functions.GetDecimal(txtFSEligible.Text)))
                        {
                            OrderInfo.FSTotal += Functions.GetDecimal(fsAmt) / 100;
                            AddPaymentList(Functions.GetDecimal(fsAmt) / 100);
                            this.Close();
                        }
                        else if ((Functions.GetDecimal(fsAmt == "" ? "0" : fsAmt) / 100) < (Functions.GetDecimal(txtFSEligible.Text)))
                        {
                            OrderInfo.FSTotal += Functions.GetDecimal(fsAmt) / 100;
                            OrderInfo.remainingFSAmt = (Functions.GetDecimal(txtFSEligible.Text) - (Functions.GetDecimal(fsAmt) / 100));
                            AddPaymentList(Functions.GetDecimal(fsAmt) / 100);
                            this.Close();
                        }
                    }

                    else if (txtFSAmount.Text.Trim().ToLower().EndsWith("cn"))
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong.", false);
                txtFSAmount.Text = "";
                txtFSAmount.Focus();
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }

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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmFoodStamp_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //PortOpen_Close(false);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
        }

        public void AddPaymentList(decimal Amount)
        {
            PaymentTransMasterModel _PaymentTransMasterModel = new PaymentTransMasterModel();
            _PaymentTransMasterModel.FoodStampAmount = Amount;
            _PaymentTransMasterModel.PaymentMethodID = 4;
            //_PaymentTransMasterModel.Balance = (Functions.GetDecimal(txtFSEligible.Text)) - (Functions.GetDecimal(txtFSAmount.Text.ToLower().Replace("fs", "")) / 100);
            MultiPaymentInfo.lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {
                    this.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmFoodStamp + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
