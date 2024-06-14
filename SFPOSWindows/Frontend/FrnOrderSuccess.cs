using MetroFramework.Forms;
using SFPOS.Common;
using System;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrnOrderSuccess : MetroForm
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public FrnOrderSuccess()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
            OnMyEvent(this, new EventArgs());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                this.Close();
                OnMyEvent(this, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrnOrderSuccess_Load(object sender, EventArgs e)
        {
            timer1.Interval = XMLData.OrderSuccessScreen * 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
            OnMyEvent(this, new EventArgs());
        }
    }
}
