using MetroFramework.Forms;
using SFPOS.Common;
using SFPOSWindows.Frontend;
using System.Windows.Forms;
using System;

namespace SFPOSWindows
{
    public partial class ConfirmMessageBox : MetroForm
    {
        public bool IsCancel = false;
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public string input1 = "";
        public string input2 = "";
        public ConfirmMessageBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.Close();
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            IsCancel = true;
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void CustomMessageBox_Load(object sender, System.EventArgs e)
        {
            this.BringToFront();
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

        private void CustomMessageBox_Shown(object sender, System.EventArgs e)
        {
            // this.Location = Screen.AllScreens[0].Bounds.Location;
            //// this.CenterToScreen();
            // this.WindowState = FormWindowState.Normal;
            // //this.StartPosition = FormStartPosition.CenterParent;
            // this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            // this.Bounds = Screen.AllScreens[0].Bounds;
        }
    }
}
