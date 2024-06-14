using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using SFPOSWindows.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MenuForm
{

    public partial class MenuLiveCounter : Form
    {
        SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();

        public MenuLiveCounter()
        {
            InitializeComponent();
        }

        private void MenuLiveCounter_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSystem();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuLiveCounter + ex.StackTrace, ex.LineNumber());
            }
        }

        protected void ActivePanel_Click(object sender, EventArgs e)
        {
            try
            {
                var objPictureBox = (sender as PictureBox);
                LoginInfo.CounterIP = objPictureBox.Name.Replace("pic", CommonModelCont.EmptyString);
                frmOrder objfrmOrder = new frmOrder();
                objfrmOrder.dataLoad();

                DataGridViewImageColumn imgEye = new DataGridViewImageColumn();
                var bmp = new Bitmap(Resources.Eye);
                imgEye.Image = bmp;
                objfrmOrder.OrderMasterGrdView.Columns.Add(imgEye);
                imgEye.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                objfrmOrder.ShowDialog();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuLiveCounter + ex.StackTrace, ex.LineNumber());
            }
        }

        protected void ActivePanelDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(this, AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var objPictureBox = (sender as PictureBox);
                    LoginInfo.MacAddress = objPictureBox.Name.Replace("picDelet", CommonModelCont.EmptyString);
                    CounterService _CounterService = new CounterService();
                    bool counters = _CounterService.DeleteCounterByMacAddress(LoginInfo.MacAddress);
                    if (counters)
                    {
                        ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                        LoadSystem();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuLiveCounter + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StatusUpdate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //StatusUpdate();
        }
        public void StatusUpdate()
        {
            try
            {
                CounterService _CounterService = new CounterService();
                var counters = _CounterService.GetAllCounter();

                for (int i = 0; i < counters.Count; i++)
                {
                    PictureBox picturebox = this.Controls.Find("pic" + counters[i].CounterIP.ToString(), true).FirstOrDefault() as PictureBox;
                    if (counters[i].LoginTime != null && counters[i].LogoutTime == null)
                    {
                        var image = new Bitmap(Resources.Active_user1);
                        picturebox.Image = image;
                    }
                    else
                    {
                        var image = new Bitmap(Resources.deActive_user);
                        picturebox.Image = image;
                    }
                }

            }
            catch (Exception ex)
            {
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuLiveCounter + ex.StackTrace, ex.LineNumber());
            }
        }

        private bool CheckForm(Form form)
        {
            form = Application.OpenForms[form.Text];
            if (form != null)
                return true;
            else
                return false;
        }

        private void btnLTSystem_Click(object sender, EventArgs e)
        {
            if (btnLTSystem.Text == "LT System")
            {

                this.Text = "LT System";
                MenuLTCounter objFrmProduct = new MenuLTCounter();
                btnLTSystem.Text = "Live System";
                if (!CheckForm(objFrmProduct))
                {
                    PanelGrid.Controls.Clear();
                    objFrmProduct.TopLevel = false;
                    PanelGrid.Controls.Add(objFrmProduct);
                    objFrmProduct.FormBorderStyle = FormBorderStyle.None;
                    objFrmProduct.Width = PanelGrid.Width;
                    objFrmProduct.Height = PanelGrid.Height;
                    objFrmProduct.Show();
                }
                else
                {
                    Application.OpenForms[objFrmProduct.Name].Focus();
                }
            }
            else
            {

                this.Text = "kom";
                MenuLiveCounter objFrmProduct = new MenuLiveCounter();
                btnLTSystem.Text = "LT System";
                if (!CheckForm(objFrmProduct))
                {
                    PanelGrid.Controls.Clear();
                    objFrmProduct.TopLevel = false;
                    PanelGrid.Controls.Add(objFrmProduct);
                    objFrmProduct.FormBorderStyle = FormBorderStyle.None;
                    objFrmProduct.Width = PanelGrid.Width;
                    objFrmProduct.Height = PanelGrid.Height;
                    objFrmProduct.Show();
                }
                else
                {
                    Application.OpenForms[objFrmProduct.Name].Focus();
                }
            }
        }

        public void LoadSystem()
        {
            try
            {
                PanelGrid.Controls.Clear();
                CounterService _CounterService = new CounterService();
                var counters = _CounterService.GetAllCounter();

                int Counter = 1;
                var pnlLocX = 50;
                var pnlLocY = 100;
                var pnlSizeX = 175;
                var pnlSizeY = 200;

                var pixboxLocX = 30;
                var pixboxLocY = 5;
                var pixboxSizeX = 100;
                var pixboxSizeY = 100;

                var lblLocX = 0;
                var lblLocY = 110;
                var lblSizeX = 175;
                var lblSizeY = 30;

                var lbl_LocX = 0;
                var lbl_LocY = 140;
                var lbl_SizeX = 175;
                var lbl_SizeY = 30;

                var lbl_InfoLocX = 0;
                var lbl_InfoLocY = 170;
                var lbl_InfoSizeX = 175;
                var lbl_InfoSizeY = 30;

                var pixDeleteLocX = 145;
                var pixDeleteLocY = 5;
                var pixDeleteSizeX = 36;
                var pixDeleteSizeY = 36;

                for (int i = 0; i < counters.Count; i++)
                {
                    var dynamicPanel = new Panel();
                    dynamicPanel.Name = "pnl" + counters[i].CounterIP.ToString();
                    dynamicPanel.Size = new Size(pnlSizeX, pnlSizeY);
                    dynamicPanel.Location = new Point(pnlLocX, pnlLocY);
                    dynamicPanel.Cursor = Cursors.No;
                    dynamicPanel.BorderStyle = BorderStyle.FixedSingle;

                    var picturebox = new PictureBox();
                    picturebox.Location = new Point(pixboxLocX, pixboxLocY);
                    picturebox.Name = "pic" + counters[i].CounterIP.ToString();
                    picturebox.Size = new Size(pixboxSizeX, pixboxSizeY);


                    var pictureboxDelete = new PictureBox();
                    pictureboxDelete.Location = new Point(pixDeleteLocX, pixDeleteLocY);
                    pictureboxDelete.Name = "picDelet" + counters[i].MacAddress.ToString();
                    pictureboxDelete.Size = new Size(pixDeleteSizeX, pixDeleteSizeY);
                    var imagedelete = new Bitmap(Resources.delete);
                    pictureboxDelete.Image = imagedelete;
                    pictureboxDelete.Cursor = Cursors.Hand;
                    pictureboxDelete.Click += ActivePanelDelete_Click;
                    dynamicPanel.Controls.Add(pictureboxDelete);

                    if (counters[i].LoginTime != null && counters[i].LogoutTime == null)
                    {
                        var image = new Bitmap(Resources.Active_user1);
                        picturebox.Image = image;
                        dynamicPanel.Cursor = Cursors.Hand;
                        picturebox.Click += ActivePanel_Click;
                    }
                    else
                    {
                        var image = new Bitmap(Resources.deActive_user);
                        picturebox.Image = image;
                    }


                    picturebox.BackgroundImageLayout = ImageLayout.Stretch;
                    dynamicPanel.Controls.Add(picturebox);

                    //string CIP = String.IsNullOrEmpty(counters[i].MacAddress) ? "" : counters[i].MacAddress.ToString();
                    string CIP = String.IsNullOrEmpty(counters[i].CounterNo.ToString()) ? "" : counters[i].CounterNo.ToString();
                    string Version = String.IsNullOrEmpty(counters[i].CurrentVersion) ? "" : counters[i].CurrentVersion.ToString();
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

                    string info_CounterIP = String.IsNullOrEmpty(counters[i].CounterIP) ? "" : counters[i].CounterIP.ToString();
                    string MacAddress = String.IsNullOrEmpty(counters[i].MacAddress) ? "" : counters[i].MacAddress.ToString();
                    var lbl_info = new Label
                    {
                        Location = new Point(lbl_InfoLocX, lbl_InfoLocY),
                        Name = "lbl_info" + i,
                        Size = new Size(lbl_InfoSizeX, lbl_InfoSizeY),
                        Text = info_CounterIP + "(" + MacAddress + ")",
                        BackColor = Color.FromArgb(0, 174, 219),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    dynamicPanel.Controls.Add(lbl_info);



                    pnlLocX += pnlSizeX + 50;

                    if (Counter % 5 == 0)
                    {
                        pnlLocY += pnlSizeY + 50;
                        pnlLocX = 50;
                    }
                    Controls.Add(dynamicPanel);
                    PanelGrid.Controls.Add(dynamicPanel);
                    Counter++;
                }
                timer1.Start();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuLiveCounter + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}