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
using SFPOS.Entities;
using SFPOS.Entities.FrontEnd;
using SFPOS.BAL.Frontend;
using SFPOS.DAL;

namespace SFPOSWindows.CustomControl
{
    public partial class UCCheckCouponNew : UserControl
    {
        #region Properties
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public decimal MinPurchaseAmount = 0;
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public delegate void onCheckCustomerEventHandler(object sender, EventArgs e);
        public event onCheckCustomerEventHandler onCheckCustomerEvent;
        public bool IsCancel = false;
        public bool NoCoupun = false;
        public bool IsAllowMultiple = false;
        public string verfiyCouponCustomer = null;
        CouponServices _CouponServices = new CouponServices();
        public string CouponCode = "";
        CustomerAppliedCouponService _customerAppliedService = new CustomerAppliedCouponService();
        List<CustomerAppliedCouponModel> LstCustomerAppliedCouponModel = new List<CustomerAppliedCouponModel>();
        List<CouponModel> LstCouponModel = new List<CouponModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCheckCouponNew));
        #endregion

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                IsCancel = true;
                this.Hide();
                OnMyEvent(this, new EventArgs());

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }


        #endregion

        #region Functions
        public UCCheckCouponNew()
        {
            InitializeComponent();
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

        private void UCCheckCouponNew_Load(object sender, EventArgs e)
        {
            LstCouponModel = _CouponServices.GetCoupon(MinPurchaseAmount, CustomerInfo.CustomerId);
            try
            {
                if (LstCouponModel.Count != 0)
                {
                    var len = LstCouponModel.Count;
                    var _nextbtncount = 0;
                    var locX_Dim = 0;
                    var locY_Dim = 0;
                    var countbtn = 0;
                    var mainCount = (len > 3 ? len - 3 : 1);
                    var subCount = (len > 3 ? 3 : len);
                    for (int i = 0; i < 3; i++)
                    {
                        locX_Dim = 7;
                        if (i == 0)
                        {
                            locY_Dim = 8;
                        }
                        for (int j = 0; j < 2; j++)
                        {
                            Button obj = new Button();

                            if (_nextbtncount < len)
                            {
                                obj.Name = "btn" + LstCouponModel[_nextbtncount].CouponID.ToString();
                                obj.Text = LstCouponModel[_nextbtncount].CouponName.ToString();
                                obj.Location = new System.Drawing.Point(locX_Dim + 15, locY_Dim + 55);

                                obj.BackgroundImage = ((Image)(resources.GetObject("button1.BackgroundImage")));
                                obj.Click += delegate (object cs, EventArgs ev) { button_Click(cs, ev, ""); };
                                _nextbtncount = _nextbtncount + 1;
                            }
                            else
                            {
                                obj.Name = "btn" + countbtn;
                                obj.Text = "-";
                                obj.Location = new System.Drawing.Point(locX_Dim + 15, locY_Dim + 55);
                                obj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
                                countbtn++;
                            }

                            obj.Anchor = System.Windows.Forms.AnchorStyles.None;
                            obj.BackColor = System.Drawing.Color.White;
                            obj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            obj.Cursor = System.Windows.Forms.Cursors.Hand;
                            obj.FlatAppearance.BorderSize = 0;
                            obj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                            obj.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            obj.Size = new System.Drawing.Size(160, 45);
                            this.Controls.Add(obj);
                            locX_Dim = locX_Dim + 208;
                        }
                        locY_Dim = locY_Dim + 51;

                    }
                }
                else
                {
                    NoCoupun = true;
                    this.Hide();
                    OnMyEvent(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, ex.StackTrace, ex.LineNumber());
            }

        }

        private void button_Click(object sender, EventArgs e, string meassage)
        {
            string _CouponCode = string.Empty;
            var objPictureBox = (sender as Button);
            if (objPictureBox.Text != "-")
            {
                CouponInfo.CouponId = Convert.ToInt32(objPictureBox.Name.Replace("btn", "").Trim());
                tbl_CouponMaster objCouponMaster = new tbl_CouponMaster();
                objCouponMaster = _db.tbl_CouponMaster.Where(p => p.CouponName == objPictureBox.Text).FirstOrDefault();
                IsAllowMultiple = objCouponMaster.IsAllowMultipleTime.Value;
                var _verfiyCouponCustomer = _customerAppliedService.GetCustomerAppliedCoupon(CustomerInfo.CustomerId, CouponInfo.CouponId).FirstOrDefault();
                verfiyCouponCustomer = _verfiyCouponCustomer?.ToString() ?? string.Empty;
                bool IsRestricted = objCouponMaster.IsRestricted ?? false;

                if (IsRestricted == false)
                {
                    Fn_ApplyCoupon();
                }
                else
                {
                    onCheckCustomerEvent(this, new EventArgs());
                    Fn_ApplyCoupon();
                }
            }
        }

        private void ApplyeCoupon(long CoupenID)
        {
            string _CouponCode = string.Empty;

            LstCouponModel = LstCouponModel.Where(person => person.CouponID == CouponInfo.CouponId).ToList();
            _CouponCode = LstCouponModel[0].CoupenCode.ToString();
            CouponCode = _CouponCode;
            var LstCouponMasterModel = _db.tbl_CouponMaster.Where(p => p.CouponID == CouponInfo.CouponId).FirstOrDefault();

            CouponInfo.isCoupon = true;
            CouponInfo.CouponCode = LstCouponMasterModel.CoupenCode;
            CouponInfo.Discount = Convert.ToDecimal(LstCouponMasterModel.Discount);
            CouponInfo.MinPurAmt = Convert.ToDecimal(LstCouponMasterModel.MinPurchaseAmt);
            CouponInfo.availableCoupon = Convert.ToInt64(LstCouponMasterModel.AvailableCount);
            CouponInfo.usedCoupon = Convert.ToInt64(LstCouponMasterModel.UsedCount);
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        public void Fn_ApplyCoupon()
        {
            if (IsAllowMultiple == true)
            {
                ApplyeCoupon(CouponInfo.CouponId);
            }
            else
            {
                if (verfiyCouponCustomer == "" || verfiyCouponCustomer == null)
                {
                    ApplyeCoupon(CouponInfo.CouponId);
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Coupon has been used already.", false);
                    NoCoupun = false;
                    OnMyEvent(this, new EventArgs());
                }
            }
        }
    }
}
