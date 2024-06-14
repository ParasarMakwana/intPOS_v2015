using MetroFramework.Forms;
using SFPOS.Common;
using System;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmSupervisor : MetroForm
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public delegate void onResumeEventHandler(object sender, EventArgs e);
        public event onResumeEventHandler OnResumeEvent;
        public FrmSupervisor()
        {
            InitializeComponent();
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            ManagerAction.suspend = true;
            ManagerAction.TillStatus = false;
            ManagerAction.resume = false;
            this.Close();
            OnMyEvent(this, new EventArgs());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.D2))
            {
                ManagerAction.suspend = true;
                ManagerAction.TillStatus = false;
                ManagerAction.resume = false;
                this.Close();
                OnMyEvent(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.D1))
            {
                ManagerAction.TillStatus = true;
                ManagerAction.suspend = false;
                ManagerAction.resume = false;
                this.Close();
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
                    //FrmResume objResume = new FrmResume();
                    //objResume.ShowDialog();
                }
                else
                {
                    ClsCommon.MsgBox("Information","Your cart must be empty for resume transaction.!", false);
                }
                this.Close();
                OnMyEvent(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.D0))
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnTillStatus_Click(object sender, EventArgs e)
        {
            ManagerAction.TillStatus = true;
            ManagerAction.resume = false;
            ManagerAction.suspend = false;
            this.Close();
            OnMyEvent(this, new EventArgs());
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            //FrmResume objResume = new FrmResume();
            //objResume.ShowDialog();
            this.Close();
            OnResumeEvent(this, new EventArgs());
        }

        private void btnExitWindow_Click(object sender, EventArgs e)
        {
            this.Close();
            OnMyEvent(this, new EventArgs());
        }
    }
}
