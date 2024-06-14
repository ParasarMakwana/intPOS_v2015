using SFPOS.Common;
using SFPOSWindows.Frontend;
using System;
using System.Windows.Forms;

namespace SFPOSWindows.CustomControl
{
    public partial class UCSupervisor : UserControl
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public delegate void onResumeEventHandler(object sender, EventArgs e);
        public event onResumeEventHandler OnResumeEvent;

        public UCSupervisor()
        {
            InitializeComponent();
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            ManagerAction.suspend = true;
            ManagerAction.TillStatus = false;
            ManagerAction.resume = false;
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.D2))
            {
                ManagerAction.suspend = true;
                ManagerAction.TillStatus = false;
                ManagerAction.resume = false;
                this.Hide();
                OnMyEvent(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.D1))
            {
                ManagerAction.TillStatus = true;
                ManagerAction.suspend = false;
                ManagerAction.resume = false;
                this.Hide();
                OnMyEvent(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.D3))
            {
                if (btnResume.Enabled == true)
                {
                    ManagerAction.suspend = false;
                    ManagerAction.TillStatus = false;
                    OnResumeEvent(this, new EventArgs());
                    //UCResume objResume = new UCResume();
                    //objResume.OnMyEvent += new UCResume.onMyEventHandler(CatchResumeEvent);
                    //objResume.Show();
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Your cart must be empty for resume transaction.!", false);
                }
                this.Hide();
                OnMyEvent(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.D0))
            {
                this.Hide();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnTillStatus_Click(object sender, EventArgs e)
        {
            ManagerAction.TillStatus = true;
            ManagerAction.resume = false;
            ManagerAction.suspend = false;
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            this.Hide();
            OnResumeEvent(this, new EventArgs());
            //UCResume objResume = new UCResume();
            //objResume.OnMyEvent += new UCResume.onMyEventHandler(CatchResumeEvent);
            //objResume.Show();
        }

        private void btnExitWindow_Click(object sender, EventArgs e)
        {
            ManagerAction.TillStatus = false;
            ManagerAction.resume = false;
            ManagerAction.suspend = false;
            ManagerAction.Exit = true;
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }
    }
}
