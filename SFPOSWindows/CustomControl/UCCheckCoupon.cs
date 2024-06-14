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

namespace SFPOSWindows.CustomControl
{
    public partial class UCCheckCoupon : UserControl
    {
        #region Properties
        CouponService _CouponService = new CouponService();
        public decimal MinPurchaseAmount = 0;
        List<CouponMasterModel> LstCouponMasterModel = new List<CouponMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        #endregion

        #region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                GetCouponValidate();
            }
            catch (Exception ex)
            {
                txtCouponCode.Text = "";
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                txtCouponCode.Text = "";
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtCouponCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    GetCouponValidate();
                }
            }
            catch (Exception ex)
            {
                txtCouponCode.Text = "";
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public UCCheckCoupon()
        {
            InitializeComponent();
        }

        public void GetCouponValidate()
        {
            try
            {
                if (txtCouponCode.Text != "")
                {
                    LstCouponMasterModel = _CouponService.GetCouponCode(txtCouponCode.Text);
                    if (LstCouponMasterModel.Count != 0)
                    {
                        CouponInfo.CouponCode = LstCouponMasterModel.FirstOrDefault().CoupenCode;
                        CouponInfo.Discount = Convert.ToDecimal(LstCouponMasterModel.FirstOrDefault().Discount);
                        CouponInfo.MinPurAmt = Convert.ToDecimal(LstCouponMasterModel.FirstOrDefault().MinPurchaseAmt);
                        CouponInfo.availableCoupon = Convert.ToInt64(LstCouponMasterModel.FirstOrDefault().AvailableCount);
                        CouponInfo.usedCoupon = Convert.ToInt64(LstCouponMasterModel.FirstOrDefault().UsedCount);
                        if (MinPurchaseAmount < CouponInfo.MinPurAmt)
                        {
                            ClsCommon.MsgBox("Information", "Minimum purchase amount should be " + CouponInfo.MinPurAmt + " to use this coupon!", false);
                            txtCouponCode.Text = "";
                            txtCouponCode.Focus();
                        }
                        else if (CouponInfo.availableCoupon == CouponInfo.usedCoupon)
                        {
                            ClsCommon.MsgBox("Information", "Can't use single coupon multiple times!", false);
                            txtCouponCode.Text = "";
                            txtCouponCode.Focus();
                        }
                        else
                        {
                            CouponInfo.isCoupon = true;
                            this.Hide();
                        }
                    }
                    else
                    {
                        ClsCommon.MsgBox("Information", "Coupon Code may be out dated or previously used!", false);
                        txtCouponCode.Text = "";
                        txtCouponCode.Focus();
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please enter coupon code!", false);
                    txtCouponCode.Focus();
                }
            }
            catch (Exception ex)
            {
                txtCouponCode.Text = "";
                txtCouponCode.Focus();
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {
                    this.Hide();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

       
    }
}
