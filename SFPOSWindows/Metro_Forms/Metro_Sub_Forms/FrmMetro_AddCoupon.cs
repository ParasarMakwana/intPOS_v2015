using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddCoupon : MetroForm
    {
        #region Properties
        public long CouponID = 0;
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        #endregion

        public FrmMetro_AddCoupon()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case "txtCouponName":
                    //txtCouponName
                    if ((string.IsNullOrWhiteSpace(txtCouponName.Text)))
                    {
                        txtCouponName.Focus();
                        ep.SetError(txtCouponName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtCouponName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtCouponName
                    if ((string.IsNullOrWhiteSpace(txtCouponName.Text)))
                    {
                        txtCouponName.Focus();
                        ep.SetError(txtCouponName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtCouponName, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    CouponService _CouponService = new CouponService();

                    CouponMasterModel objCouponMasterModel = new CouponMasterModel();

                    objCouponMasterModel.CouponName = txtCouponName.Text.Trim();
                    objCouponMasterModel.CoupenCode = (String.IsNullOrEmpty(txtCouponCode.Text) ? GetUniqueToken(8) : (txtCouponCode.Text));
                    objCouponMasterModel.StartDate = datePickerStartDate.Value;
                    objCouponMasterModel.EndDate = datePickerEndDate.Value;
                    objCouponMasterModel.MinPurchaseAmt = (String.IsNullOrEmpty(txtMinPurAmt.Text.ToString()) ? 0 : Convert.ToDecimal(txtMinPurAmt.Text.Trim()));
                    objCouponMasterModel.Discount = (String.IsNullOrEmpty(txtDiscount.Text.ToString()) ? 0 : Convert.ToDecimal(txtDiscount.Text.Trim()));
                    objCouponMasterModel.AvailableCount = Convert.ToInt64(txtCouponFrequency.Text);
                    objCouponMasterModel.UsedCount = 0;
                    
                    if (ToggleIsStaticCoupon.Checked)
                        objCouponMasterModel.IsActive = true;
                    else
                        objCouponMasterModel.IsActive = false;

                    if (CouponID <= 0)
                    {
                        if (toggleIsMulUser.Checked)
                        {
                            var add = _CouponService.AddCoupon(objCouponMasterModel, 1);
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                        else if (!toggleIsMulUser.Checked)
                        {
                            int Coupon = Convert.ToInt32(txtCouponFrequency.Text);

                            for (int row = 0 ;row < Coupon; row++)
                            {
                                objCouponMasterModel.AvailableCount = 1;
                                objCouponMasterModel.CoupenCode = (String.IsNullOrEmpty(txtCouponCode.Text) ? GetUniqueToken(8) : (txtCouponCode.Text)); 
                                var add = _CouponService.AddCoupon(objCouponMasterModel, 1);
                                txtCouponCode.Text = null;
                            }
                        }
                    }
                    else if (CouponID > 0)
                    {
                        objCouponMasterModel.CouponID = CouponID;
                        var add = _CouponService.AddCoupon(objCouponMasterModel, 2);
                        if (add != null)
                        {
                            ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Update, false);
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }

        }

        private void metroBtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtCouponCode.Text = "";
            ToggleIsStaticCoupon.Checked = true;
            toggleIsMulUser.Checked = false;
            txtCouponName.Text = "";
            txtCouponFrequency.Text = "";
            txtMinPurAmt.Text = "";
            txtDiscount.Text = "";
            datePickerStartDate.Value = DateTime.Now;
            datePickerEndDate.Value = DateTime.Now;
        }

        private void ToggleIsStaticCoupon_CheckedChanged(object sender, EventArgs e)
        {
            if (ToggleIsStaticCoupon.Checked)
            {
                toggleIsMulUser.Checked = false;
                toggleIsMulUser.Visible = false;
                lblMultipleUse.Visible = false;
                txtCouponCode.Text = "";
                txtCouponCode.Enabled = true;
            }
            else
            {
                txtCouponCode.Text = GetUniqueToken(8);
                txtCouponCode.Enabled = false;
                lblMultipleUse.Visible = true;
                toggleIsMulUser.Visible = true;
            }
        }

        //public string GenerateRandomString(int length)
        //{
        //    var builder = new StringBuilder();
        //    while (builder.Length < length)
        //    {
        //        builder.Append(Guid.NewGuid().ToString());
        //    }
        //    return builder.ToString(0, length);
        //}

        //private string GetUniqueKey()
        //{
        //    int maxSize = 8;
        //    int minSize = 6;
        //    char[] chars = new char[62];
        //    string a;
        //    a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        //    chars = a.ToCharArray();
        //    int size = maxSize;
        //    byte[] data = new byte[1];
        //    RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        //    crypto.GetNonZeroBytes(data);
        //    size = maxSize;
        //    data = new byte[size];
        //    crypto.GetNonZeroBytes(data);
        //    StringBuilder result = new StringBuilder(size);
        //    foreach (byte b in data)
        //    {
        //        result.Append(chars[b % (chars.Length)]);
        //    }
        //    return result.ToString();
        //}

        public static string GetUniqueToken(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                byte[] buffer = null;

                int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);

                crypto.GetBytes(data);
                char[] result = new char[length];

                for (int i = 0; i < length; i++)
                {
                    byte value = data[i];
                    while (value > maxRandom)
                    {
                        if (buffer == null)
                        {
                            buffer = new byte[1];
                        }

                        crypto.GetBytes(buffer);
                        value = buffer[0];
                    }
                    result[i] = chars[value % chars.Length];
                }
                return new string(result);
            }
        }

        private void toggleIsMulUser_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleIsMulUser.Checked)
            {
                if (!ToggleIsStaticCoupon.Checked)
                {
                    txtCouponCode.Text = GetUniqueToken(8);
                    txtCouponCode.Enabled = false;
                }
                else
                {
                    txtCouponCode.Text = "";
                    txtCouponCode.Enabled = true;
                }
                ToggleIsStaticCoupon.Checked = false;
            }
            else
            {
                if (!ToggleIsStaticCoupon.Checked)
                {
                    txtCouponCode.Text = GetUniqueToken(8);
                    txtCouponCode.Enabled = false;
                }
                else
                {
                    txtCouponCode.Text = "";
                    txtCouponCode.Enabled = true;
                }
                ToggleIsStaticCoupon.Checked = false;
            }
        }
    }
}

