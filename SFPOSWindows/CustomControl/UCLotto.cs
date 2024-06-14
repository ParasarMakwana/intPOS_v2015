using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using SFPOS.BAL.Frontend;
using System.Text.RegularExpressions;

namespace SFPOSWindows.CustomControl
{
    public partial class UCLotto : UserControl
    {
        #region Properties
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public LottoModel objLottoModel = new LottoModel();

        #endregion
        public UCLotto()
        {
            InitializeComponent();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtLottoAmount.Text.Trim()) && (Regex.Match(txtLottoAmount.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success)
                {
                    AddLottoTransaction(1);
                    OnMyEvent(this, new EventArgs());
                    ClsCommon.MsgBox("Success", "Lotto sales successfully", false);
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Invalid amount!", false);
                    txtLottoAmount.Text = "";
                    txtLottoAmount.Focus();
                }
            }
            catch (Exception ex)
            {

                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmLotto + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnPayout_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtLottoAmount.Text.Trim()) && (Regex.Match(txtLottoAmount.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success)
                {
                    AddLottoTransaction(2);
                    OnMyEvent(this, new EventArgs());
                    ClsCommon.MsgBox("Success", "Lotto payout successfully", false);
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Invalid amount!", false);
                    txtLottoAmount.Text = "";
                    txtLottoAmount.Focus();
                }
            }
            catch (Exception ex)
            {

                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmLotto + ex.StackTrace, ex.LineNumber());
            }
        }

        private void UCLotto_Load(object sender, EventArgs e)
        {
            try
            {
                txtLottoAmount.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmLotto + ex.StackTrace, ex.LineNumber());
            }
        }

        #region Function
        public void AddLottoTransaction(int lottoType)
        {
            try
            {
                txtLottoAmount.Focus();
                objLottoModel = new LottoModel();
                objLottoModel.LottoPrice = Functions.GetDecimal(txtLottoAmount.Text.Trim()) / 100;
                objLottoModel.LottoType = lottoType;
                objLottoModel.IsActive = true;
                objLottoModel.IsDelete = false;
                objLottoModel.CounterIP = LoginInfo.CounterIP;
                objLottoModel.MacAddress = LoginInfo.MacAddress;
                objLottoModel.CreatedBy = LoginInfo.UserId;
                objLottoModel.CreatedDate = DateTime.Now;
                objLottoModel.StoreID = LoginInfo.StoreID;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmLotto + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
