using MetroFramework.Forms;
using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using SFPOSWindows.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;



namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddLTSystem : MetroForm
    {
        #region Properties
        public long CouponID = 0;
        CounterService _CounterService = new CounterService();
        CounterMasterModel _CouterMasterModel = new CounterMasterModel();
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        #endregion

        public FrmMetro_AddLTSystem()
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

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {

        }

        private void metroBtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            //txtCouponCode.Text = "";
            //ToggleIsStaticCoupon.Checked = true;
            //toggleIsMulUser.Checked = false;
            //txtCouponName.Text = "";
            //txtCouponFrequency.Text = "";
            //txtMinPurAmt.Text = "";
            //txtDiscount.Text = "";
            //datePickerStartDate.Value = DateTime.Now;
            //datePickerEndDate.Value = DateTime.Now;
        }
        private void FrmMetro_AddLTSystem_Load(object sender, EventArgs e)
        {
            GenrateTicketScreen();
        }
        private void GenrateTicketScreen()
        {
            try
            {
                var counters = _CounterService.GetAllCounter();
                var len = counters.Count;
                this.AutoScroll = true;
                int Counter = 1;
                var pnlLocX = 50;
                var pnlLocY = 100;
                var pnlSizeX = 160;
                var pnlSizeY = 170;

                var pixboxLocX = 30;
                var pixboxLocY = 5;
                var pixboxSizeX = 100;
                var pixboxSizeY = 100;

                var lblLocX = 0;
                var lblLocY = 110;
                var lblSizeX = 160;
                var lblSizeY = 30;

                var chkLocX = 137;
                var chkLocY = 0;
                var chkSizeX = 20;
                var chkSizeY = 30;

                var lbl_LocX = 0;
                var lbl_LocY = 140;
                var lbl_SizeX = 160;
                var lbl_SizeY = 30;

                for (int i = 0; i < (counters.Count); i++)
                {

                    var dynamicPanel = new Panel();
                    dynamicPanel.Name = "pnl" + counters[i].CounterIP.ToString();
                    dynamicPanel.Size = new Size(pnlSizeX, pnlSizeY);
                    dynamicPanel.Location = new Point(pnlLocX, pnlLocY);
                    dynamicPanel.Cursor = Cursors.No;

                    dynamicPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    var picturebox = new PictureBox();
                    picturebox.Location = new Point(pixboxLocX, pixboxLocY);
                    picturebox.Name = "pic" + counters[i].CounterIP.ToString();
                    picturebox.Size = new Size(pixboxSizeX, pixboxSizeY);


                    if (counters[i].IsLTSystemActive)
                    {
                        var image = new Bitmap(Resources.ActiveTicket);
                        picturebox.Image = image;
                        dynamicPanel.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        var image = new Bitmap(Resources.deActiveTicket);
                        picturebox.Image = image;
                        dynamicPanel.Cursor = Cursors.Hand;
                    }



                    picturebox.BackgroundImageLayout = ImageLayout.Stretch;
                    dynamicPanel.Controls.Add(picturebox);

                    //string CIP = String.IsNullOrEmpty(counters[i].MacAddress) ? "" : counters[i].MacAddress.ToString();
                    string CIP = String.IsNullOrEmpty(counters[i].CounterNo.ToString()) ? "" : counters[i].CounterNo.ToString();
                    string Version = String.IsNullOrEmpty(counters[i].CurrentVersion) ? "" : counters[i].CurrentVersion.ToString();
                    var chkBox = new CheckBox();
                    if (counters[i].IsLTSystemActive)
                    {
                        chkBox = new CheckBox
                        {
                            Location = new Point(chkLocX, chkLocY),
                            Name = "chkBox" + counters[i].CounterID,
                            Size = new Size(chkSizeX, chkSizeY),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Checked = true
                        };
                    }
                    else
                    {
                        chkBox = new CheckBox
                        {
                            Location = new Point(chkLocX, chkLocY),
                            Name = "chkBox" + counters[i].CounterID,
                            Size = new Size(chkSizeX, chkSizeY),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Checked = false
                        };
                    }
                    chkBox.CheckedChanged += CheckPanel_ChangeEvent;
                    dynamicPanel.Controls.Add(chkBox);

                    var lbl = new Label
                    {
                        Location = new Point(lblLocX, lblLocY),
                        Name = "lbl" + i,
                        Size = new Size(lblSizeX, lblSizeY),
                        Text = "CR-" + CIP + "  (" + Version + ")",
                        BackColor = Color.FromArgb(0, 174, 219),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    dynamicPanel.Controls.Add(lbl);

                    var lbl_ = new Label
                    {
                        Location = new Point(lbl_LocX, lbl_LocY),
                        Name = "lbl1_" + i,
                        Size = new Size(lbl_SizeX, lbl_SizeY),
                        Text = String.IsNullOrEmpty(counters[i].CurrentLoginUser) ? "" : counters[i].CurrentLoginUser.ToString(),
                        BackColor = Color.FromArgb(0, 121, 164),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    dynamicPanel.Controls.Add(lbl_);


                    pnlLocX += pnlSizeX + 50;

                    if (Counter % 6 == 0)
                    {
                        pnlLocY += pnlSizeY + 50;
                        pnlLocX = 50;
                    }
                    Controls.Add(dynamicPanel);
                    Counter++;

                }
                //timer1.Start();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuLiveCounter + ex.StackTrace, ex.LineNumber());
            }
        }

        protected void CheckPanel_ChangeEvent(object sender, EventArgs e)
        {
            try
            {
                var objCheckBox = (sender as CheckBox);
                var CounterId = objCheckBox.Name.Replace("chkBox", CommonModelCont.EmptyString);
                if (objCheckBox.Checked)
                {
                    _CouterMasterModel.CounterID = Convert.ToInt32(CounterId);
                    _CouterMasterModel.IsLTSystemActive = true;
                    _CounterService.AddCounter(_CouterMasterModel, 2);
                }
                else
                {
                    _CouterMasterModel.CounterID = Convert.ToInt32(CounterId);
                    _CouterMasterModel.IsLTSystemActive = false;
                    _CounterService.AddCounter(_CouterMasterModel, 2);
                }
                GenrateTicketScreen();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuLiveCounter + ex.StackTrace, ex.LineNumber());
            }
        }

    }
}

